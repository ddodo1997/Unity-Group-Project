using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    public int currentStage = 1;
    public List<Spawner> spawners;

    private void Start()
    {
        //Spawn();
    }

    private void Spawn()
    {
        foreach(Spawner spawner in spawners)
        {
            
        }
    }

}
