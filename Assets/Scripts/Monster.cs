using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IAction
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
        Aggro
    };
    private Rigidbody2D rb;
    private CircleCollider2D circleCollider;

    public Type type;
    public Status currentStauts = Status.Idle;
    public Player player;
    public Vector2 direction;
    public float statusStartTime = 0f;
    public float maxIdleTime;
    public float maxMoveTime;
    public int minAggroLevel = 40;

    public MonsterStatus status = new MonsterStatus();
    public void Attack()
    {

    }
    public void Move()
    {
        rb.velocity = direction * status.Agility;
    }
    public void OnDamage(float damage)
    {

    }
    public void OnDie()
    {

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        status.SetStatus("TEST");
        circleCollider.radius = status.Distance;
        
        type = Type.Elite;
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
                break;
            case Status.Move:
                direction = Random.insideUnitCircle.normalized;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                maxMoveTime = Random.Range(1f, 3f);
                break;
            case Status.Aggro:
                direction = (player.transform.position - transform.position).normalized;
                break;
        }
    }
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        switch (currentStauts)
        {
            case Status.Idle:
                if(Time.time - statusStartTime > maxIdleTime)
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
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Move();
                rb.velocity *= 2f;
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (status.Level >= minAggroLevel && collision.CompareTag("Player"))
            SetStatus(Status.Aggro);
    }
}
