using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static Cinemachine.DocumentationSortingAttribute;

public class FieldDropItem : MonoBehaviour
{
    public ItemData item;
    public InventoryManager inventory;
    private Rigidbody2D rb;
    private float yPos;
    private float speed = 150f;
    public float targetDistance;
    public Vector2 targetDirection;
    public Player player;
    public float pullMinDistance = 3f;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool pickupAble = false;

    public void Setting(Vector3 position, ItemData item)
    {
        yPos = position.y;
        var path = string.Format(PathFormats.sprites, item.Id);
        this.item = item is EquipmentData ? item : item as WeaponData;
        item.sprite = Resources.Load<Sprite>(path);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
        item.IsEmpty = false;
        this.item.currentExp = (int)Random.Range(0, this.item.ExperienceValue);
        this.item.Level = Random.Range(1, 50);
        this.item.SetStatusForLevel();
        if(item is WeaponData)
        {
            var weaponData = (WeaponData)item;
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

        if (pickupAble)
        {
            targetDirection = (player.transform.position - transform.position);
            targetDistance = targetDirection.magnitude;
            if (isGrounded && targetDistance <= pullMinDistance && inventory.items.Count < InventoryManager.maxItemSlot)
            {
                rb.AddForce(targetDirection * speed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            isGrounded = transform.position.y <= yPos - Random.Range(1f, 2.5f);
            if (isGrounded)
            {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                pickupAble = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player) && inventory.items.Count < InventoryManager.maxItemSlot && pickupAble)
        {
            Destroy(gameObject);
            inventory.OnPickUpItem(ref item);
            return;
        }
    }
}
