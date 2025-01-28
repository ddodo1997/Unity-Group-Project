using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Creature
{
    public enum Type
    {
        Normal,
        Elite,
        Boss
    };
    public enum Status
    {
        Idle,
        Move,
        Aggro,
        Attack,
        Die
    };
    public Animator animator;

    private Rigidbody2D rb;
    public Player player;
    private BoxCollider2D attackArea;
    private GameObject prefab;
    private ItemDrop itemDrop;

    public Status currentStauts = Status.Idle;
    private Vector2 direction;

    private float statusStartTime = 0f;
    private float maxIdleTime;
    private float maxMoveTime;
    public float targetDistance;

    public int tempLevel;

    private bool isMoving = false;
    public bool isDie = false;


    [SerializeField]
    public MonsterStatus status = new MonsterStatus();
    public override void Attack()
    {
        if (Time.time - statusStartTime > status.CoolTime)
        {
            animator.SetTrigger(attackTrigger);
            statusStartTime = Time.time;
        }
    }
    public override void Move()
    {
        rb.velocity = direction * status.Agility;
    }

    public override void OnDamage(float damage)
    {
        //비선공몹용 코드
        if (currentStauts != Status.Aggro)
            SetStatus(Status.Aggro);

        status.Health -= damage;
        if (status.Health <=0f)
        {
            OnDie();
            return;
        }
        animator.SetTrigger(damageTrigger);
    }

    public override void OnDie()
    {
        StartCoroutine(AfterDie());


        rb.isKinematic = true;
        isDie = true;
        animator.SetTrigger(deathTrigger);
        animator.SetBool(dieBool, isDie);

        itemDrop.Drop(status);
    }
    public void StartGame(string name)
    {
        status.SetStatus(name);
        var path = string.Format(PathFormats.prefabs, name);
        prefab = (GameObject)Instantiate(Resources.Load(path), transform.position, transform.rotation);
        prefab.transform.SetParent(transform, false);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        itemDrop = GetComponent<ItemDrop>();
        StartGame("SPUM_20241203203032691");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        attackArea = GetComponent<BoxCollider2D>();

        attackArea.size = new Vector2(status.Range, attackArea.size.y);
        attackArea.offset = new Vector2(attackArea.offset.x - status.Range * 0.5f, attackArea.offset.y);


        Animator[] AllAnimators = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator trans in AllAnimators)
        {
            if (trans.name == "UnitRoot")
            {
                animator = trans;
                break;
            }
        }

        //테스트용 코드
        transform.position = new Vector3(-9, 0, 0);
    }
    private void SetStatus(Status stat)
    {
        currentStauts = stat;
        statusStartTime = Time.time;
        switch (currentStauts)
        {
            case Status.Idle:
                maxIdleTime = Random.Range(2f, 4f);
                rb.velocity = Vector2.zero;
                isMoving = false;
                break;
            case Status.Move:
                direction = Random.insideUnitCircle.normalized;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                maxMoveTime = Random.Range(1f, 3f);
                break;
            case Status.Aggro:
                direction = (player.transform.position - transform.position).normalized;
                break;
            case Status.Attack:
                rb.velocity = Vector2.zero;
                break;
        }
    }
    private void Update()
    {
        if (player.isDie)
            return;
        isMoving = rb.velocity.magnitude > 0;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
            OnColliderEnable();
        else
            OnColliderDisable();




#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            itemDrop.Drop(status);
        }
#endif
    }

    public void OnColliderEnable()
    {
        attackArea.enabled = true;
    }

    public void OnColliderDisable()
    {
        attackArea.enabled = false;
    }

    private void FixedUpdate()
    {
        if (isDie || player.isDie)
        {
            return;
        }
        targetDistance = (player.transform.position - transform.position).magnitude;
        switch (currentStauts)
        {
            case Status.Idle:
                if (Time.time - statusStartTime > maxIdleTime)
                {
                    SetStatus(Status.Move);
                }
                break;
            case Status.Move:
                if (Time.time - statusStartTime > maxMoveTime)
                {
                    SetStatus(Status.Idle);
                }
                else
                {
                    //Move();
                }

                //선공몹 처리
                if (tempLevel >= status.Level && targetDistance < status.Distance)
                    SetStatus(Status.Aggro);
                break;
            case Status.Aggro:
                direction = (player.transform.position - transform.position).normalized;
                if (targetDistance > status.Distance)
                {
                    SetStatus(Status.Idle);
                }
                else if (targetDistance > status.Range)
                {
                    Move();
                }
                else
                {
                    SetStatus(Status.Attack);
                }
                break;
            case Status.Attack:
                direction = (player.transform.position - transform.position).normalized;
                if (targetDistance > status.Range)
                    SetStatus(Status.Aggro);
                else
                    Attack();
                break;
        }

        if (direction.x < 0)
        {
            transform.rotation = Direction.Left;
        }
        else
        {
            transform.rotation = Direction.Right;
        }
        animator.SetBool(moveBool, isMoving);
    }

    public IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player) && !collision.isTrigger)
        {
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.OnDamage(status.Strength);
            }
        }
    }
}
