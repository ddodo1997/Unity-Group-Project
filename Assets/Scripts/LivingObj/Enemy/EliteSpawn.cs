using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EliteSpawn : MonoBehaviour
{
    public GameObject prefab;
    public void Spawn(int stage)
    {
        var monsterStatue = DataTableManager.MonsterTable.GetEliteList(stage);
        var newMonsterStatus = monsterStatue.GetNewData();
        var monster = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<EliteMonster>();
        monster.SettingMonster(newMonsterStatus);
        monster.tag = Tags.Elite;
    }
}
