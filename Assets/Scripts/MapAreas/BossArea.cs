using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArea : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Monster))
        {
            var monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.GoBack();
            }
        }

        if (collision.CompareTag(Tags.Player))
        {
            var boss = GameObject.FindGameObjectWithTag(Tags.Boss)?.GetComponent<BossMonster>() ?? null;
            var player = collision.GetComponent<Player>();
            if (player != null && boss != null)
            {
                boss.SetStatus(BossMonster.Status.Aggro);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player) && !collision.isTrigger)
        {
            var bossMonster = GameObject.FindGameObjectWithTag(Tags.Boss)?.GetComponent<BossMonster>() ?? null;
            if (bossMonster != null)
                bossMonster.SetStatus(BossMonster.Status.Wait);
        }
    }
}
