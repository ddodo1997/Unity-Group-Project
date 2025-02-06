using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly int[] levels = {10,20,30,40,50};
    public GameObject prefab;
    private BoxCollider2D spawnArea;
    private readonly int monsterCntPerArea = 6;
    public BoxCollider2D SpawnArea
    {
        get => spawnArea;
        set => spawnArea = value;
    }

    public float radius;

    private void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        radius = spawnArea.size.x * 2;
    }

    public void Spawn(int stage)
    {
        for (int i = 0; i < monsterCntPerArea; i++)
        {
            var spawnPos = (Vector2)transform.position + Random.insideUnitCircle * radius;
            if (Physics2D.OverlapPoint(spawnPos, LayerMask.GetMask(Layers.SafeArea)) || Physics2D.OverlapPoint(spawnPos, LayerMask.GetMask(Layers.BossArea)))
            {
                i--;
                continue;
            }
            var monsterList = DataTableManager.MonsterTable.GetListWithStage(stage);
            var monsterStatus = monsterList[Random.Range(0, monsterList.Count)].GetNewData();
            var monster = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<Monster>();
            monster.SettingMonster(monsterStatus);
        }
    }
}
