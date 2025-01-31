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
    public GameObject EnforceWindow;
    public GameObject EquipmentWindow;
    private bool isOpen = false;
    private bool isEnforce = false;
    public static readonly int maxItemSlot = 60;
    public List<ItemData> items = new List<ItemData>(maxItemSlot);
    public List<InventorySlot> slots = new List<InventorySlot>(maxItemSlot);
    private ItemData currentItem;
    public Player player;

    public void OnInventoryOpenButtonTouch()
    {
        if(EnforceWindow.activeSelf)
            OnEnforceButtonTouch();
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        inventoryBackGround.SetActive(isOpen);
        Time.timeScale = isOpen ? 0.0f : 1.0f;
        UpdateSlots();
    }

    public void SetCurrentItem(ItemData item)
    {
        currentItem = item;
    }

    public void OnTouchEquipButton()
    {
        if (currentItem is null)
        {
            Debug.Log("currentItem is null");
            return;
        }
        player.playerEquip.OnEquipItem(currentItem);
        SortingInventory();
    }

    public void ReturnInventory(ItemData item)
    {

    }

    public void OnEnforceButtonTouch()
    {
        isEnforce = !isEnforce;
        EnforceWindow.SetActive(isEnforce);
        EquipmentWindow.SetActive(!isEnforce);
    }

    public void OnPickUpItem(ItemData item)
    {
        items.Add(item);
        items[items.Count - 1] = item;
        slots[items.Count - 1].SetData(item is EquipmentData ? item : item as WeaponData);
    }


    public void OnSortingWeapon()
    {
        var query = from item in items
                    where item.GetType() == typeof(WeaponData)
                    select item;

        UpdateSlots(query.ToList());
    }

    public void OnSortingArmor()
    {
        var query = from item in items
                    where item.GetType() == typeof(EquipmentData)
                    select item;

        UpdateSlots(query.ToList());
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < items.Count; i++)
        {
            var image = slots[i].transform.GetChild(0).GetComponent<Image>();
            image.sprite = items[i].sprite;
        }
    }
    public void UpdateSlots(List<ItemData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var image = slots[i].transform.GetChild(0).GetComponent<Image>();
            image.sprite = list[i].sprite;
        }
    }
    public void SortingInventory()
    {
        items.Sort(new InvenComparer());
    }
}

public class InvenComparer : IComparer<ItemData>
{
    public int Compare(ItemData x, ItemData y)
    {
        return x.Rate.CompareTo(y.Rate);
    }
}