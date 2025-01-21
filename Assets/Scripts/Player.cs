using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IEntity
{
    public VirtualJoyStick moveJoystick;
    public VirtualJoyStick attackJoystick;
    private Rigidbody2D rb;

    [SerializeField]
    private Vector2 moveDirection;
    [SerializeField]
    private Vector2 lookDirection;

    public float HP { get; set; }
    public float Speed { get; set; }

    public void Attack()
    {
        if (attackJoystick.Input.magnitude == 0)
            return;
        lookDirection = attackJoystick.Input;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg);
    }

    public void Move()
    {
        moveDirection = new Vector2(moveJoystick.Input.x, moveJoystick.Input.y);
        rb.velocity = moveDirection * Speed;
    }

    public void OnDamage(float damage)
    {
    }

    public void OnDie()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Speed = 5f;
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
