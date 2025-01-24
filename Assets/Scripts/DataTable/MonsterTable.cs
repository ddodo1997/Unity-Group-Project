using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;

public class MonsterTable : DataTable
{

    private readonly Dictionary<string, MonsterStatus> dictionary = new Dictionary<string, MonsterStatus>();

    public override void Load(string fileName)
    {
        var path = string.Format(PathFormats.tables, fileName);

        var textAsset = Resources.Load<TextAsset>(path);

        var list = LoadCSV<MonsterStatus>(textAsset.text);

        dictionary.Clear();
        foreach (var monster in list)
        {
            if (!dictionary.ContainsKey(monster.Id))
            {
                dictionary.Add(monster.Id, monster);
            }
            else
            {
                Debug.Log($"{monster.Id} is ม฿บน!");
            }
        }
    }

    public MonsterStatus Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            Debug.LogError($"{this} Get Error{key}");
            return null;
        }
        return dictionary[key];
    }
}
