using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    private Monster monster;
    public FieldDropItem tempItem;
    private Vector3 startPos;

    private void Start()
    {
        monster = GetComponent<Monster>(); 
    }
    public void Drop(MonsterStatus status)
    {
        startPos = transform.position + Random.onUnitSphere;

        for (int i = 0; i < Random.Range(0, Variables.MaxDropCnt); i++)
        {
            var temp = Instantiate(tempItem, startPos, Quaternion.identity);
            var list = DataTableManager.ArmorTable.GetList();
            temp.Setting(startPos, list[Random.Range(0, list.Count)].GetNewData());
        }

        for (int i = 0; i < Random.Range(0, Variables.MaxDropCnt); i++)
        {
            var temp = Instantiate(tempItem, startPos, Quaternion.identity);
            var list = DataTableManager.WeaponTable.GetList();
            temp.Setting(startPos, list[Random.Range(0, list.Count)].GetNewData());
        }
    }
}
