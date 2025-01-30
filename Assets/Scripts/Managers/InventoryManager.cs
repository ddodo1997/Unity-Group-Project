using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject inventoryBackGround;
    private bool isOpen = false;
    public static int maxItemSlot = 60;
    public List<ItemData> items = new List<ItemData>(maxItemSlot);
    public List<Button> slots = new List<Button>(maxItemSlot);
    public void OnInventoryOpenButtonTouch()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        inventoryBackGround.SetActive(isOpen);
        Time.timeScale = isOpen ? 0.0f : 1.0f;
    }

    public void OnPickUpItem(ItemData item)
    {
        items.Add(item);
    }

    public void OnSlotTouch()
    {
        //아이템 슬롯 터치 시



        UpdateSlots();
    }

    public void OnSortingWeapon()
    {
        var query = from item in items
                    where item.GetType() == typeof(WeaponData)
                    select item;

    }

    public void OnSortingArmor()
    {
        var query = from item in items
                    where item.GetType() == typeof(EquipmentData)
                    select item;

    }

    public void UpdateSlots()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var image = slots[i].transform.GetChild(0).GetComponent<Image>();
            image.sprite = items[i].sprite;
        }
    }


}
