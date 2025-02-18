using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    public GameManager gameManager;
    public List<Spawner> spawners;
    public List<BoxSpawner> boxSpawners;
    public EliteSpawn eliteSpawn;
    public BossSpawn bossSpawn;
    public GameObject itemBox;

    private void Start()
    {
        Spawn();
        BoxSpawn();
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

    private void BoxSpawn()
    {
        int boxCnt = 0;
        while(true)
        {
            foreach(var area in boxSpawners)
            {
                if (boxCnt == 2)
                    return;
                if(Random.Range(0f,1f) * 100 < 50)
                {
                    area.Spawn();
                    boxCnt++;
                }
            }
        }
    }
}
