using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class FieldDropItem : MonoBehaviour
{
    public ItemData item;
    public InventoryManager inventory;
    private Rigidbody2D rb;
    private float yPos;
    private float speed = 300f;
    public float targetDistance;
    public Vector2 targetDirection;
    public Player player;
    public float pullMinDistance = 3f;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded = false;

    public void Setting(Vector3 position, ItemData item)
    {
        yPos = position.y;
        var path = string.Format(PathFormats.sprites, item.Id);
        this.item = item is EquipmentData ? item : item as WeaponData;
        item.sprite = Resources.Load<Sprite>(path);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
        item.IsEmpty = false;
        if (item is EquipmentData)
        {
            if ((item as EquipmentData).Type == ArmorType.Ring)
            {
                var vec = new Vector3(2, 2, 2);
                transform.localScale = vec;
            }
        }
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
            inventory.OnPickUpItem(ref item);
        }
    }
}
