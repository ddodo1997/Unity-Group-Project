using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;
    private bool isOpen = false;
    public void OnInventoryOpenButtonTouch()
    {
        isOpen = !isOpen;
        inventory.SetActive(isOpen);
    }
}
