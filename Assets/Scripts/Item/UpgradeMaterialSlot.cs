using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMaterialSlot : MonoBehaviour
{
    public ItemData itemData;
    private Image image;
    public InventoryManager inventoryManager;
    public UpgradeManager upgradeManager;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void AddMaterialItem(ref ItemData item)
    {
        SetData(ref item);
    }
    public void RemoveMaterialItem()
    {
        if (itemData.IsEmpty)
            return;

        inventoryManager.items.Add(itemData);
        inventoryManager.Sorting(inventoryManager.currentSortBy);
        SetData();
    }
    public void SetData(ref ItemData item)
    {
        itemData = item;
        image.sprite = itemData?.sprite;
    }
    public void SetData()
    {
        itemData = new ItemData();
        image.sprite = null;
    }
}
