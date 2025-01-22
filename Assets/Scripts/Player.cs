using System.Collections;
using System.Collections.Generic;
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
    private BoxCollider2D col;
    private GameObject prefab;
    public Animator animator;
    public SpriteRenderer weaponRenderer;
    private Vector2 moveDirection;
    private Vector2 lookDirection;
    private float startAttackTime;
    private bool isMoving = false;

    public override void Attack()
    {
        if (attackJoystick.Input.magnitude == 0)
        {
            return;
        }
        if (attackJoystick.Input.x < 0)
        {
            lookDirection = attackJoystick.Input;
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(0, 1) * Mathf.Rad2Deg, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(0, -1) * Mathf.Rad2Deg, 0);
        }

        if(Time.time - startAttackTime > status.CoolTime)
        {
            animator.SetTrigger(attackTrigger);
            startAttackTime = Time.time;
        }

    }


    public override void Move()
    {
        isMoving = moveJoystick.Input.magnitude != 0;
        moveDirection = moveJoystick.Input;
        rb.velocity = moveDirection * status.Agility;
        animator.SetBool(moveBool, isMoving);
    }

    public override void OnDamage(float damage)
    {
        animator.SetTrigger(damageTrigger);
    }

    public override void OnDie()
    {
        animator.SetTrigger(deathTrigger);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        status.SetStatus("TEST");
        col.size = new Vector2(status.Range, col.size.y);

        prefab = (GameObject)Instantiate( Resources.Load("Wizard"), transform.position, transform.rotation);
        prefab.transform.SetParent(transform, false);

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

    private void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }
}
