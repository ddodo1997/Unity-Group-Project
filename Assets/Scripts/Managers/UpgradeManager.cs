using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemData upgradeTargetItem;
    public Image image;
    public List<UpgradeMaterialSlot> materialSlots;
    public static readonly int maxMaterialCnt = 20;
    public TextMeshProUGUI targetItemName;
    public TextMeshProUGUI currentMaterialText;

    public void OnUpgreadeStart(ItemData item)
    {
        upgradeTargetItem = item;
        image.sprite = upgradeTargetItem.sprite;
    }

    public void OnUpgrade()
    {

    }
    public void OnCancle()
    {
        if (!inventoryManager.isUpgrade)
            return;
        foreach (var item in materialSlots)
        {
            if(!item.itemData.IsEmpty)
                inventoryManager.items.Add(item.itemData);
            item.SetData();
        }

        inventoryManager.items.Add(upgradeTargetItem);
        OnUpgreadeStart(new ItemData());
        inventoryManager.OnUpgradeButtonTouch();
        inventoryManager.Sorting(inventoryManager.currentSortBy);
    }

    public void AddMaterialItem()
    {
        if (!inventoryManager.isUpgrade || inventoryManager.currentItem.IsEmpty)
            return;
        for (int i = 0; i < materialSlots.Count; i++)
        {
            if (materialSlots[i].itemData.IsEmpty)
            {
                materialSlots[i].AddMaterialItem(ref inventoryManager.currentItem);
                inventoryManager.items.Remove(inventoryManager.currentItem);
                break;
            }
        }
        inventoryManager.SetCurrentItem();
        inventoryManager.Sorting(inventoryManager.currentSortBy);
        UpdateMaterialText();
    }

    public void RemoveMaterialItem()
    {

    }

    public void UpdateMaterialList()
    {

    }

    public void Sort()
    {
        for(int i = 0; i < materialSlots.Count; i++)
        {
            materialSlots[i].SetData(ref materialSlots[i].itemData);
        }
    }

    public void SettingTargetItem(ItemData item)
    {
        upgradeTargetItem = item;
        image.sprite = upgradeTargetItem.sprite;
    }

    public void UpdateMaterialText()
    {
        var query = from i in materialSlots
                    where !i.itemData.IsEmpty
                    select i;
        currentMaterialText.text = $"{query.ToList().Count} / {maxMaterialCnt}";
    }
}
