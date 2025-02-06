using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : InteractiveObject
{
    protected int hitCnt = 5;
    private bool isOpen = false;
    public void OnDamage()
    {
        if (!isOpen)
        {
            StartCoroutine(ShakeCoroutine());
            hitCnt--;
            if (hitCnt == 0)
            {
                isOpen = true;
                GetComponent<ItemDrop>().DropOne();
                Destroy(gameObject);
            }
        }
    }
}
