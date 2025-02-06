using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject prefab;
    public void Spawn(int stage)
    {
        var monsterStatue = stage % 2 == 0 ? DataTableManager.MonsterTable.GetBossList(stage) : DataTableManager.MonsterTable.GetEliteList(stage);
        var newMonsterStatus = monsterStatue.GetNewData();
        var monster = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Monster>();
        monster.SettingMonster(newMonsterStatus);
        monster.tag = Tags.Boss;
        monster.hpBar.SetPosition(new Vector3(0, 3.5f, 0));
    }
}
