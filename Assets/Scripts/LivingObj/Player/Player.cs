using System.Collections;
using UnityEngine;

public class Player : LivingEntity
{
    public GameManager gameManager;
    public VirtualJoyStick moveJoystick;
    public VirtualJoyStick attackJoystick;
    public PlayerStatus status = new PlayerStatus();

    private Rigidbody2D rb;
    public BoxCollider2D attackArea;
    private CircleCollider2D hitBox;
    private GameObject prefab;
    public Animator animator;
    public SpriteRenderer weaponRenderer;
    public PlayerEquipment playerEquip;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    private float startAttackTime;
    private bool isMoving = false;
    public bool isDie = false;

    public void Rotation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
        {
            return;
        }
        if (attackJoystick.Input.magnitude != 0)
        {
            if (attackJoystick.Input.x < 0)
            {
                lookDirection = attackJoystick.Input;
                transform.rotation = Direction.Left;
            }
            else
            {
                transform.rotation = Direction.Right;
            }
        }
        else if (moveJoystick.Input.magnitude != 0)
        {
            if (moveJoystick.Input.x < 0)
            {
                lookDirection = moveJoystick.Input;
                transform.rotation = Direction.Left;
            }
            else
            {
                transform.rotation = Direction.Right;
            }
        }
    }

    public override void Attack()
    {
        if (attackJoystick.Input.magnitude == 0)
        {
            return;
        }
        if (Time.time - startAttackTime > status.CoolTime)
        {
            animator.SetTrigger(attackTrigger);
            startAttackTime = Time.time;
        }
    }

    public void OnColliderEnable()
    {
        attackArea.enabled = true;
    }

    public void OnColliderDisable()
    {
        attackArea.enabled = false;
    }

    public float tempSpeed;
    public override void Move()
    {
        isMoving = moveJoystick.Input.magnitude != 0;
        moveDirection = moveJoystick.Input;
        rb.velocity = moveDirection * status.MovementSpeed;
        animator.SetBool(moveBool, isMoving);
    }


    public override void OnDamage(float damage)
    {
        status.Health -= damage - status.Defense;
        if (status.Health <= 0f)
        {
            OnDie();
            return;
        }
        animator.SetTrigger(damageTrigger);
    }

    public override void OnDie()
    {
        isDie = true;
        gameManager.OnGameOver();
        rb.velocity = Vector2.zero;
        animator.SetTrigger(deathTrigger);
        animator.SetBool(dieBool, isDie);
    }

    public void StartGame(string name)
    {
        status.SetStatus(name);
        var path = string.Format(PathFormats.prefabs, name);
        prefab = (GameObject)Instantiate(Resources.Load(path), transform.position, transform.rotation);
        prefab.transform.SetParent(transform, false);
        prefab.transform.position = Vector3.zero;
        prefab.layer = LayerMask.NameToLayer(Tags.Player);
        ChangeLayerRecursively(gameObject, prefab.layer);
        GetComponent<PlayerEquipment>().UpdateStatusText();
    }
    public void StatusBasedSetting()
    {
        attackArea.size = new Vector2(status.Range, attackArea.size.y);
        attackArea.offset = new Vector2(status.Range * -0.5f, attackArea.offset.y);
        StopCoroutine(AutoHeal());
        StartCoroutine(AutoHeal());
    }
    public IEnumerator AutoHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            status.Health += status.Intelligence * 0.2f;
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        attackArea = GetComponent<BoxCollider2D>();
        playerEquip = GetComponent<PlayerEquipment>();

        StartGame("Warrior");
        StatusBasedSetting();
        SpriteRenderer[] AllSpriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer trans in AllSpriteRenderers)
        {
            if (trans.name == "R_Weapon")
            {
                weaponRenderer = trans;
                break;
            }
        }
    }

    private void Start()
    {
        isDie = false;
        Animator[] AllAnimators = gameObject.GetComponentsInChildren<Animator>();
        foreach (Animator trans in AllAnimators)
        {
            if (trans.name == "UnitRoot")
            {
                animator = trans;
                break;
            }
        }
    }
    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie)
            return;

        if (collision.CompareTag(Tags.Monster) && !collision.isTrigger)
        {
            var monster = collision.GetComponent<Monster>();

            if (monster != null && !monster.isDie)
            {
                monster.OnDamage(status.CriticalChance <= Random.Range(0f, 100f) ? status.Strength * (status.Critical * 0.3f) : status.Strength);
            }
        }
    }

    private void Update()
    {
        if (isDie)
            return;
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.S))
            OnDie();
#endif
        Attack();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
            OnColliderEnable();
        else
            OnColliderDisable();
    }

    private void FixedUpdate()
    {
        if (isDie)
            return;
        Rotation();
        Move();
    }
}
