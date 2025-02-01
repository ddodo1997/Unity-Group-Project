using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemData itemData;
    public Image image;
    // Start is called before the first frame update
    private void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnSlotTouch()
    {
        //��� ���� ��ġ ��
        if (itemData.IsEmpty)
        {
            return;
        }
        inventoryManager.items.Add(itemData);
        SetData(null);
        inventoryManager.Sorting(inventoryManager.currentSortBy);
    }

    public void SetData(ItemData itemData)
    {
        this.itemData = itemData ?? new ItemData();
        image.sprite = itemData?.sprite ?? null;
    }
}
