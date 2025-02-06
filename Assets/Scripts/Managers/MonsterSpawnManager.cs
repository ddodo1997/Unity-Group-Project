using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    private int currentStage = 1;
    public List<Spawner> spawners;
    public EliteSpawn eliteSpawn;
    private void Start()
    {
        Spawn();
        gameManager = GameObject.FindGameObjectWithTag(Tags.GameManager).GetComponent<GameManager>();
    }

    private void Spawn()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.Spawn(currentStage);
        }
        eliteSpawn.Spawn(currentStage);
    }

}
