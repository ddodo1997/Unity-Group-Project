using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            player = collision.GetComponent<Player>();
            player.isAggroAble = false;
            StartCoroutine(FullHeal());
        }

        if (collision.CompareTag(Tags.Monster))
        {
            var monster = collision.GetComponent<Monster>();
            monster.GoBack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            player = collision.GetComponent<Player>();
            player.isAggroAble = true;
            StopAllCoroutines();
        }
    }
    private IEnumerator FullHeal()
    {
        yield return new WaitForSeconds(5f);

        player.status.hp = player.status.Health;
        player.playerEquip.hpBar.UpdateHpBar(player.status);
    }
}
