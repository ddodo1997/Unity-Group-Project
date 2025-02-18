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
    [SerializeField]
    private Sprite normalSprite;
    private void Start()
    {
        image = GetComponent<Image>();
        normalSprite = GetComponent<Image>().sprite;
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
        upgradeManager.UpdateMaterialText();
    }
    public void SetData(ref ItemData item)
    {
        itemData = item;
        image.sprite = itemData?.sprite ?? normalSprite;
    }
    public void SetData()
    {
        itemData = new ItemData();
        image.sprite = normalSprite;
    }
}
