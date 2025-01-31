using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{

    public Player player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
