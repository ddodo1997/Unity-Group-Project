using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemData itemData;
    private Sprite sprite;
    private Image itemImage; 
    private Color tempColor = Color.white;
    private Color normalColor = Color.white;
    // Start is called before the first frame update
    private void Awake()
    {
        itemData = new ItemData();
    }
    private void Start()
    {
        sprite = GetComponent<Sprite>();
        itemImage = GetComponentsInChildren<Image>()[1];
        tempColor.a = 0f;
    }

    private void Update()
    {
         if(itemData.IsEmpty)
            itemImage.color = tempColor;
         else
            itemImage.color = normalColor;
    }

    public void OnSlotTouch()
    {
        //아이템 슬롯 터치 시
        if (itemData.IsEmpty)
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
