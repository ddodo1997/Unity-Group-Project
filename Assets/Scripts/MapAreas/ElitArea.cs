using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteArea : MonoBehaviour
{
    private GameManager gameManager;
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag(Tags.GameManager).GetComponent<GameManager>();
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
            var elite = GameObject.FindGameObjectWithTag(Tags.Elite)?.GetComponent<EliteMonster>() ?? null;
            var player = collision.GetComponent<Player>();
            if (player != null && elite != null)
            {
                elite.SetStatus(EliteMonster.Status.Aggro);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player) && !collision.isTrigger)
        {
            var eliteMonster = GameObject.FindGameObjectWithTag(Tags.Elite)?.GetComponent<EliteMonster>() ?? null;
            if(eliteMonster != null)
                eliteMonster.SetStatus(EliteMonster.Status.Wait);
        }
    }
}
