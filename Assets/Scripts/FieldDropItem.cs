using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class FieldDropItem : ItemData
{

    public InventoryManager inventory;
    private Rigidbody2D rb;
    private float yPos;
    private float speed = 300f;
    public float targetDistance;
    public Vector2 targetDirection;
    public Player player;
    public float pullMinDistance = 3f;

    private bool isGrounded = false;

    public void Setting(Vector3 position)
    {
        yPos = position.y;
    }

    private void Start()
    {
        float randomAngle = Random.Range(60f, 120f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * speed);

        inventory = GameObject.FindGameObjectWithTag(Tags.InventoryManager).GetComponent<InventoryManager>();
    }
    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        }
        isGrounded = transform.position.y <= yPos - Random.Range(1f, 2.5f);
        if (isGrounded)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
        targetDirection = (player.transform.position - transform.position);
        targetDistance = targetDirection.magnitude;
        if (targetDistance <= pullMinDistance && inventory.items.Count < InventoryManager.maxItemSlot)
        {
            rb.AddForce(targetDirection * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player) && inventory.items.Count < InventoryManager.maxItemSlot)
        {
            Destroy(gameObject);
            inventory.OnPickUpItem(this);
        }
    }
}
