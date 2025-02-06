using System.Collections;
using UnityEngine;
using static Cinemachine.CinemachineOrbitalTransposer;

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
    public bool isAggroAble = false;
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
        status.hp -= Mathf.Clamp(damage - status.Defense,0f, float.MaxValue);
        playerEquip.hpBar.UpdateHpBar(status);
        if (status.hp <= 0f)
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
        StopAllCoroutines();
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
    }
    public IEnumerator AutoHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            OnHeal((int)(status.Intelligence * 0.2f));
        }
    }

    public void OnHeal(float healling, bool particleOn = false)
    {
        status.hp += healling;
        if (status.hp >= status.Health)
        {
            status.hp = status.Health;
        }
        if (particleOn)
        {
            //파티클 효과 재생
        }
        playerEquip.hpBar.UpdateHpBar(status);
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
        StartCoroutine(AutoHeal());
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
                monster.OnDamage(status.CriticalChance >= Random.Range(0f, 100f) ? status.Strength * (status.Critical * 0.3f) : status.Strength);
            }
        }

        if (collision.CompareTag(Tags.Elite) && !collision.isTrigger)
        {
            var monster = collision.GetComponent<EliteMonster>();

            if (monster != null && !monster.isDie)
            {
                monster.OnDamage(status.CriticalChance >= Random.Range(0f, 100f) ? status.Strength * (status.Critical * 0.3f) : status.Strength);
            }
        }
        if (collision.CompareTag(Tags.Boss) && !collision.isTrigger)
        {
            var monster = collision.GetComponent<BossMonster>();

            if (monster != null && !monster.isDie)
            {
                monster.OnDamage(status.CriticalChance >= Random.Range(0f, 100f) ? status.Strength * (status.Critical * 0.3f) : status.Strength);
            }
        }

        if (collision.CompareTag(Tags.HeallingWell))
        {
            var well = collision.GetComponent<HeallingWell>();
            if(well != null)
            {
                well.OnDamage();
            }
        }


        if(collision.CompareTag(Tags.ItemBox))
        {
            var box = collision.GetComponent<ItemBox>();
            if(box != null)
            {
                box.OnDamage();
            }
        }
    }

    private void Update()
    {
        if (isDie)
            return;
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.S))
            OnDamage(300);
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
