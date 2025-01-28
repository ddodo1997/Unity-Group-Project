using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataTableManager 
{
    private static Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();
    public static CharacterTable CharacterTable
    {
        get { return Get<CharacterTable>(DataTableIds.EntityStatus[(int)EntityStatus.Player]); }
    }

    public static MonsterTable MonsterTable
    {
        get => Get<MonsterTable>(DataTableIds.EntityStatus[(int)EntityStatus.Monsters]);
    }

    public static ArmorTable ArmorTable
    {
        get => Get<ArmorTable>(DataTableIds.ItemStatus[(int)EquipType.Armor]);
    }
    static DataTableManager()
    {
        var characterTable = new CharacterTable();
        var characterTableId = DataTableIds.EntityStatus[(int)EntityStatus.Player];
        characterTable.Load(characterTableId);
        tables.Add(characterTableId, characterTable);

        var monsterTable = new MonsterTable();
        var monsterTableId = DataTableIds.EntityStatus[(int)EntityStatus.Monsters];
        monsterTable.Load(monsterTableId);
        tables.Add(monsterTableId, monsterTable);

        //var armorTable = new ArmorTable();
        //var armorTableId = DataTableIds.ItemStatus[((int)EquipType.Armor)];
        //armorTable.Load(armorTableId);
        //tables.Add(armorTableId, armorTable);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if(!tables.ContainsKey(id))
        {
            Debug.Log("���̺� ����");
            return null;
        }
        return tables[id] as T;
    }


}
