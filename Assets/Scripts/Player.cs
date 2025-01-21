using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IAction
{
    public VirtualJoyStick moveJoystick;
    public VirtualJoyStick attackJoystick;
    public PlayerStatus status = new PlayerStatus();
    public Animator animator;
    public GameObject Weapon;

    private static readonly int attackTrigger = Animator.StringToHash("2_Attack");
    private static readonly int damageTrigger = Animator.StringToHash("3_Damaged");
    private static readonly int deathTrigger = Animator.StringToHash("4_Death");
    private readonly string moveBool = "1_Move";

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Vector2 moveDirection;
    private Vector2 lookDirection;
    private bool attackAble = true;
    private bool isMoving = false;

    public void Attack()
    {
        if (attackJoystick.Input.magnitude == 0)
        {
            StopCoroutine(DelayAttack());
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
        if (attackAble)
            StartCoroutine(DelayAttack());
    }

    public IEnumerator DelayAttack()
    {
        attackAble = false;
        animator.SetTrigger(attackTrigger);

        yield return new WaitForSeconds(status.CoolTime);

        attackAble = true;
    }

    public void Move()
    {
        isMoving = moveJoystick.Input.magnitude != 0;
        moveDirection = new Vector2(moveJoystick.Input.x, moveJoystick.Input.y);
        rb.velocity = moveDirection * status.Agility;
        animator.SetBool(moveBool, isMoving);
    }

    public void OnDamage(float damage)
    {
        animator.SetTrigger(damageTrigger);
    }

    public void OnDie()
    {
        animator.SetTrigger(deathTrigger);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        status.SetStatus();
        col.size = new Vector2(status.Range, col.size.y);
    }

    // Update is called once per frame
    private void Update()
    {
        moveDirection = Vector2.zero;
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }
}
