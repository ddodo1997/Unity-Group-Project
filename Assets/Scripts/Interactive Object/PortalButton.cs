using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : MonoBehaviour
{
    public Portal portal;
    public Player player;
    public void OnButtonTouch()
    {
        player.transform.position = portal.transform.position;
    }
}
