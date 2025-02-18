using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HeallingWell : InteractiveObject
{
    protected int hitCnt = 3;
    public bool isActive = true;
    protected float coolTime = 60f;
    public void OnDamage()
    {
        if (isActive)
        {
            StartCoroutine(ShakeCoroutine());
            hitCnt--;
            if (hitCnt == 0)
            {
                player.OnHeal(player.status.Health * 0.2f, true);
                StartCoroutine(CoolDown());
            }
        }
    }

    private IEnumerator CoolDown()
    {
        isActive = false;
        GetComponent<SpriteRenderer>().color = Color.gray;
        //yield return new WaitForSeconds(coolTime);
        yield return new WaitForSeconds(3f);
        hitCnt = 3;
        isActive = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
