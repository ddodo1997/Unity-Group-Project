using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    private Player player;
    private PlayerEquipment playerEquipment;
    public InventoryManager inventoryManager;
    public ItemData itemData;
    public Image image;
    // Start is called before the first frame update
    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        player = GameObject.FindWithTag(Tags.Player).GetComponent<Player>();
        playerEquipment = GameObject.FindWithTag(Tags.Player).GetComponent<PlayerEquipment>();
    }
    public void OnSlotTouch()
    {
        //장비 슬롯 터치 시
        if (itemData.IsEmpty)
        {
            return;
        }
        inventoryManager.items.Add(itemData); 
        if (itemData is WeaponData)
        {
            player.weaponRenderer.sprite = null;
        }
        SetData();
        inventoryManager.Sorting(inventoryManager.currentSortBy);
        player.status.SetStatus(ref playerEquipment.equipSlots);
        playerEquipment.UpdateStatusText();
        playerEquipment.UpdateSkill();

        playerEquipment.hpBar.UpdateHpBar(player.status);
    }

    public void SetData(ref ItemData itemData)
    {
        this.itemData = itemData ?? new ItemData();
        image.sprite = itemData?.sprite ?? null;
    }

    public void SetData()
    {
        itemData = new ItemData();
        image.sprite = null;
    }
}
