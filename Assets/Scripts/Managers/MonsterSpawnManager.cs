using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    public GameManager gameManager;
    public List<Spawner> spawners;
    public EliteSpawn eliteSpawn;
    public BossSpawn bossSpawn;
    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.Spawn(gameManager.currentStage);
        }
        if (gameManager.currentStage % 2 != 0)
            eliteSpawn.Spawn(gameManager.currentStage);
        else
            bossSpawn.Spawn(gameManager.currentStage);

    }

}
