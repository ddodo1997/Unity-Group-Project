using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEditor.Progress;

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
        //������ ���� ��ġ ��
        inventoryManager.SetCurrentItem(itemData is EquipmentData ? itemData : itemData as WeaponData);
    }

    public void SetData(ItemData itemData)
    {
        this.itemData = itemData;
    }
}
