using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public WeaponData item;
    public Player player;
    

    public void OnButtonTouch()
    {
        player.weaponRenderer.sprite = item.sprite;
    }
}
