using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public PortalController controller;
    public Player player;
    public GameObject portalUiButton;
    public PortalButton portalButton;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (portalUiButton != null)
            if (collision.CompareTag(Tags.Player))
            {
                portalUiButton.SetActive(true);
                portalButton.GetComponent<Image>().color = Color.blue;
                controller.currentActivePortal = this;
                //StartCoroutine(LateTrigger());
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (portalUiButton != null)
            if (collision.CompareTag(Tags.Player)/* && !warpFlag*/)
            {
                if(controller.currentActivePortal == this)
                    portalUiButton.SetActive(false);
                portalButton.GetComponent<Image>().color = Color.white;
            }
    }
}
