using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : LivingEntity
{
    public enum Status
    {
        Idle,
        Aggro,
        Attack,
        Fire,
        Wait,
        Return,
        Die
    };
    public GameManager gameManager;
    public Animator animator;
    public BossMonsterHpBar hpBar;
    private Rigidbody2D rb;
    public Player player;
    private CapsuleCollider2D body;
    private BoxCollider2D attackArea;
    private GameObject prefab;
    private ItemDrop itemDrop;

    public Status currentStauts = Status.Idle;
    private Vector2 direction;

    private float statusStartTime = 0f;
    public float targetDistance;

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

    public void GoBack()
    {
        direction = -direction;
    }
    public override void Move()
    {
        rb.velocity = direction * status.MovementSpeed;
    }

    public override void OnDamage(float damage)
    {
        if (status.Agility * 0.01 + status.Level >= Random.Range(0, 1000))
            return;

        status.hp -= Mathf.Clamp(damage - status.Defense * (status.Level * 0.1f), 0f, float.MaxValue);
        hpBar.UpdateHpBar(status);
        if (status.hp <= 0f)
        {
            OnDie();
            return;
        }
        animator.SetTrigger(damageTrigger);
    }

    public override void OnDie()
    {
        StartCoroutine(AfterDie());
        gameManager.UpdateMonsterList();
        body.enabled = false;
        rb.isKinematic = true;
        isDie = true;
        animator.SetTrigger(deathTrigger);
        animator.SetBool(dieBool, isDie);

        itemDrop.Drop();
    }

    public void SettingMonster(MonsterStatus status)
    {
        this.status = status;
        var path = string.Format(PathFormats.prefabs, status.Id);
        prefab = (GameObject)Instantiate(Resources.Load(path), Vector3.zero, transform.rotation);
        prefab.transform.SetParent(transform, false);
        attackArea = GetComponent<BoxCollider2D>();
        attackArea.size = new Vector2(status.Range, attackArea.size.y);
        attackArea.offset = new Vector2(attackArea.offset.x - status.Range * 0.5f, attackArea.offset.y);
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        animator = gameObject.GetComponentsInChildren<Animator>()[0];
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        itemDrop = GetComponent<ItemDrop>();
        body = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag(Tags.GameManager).GetComponent<GameManager>();
    }
    public void SetStatus(Status stat)
    {
        currentStauts = stat;
        statusStartTime = Time.time;
        switch (currentStauts)
        {
            case Status.Idle:
                rb.velocity = Vector2.zero;
                isMoving = false;
                break;
            case Status.Aggro:
                if (!player.isAggroAble)
                    SetStatus(Status.Idle);
                direction = (player.transform.position - transform.position).normalized;
                break;
            case Status.Attack:
                rb.velocity = Vector2.zero;
                break;
            case Status.Return:

                break;
            case Status.Die:
                break;
        }
    }
    private void Update()
    {
        if (player.isDie)
            return;
        isMoving = rb.velocity.magnitude > 0;

        hpBar.UpdateHpBar(status);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
            OnColliderEnable();
        else
            OnColliderDisable();
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
            case Status.Return:
                break;
            case Status.Die:
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

            if (Mathf.Clamp(player.status.Agility - (status.Agility * 0.5f), 0f, 50f) <= Random.Range(0, 100))
                return;

            if (player != null)
            {
                player.OnDamage(status.CriticalChance >= Random.Range(0f, 100f) ? status.Strength * (status.Critical * 0.3f) : status.Strength);
            }
        }
    }

}
