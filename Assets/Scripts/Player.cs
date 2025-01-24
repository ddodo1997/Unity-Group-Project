using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{
    public VirtualJoyStick moveJoystick;
    public VirtualJoyStick attackJoystick;
    public PlayerStatus status = new PlayerStatus();

    private Rigidbody2D rb;
    public BoxCollider2D attackArea;
    private CircleCollider2D hitBox;
    private GameObject prefab;
    public Animator animator;
    public SpriteRenderer weaponRenderer;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    private float startAttackTime;
    private bool isMoving = false;
    public bool isDie = false;

    public void Rotation()
    {
        if (attackJoystick.Input.magnitude != 0)
        {
            if (attackJoystick.Input.x < 0)
            {
                lookDirection = attackJoystick.Input;
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(0, 1) * Mathf.Rad2Deg, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(0, -1) * Mathf.Rad2Deg, 0);
            }
        }
        else if (moveJoystick.Input.magnitude != 0) {
            if (moveJoystick.Input.x < 0)
            {
                lookDirection = moveJoystick.Input;
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(0, 1) * Mathf.Rad2Deg, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, Mathf.Atan2(0, -1) * Mathf.Rad2Deg, 0);
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


    public override void Move()
    {
        isMoving = moveJoystick.Input.magnitude != 0;
        moveDirection = moveJoystick.Input;
        rb.velocity = moveDirection * status.MovementSpeed;
        animator.SetBool(moveBool, isMoving);
    }


    public override void OnDamage(float damage)
    {
        status.Health -= damage;
        Debug.Log(status.Health);
        if (status.Health < 0f)
        {
            OnDie();
            return;
        }
        animator.SetTrigger(damageTrigger);
    }

    public override void OnDie()
    {
        isDie = true;
        animator.SetTrigger(deathTrigger);
        animator.SetBool(dieBool, isDie);
    }

    public void StartGame(string name)
    {
        status.SetStatus(name);
        var path = string.Format(PathFormats.prefabs, name);
        prefab = (GameObject)Instantiate( Resources.Load(path), transform.position, transform.rotation);
        prefab.transform.SetParent(transform, false);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackArea = GetComponent<BoxCollider2D>();

        StartGame("Warrior");


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDie)
            return;

        if (collision.CompareTag(Tags.Monster) && !collision.isTrigger)
        {
            Debug.Log("Attack");
            var monster = collision.GetComponent<Monster>();
            Debug.Log(monster.isDie);
            if (monster != null && !monster.isDie)
            {
                monster.OnDamage(status.Strength);
            }
        }
    }

    private void Update()
    {
        if (isDie)
            return;
        Attack();
        Rotation();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
            OnColliderEnable();
        else
            OnColliderDisable();
    }

    private void FixedUpdate()
    {
        if (isDie)
            return;
        Move();
    }
}
