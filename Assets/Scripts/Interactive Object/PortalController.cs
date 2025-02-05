using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Portal currentActivePortal;
    private bool isPortalUiOpen = false;
    public void OnPortalButtonTouch()
    {
        isPortalUiOpen = !isPortalUiOpen;
        gameObject.SetActive(isPortalUiOpen);
    }
}
