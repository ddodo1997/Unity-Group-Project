using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
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

    private CircleCollider2D circleCollider;
    private Rigidbody2D rb;
    public GameObject player;
    
    private Status currentStauts = Status.Idle;
    private Vector2 direction;
    private float statusStartTime = 0f;
    private float maxIdleTime;
    private float maxMoveTime;
    private float targetDistance;
    private readonly int minAggroLevel = 40;
    private bool isMoving = false;
    public int tempLevel;

    private MonsterStatus status = new MonsterStatus();
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
        animator.SetTrigger(damageTrigger);
    }

    public override void OnDie()
    {
        animator.SetTrigger(deathTrigger);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        status.SetStatus("TEST");
        circleCollider.radius = status.Distance;
        player = GameObject.FindGameObjectWithTag("Player");
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
        isMoving = rb.velocity.magnitude > 0;
    }

    private void FixedUpdate()
    {
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
                    Move();
                }
                break;
            case Status.Aggro:
                direction = (player.transform.position - transform.position).normalized;
                if (targetDistance > 3f)
                {
                    Move();
                }
                else
                {
                    SetStatus(Status.Attack);
                }
                break;
            case Status.Attack:
                if (targetDistance > 3f)
                    SetStatus(Status.Aggro);
                else
                    Attack();
                break;
        }

        if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(0, 1) * Mathf.Rad2Deg, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, Mathf.Atan2(0, -1) * Mathf.Rad2Deg, 0);
        }
        animator.SetBool(moveBool, isMoving);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tempLevel >= minAggroLevel && collision.CompareTag("Player"))
            SetStatus(Status.Aggro);
    }
}
