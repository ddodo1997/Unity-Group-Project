using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Text;
public enum SortBy
{
    Armor,
    Weapon,
}
public class InventoryManager : MonoBehaviour
{
    public GameObject itemInfoPanel;
    public GameObject itemInfoPadding;
    public Image currentItemImage;
    public TextMeshProUGUI currentItemText;
    public UpgradeManager upgradeManager;
    public GameObject inventoryPanel;
    public GameObject inventoryBackGround;
    public GameObject UpgradeWindow;
    public GameObject EquipmentWindow;
    public Button equipButton;
    private bool isOpen = false;
    public bool isUpgrade = false;
    public static readonly int maxItemSlot = 60;
    public List<ItemData> items = new List<ItemData>(maxItemSlot);
    public List<InventorySlot> slots = new List<InventorySlot>(maxItemSlot);
    public ItemData currentItem;
    public Player player;

    public SortBy currentSortBy = SortBy.Armor;

    public TextMeshProUGUI slotCntText;

    public int ItemCount()
    {
        int result = 0;
        foreach (var item in items)
        {
            if (item == null || item.IsEmpty)
                break;
            result++;
        }

        return result;
    }

    public void OnInventoryOpenButtonTouch()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
        inventoryBackGround.SetActive(isOpen);
        Time.timeScale = isOpen ? 0.0f : 1.0f;
        if (isUpgrade)
            OnUpgradeButtonTouch();
        SortingInventory();
        SetCurrentItem();
        slotCntText.text = $"{ItemCount()} / {slots.Count()}";
    }
    public string SetcurrentItemText()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{currentItem.Name}");

        if (currentItem is WeaponData)
            stringBuilder.AppendLine($"{(currentItem as WeaponData).Type}");
        else if (currentItem is EquipmentData)
            stringBuilder.AppendLine($"{(currentItem as EquipmentData).Type}");

        stringBuilder.AppendLine($"{currentItem.Rate}");
        stringBuilder.AppendLine($"Level : {currentItem.Level}");
        stringBuilder.AppendLine($"EXP : {currentItem.currentExp} / {currentItem.LevelUpExperienceRequired}");
        

        if (currentItem.Strength != 0)
            stringBuilder.AppendLine($"Strength + {currentItem.Strength}");
        if (currentItem.Agility != 0)
            stringBuilder.AppendLine($"Agility + {currentItem.Agility}");
        if (currentItem.Health != 0)
            stringBuilder.AppendLine($"Health + {currentItem.Health}");
        if (currentItem.Intelligence != 0)
            stringBuilder.AppendLine($"Intelligence + {currentItem.Intelligence}");
        if (currentItem.Luck != 0)
            stringBuilder.AppendLine($"Luck + {currentItem.Luck}");
        if (currentItem.Critical != 0)
            stringBuilder.AppendLine($"Critical + {currentItem.Critical}");

        return stringBuilder.ToString();
    }
    public void SetCurrentItem(ref ItemData item)
    {
        currentItem = item;
        currentItemImage.sprite = currentItem.sprite;
        currentItemText.text = SetcurrentItemText();

        itemInfoPanel.SetActive(true);
        itemInfoPadding.SetActive(false);
    }
    public void SetCurrentItem()
    {
        currentItem = new ItemData();
        itemInfoPanel.SetActive(false);
        itemInfoPadding.SetActive(true);
    }

    public void OnTouchEquipButton()
    {
        if (currentItem.IsEmpty)
            return;
        player.playerEquip.OnEquipItem(currentItem);
        SortingInventory();
        slotCntText.text = $"{ItemCount()} / {slots.Count()}";
    }

    public void OnUpgradeButtonTouch()
    {
        if (currentItem.IsEmpty && !isUpgrade)
            return;

        if (!upgradeManager.upgradeTargetItem.IsEmpty)
        {
            upgradeManager.OnCancle();
        }

        isUpgrade = !isUpgrade;
        UpgradeWindow.SetActive(isUpgrade);
        EquipmentWindow.SetActive(!isUpgrade);
        equipButton.enabled = !isUpgrade;
        upgradeManager.OnUpgreadeStart(currentItem);
        items.Remove(currentItem);
        SetCurrentItem();
        Sorting(currentSortBy);
    }

    public void OnPickUpItem(ref ItemData item)
    {
        items.Add(item);
        slots[items.Count - 1].SetData(ref item);
    }

    public void Sorting(SortBy sort)
    {
        switch (sort)
        {
            case SortBy.Armor:
                OnSortingArmor();
                break;
            case SortBy.Weapon:
                OnSortingWeapon();
                break;
        }
    }

    public void OnSortingWeapon()
    {
        var query = from item in items
                    where item is WeaponData
                    select item;

        currentSortBy = SortBy.Weapon;
        UpdateSlots(query.ToList());
    }

    public void OnSortingArmor()
    {
        var query = from item in items
                    where item.GetType() == typeof(EquipmentData)
                    select item;

        currentSortBy = SortBy.Armor;
        UpdateSlots(query.ToList());
    }

    public void UpdateSlots()
    {
        if (items.Count == 0)
        {
            ClearSlots();
            return;
        }
        int maxIdx = 0;
        for (int i = 0; i < items.Count; i++)
        {
            var image = slots[i].transform.GetChild(0).GetComponent<Image>();
            slots[i].itemData = items[i];
            image.sprite = items[i].sprite;
            maxIdx = i;
        }
        for (int i = maxIdx + 1; i < maxItemSlot; i++)
        {
            var image = slots[i].transform.GetChild(0).GetComponent<Image>();
            image.sprite = null;
            slots[i].itemData = null;
        }
    }
    public void UpdateSlots(List<ItemData> list)
    {
        if (list.Count == 0)
        {
            ClearSlots();
            return;
        }
        int maxIdx = 0;
        for (int i = 0; i < list.Count; i++)
        {
            var image = slots[i].transform.GetChild(0).GetComponent<Image>();
            slots[i].itemData = list[i];
            image.sprite = list[i].sprite;
            maxIdx = i;
        }
        for (int i = maxIdx + 1; i < maxItemSlot; i++)
        {
            var image = slots[i].transform.GetChild(0).GetComponent<Image>();
            image.sprite = null;
            slots[i].itemData = null;
        }
    }
    public void SortingInventory()
    {
        items.Sort(new InvenComparer());
        Sorting(currentSortBy);
        ClaerItems();
    }
    public void ClaerItems()
    {
        foreach (ItemData item in items)
        {
            if (item.IsEmpty)
                items.Remove(item);
        }
    }

    public void ClearSlots()
    {
        foreach (var item in slots)
        {
            item.itemData = null;
            item.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }
    }
}

public class InvenComparer : IComparer<ItemData>
{
    public int Compare(ItemData x, ItemData y)
    {
        return x.Rate.CompareTo(y.Rate);
    }
}