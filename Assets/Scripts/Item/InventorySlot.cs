using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemData itemData;
    private Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Sprite>();
    }

    public void OnSlotTouch()
    {
        //아이템 슬롯 터치 시
        if (itemData is null)
            inventoryManager.SetCurrentItem();
        else
        {
            inventoryManager.SetCurrentItem(ref itemData);
        }
    }

    public void SetData(ref ItemData itemData)
    {
        this.itemData = itemData;
    }
}
