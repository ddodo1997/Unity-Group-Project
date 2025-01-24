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
    static DataTableManager()
    {
#if UNITY_EDITOR
        foreach (var id in DataTableIds.EntityStatus)
        {
            var table = new CharacterTable();
            table.Load(id);
            tables.Add(id, table);
        }
#else
        var table = new CharacterTable();
        var characterTableId = DataTableIds.EntityStatus[(int)EntityStatus.Player];
        table.Load(characterTableId);
        tables.Add(characterTableId, table);
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
