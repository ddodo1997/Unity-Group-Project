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
            var boss = GameObject.FindGameObjectWithTag(Tags.Boss).GetComponent<Monster>();
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                boss.SetStatus(Monster.Status.Aggro);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Boss))
        {
            collision.GetComponent<Monster>().GoBack();
        }
    }
}
