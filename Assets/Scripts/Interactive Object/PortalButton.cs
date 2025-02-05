using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : MonoBehaviour
{
    public Portal portal;
    public Player player;
    public void OnButtonTouch()
    {
        //player.GetComponent<CircleCollider2D>().enabled = false;
        player.transform.position = portal.transform.position;
        //player.GetComponent<CircleCollider2D>().enabled = true;
    }
}
