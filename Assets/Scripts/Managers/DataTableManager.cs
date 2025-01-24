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
    static DataTableManager()
    {
#if !UNITY_EDITOR
        foreach (var id in DataTableIds.EntityStatus)
        {
            var table = new CharacterTable();
            table.Load(id);
            tables.Add(id, table);
        }

        foreach (var id in DataTableIds.EntityStatus)
        {
            var table = new MonsterTable();
            table.Load(id);
            tables.Add(id, table);
        }
#else

        var characterTable = new CharacterTable();
        var characterTableId = DataTableIds.EntityStatus[(int)EntityStatus.Player];
        characterTable.Load(characterTableId);
        tables.Add(characterTableId, characterTable);

        var monsterTable = new CharacterTable();
        var monsterTableId = DataTableIds.EntityStatus[(int)EntityStatus.Monsters];
        monsterTable.Load(monsterTableId);
        tables.Add(monsterTableId, monsterTable);
#endif
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if(!tables.ContainsKey(id))
        {
            Debug.Log("테이블 없음");
            return null;
        }
        return tables[id] as T;
    }


}
