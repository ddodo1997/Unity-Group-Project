using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{

    public Player player;

    private void Update()
    {
        var newVec = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 5f);
        transform.position = newVec;
    }
}
