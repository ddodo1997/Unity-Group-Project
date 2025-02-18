using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject prefab;
    public void Spawn(int stage)
    {
        var monsterStatue = DataTableManager.MonsterTable.GetBossList(stage);
        var newMonsterStatus = monsterStatue.GetNewData();
        var monster = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<BossMonster>();
        monster.SettingMonster(newMonsterStatus);
        monster.tag = Tags.Boss;
    }
}
