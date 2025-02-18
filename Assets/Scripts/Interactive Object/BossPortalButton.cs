using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortalButton : PortalButton
{
    public override void OnButtonTouch()
    {
        if (GameObject.FindGameObjectWithTag(Tags.Elite) == null && GameObject.FindGameObjectWithTag(Tags.Boss) == null)
            player.transform.position = portal.transform.position;
    }
}
