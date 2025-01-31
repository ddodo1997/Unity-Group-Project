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
        //���� ��ް� ������ ���� Ȯ�� �й�
        switch (status.Rate)
        {
            case MonsterStatus.Rating.Normal:
                
                break;
            case MonsterStatus.Rating.Elite:

                break;
            case MonsterStatus.Rating.Boss:

                break;
            default:
                break;
        }

        //�׽�Ʈ��
        for (int i = 0; i < Random.Range(0, Variables.MaxDropCnt); i++)
        {
            var temp = Instantiate(tempItem, startPos, Quaternion.identity);
            temp.Setting(startPos);
        }
    }
}
