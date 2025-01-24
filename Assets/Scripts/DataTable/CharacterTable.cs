using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;

public class CharacterTable : DataTable
{

    private readonly Dictionary<string, PlayerStatus> dictionary = new Dictionary<string, PlayerStatus>();

    public override void Load(string fileName)
    {
        var path = string.Format(PathFormats.tables, fileName);

        var textAsset = Resources.Load<TextAsset>(path);

        var list = LoadCSV<PlayerStatus>(textAsset.text);

        dictionary.Clear();
        foreach (var item in list)
        {
            if (!dictionary.ContainsKey(item.Id))
            {
                dictionary.Add(item.Id, item);
            }
            else
            {
                Debug.Log($"{item.Id} is ม฿บน!");
            }
        }
    }

    public PlayerStatus Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            Debug.LogError($"{this} Get Error{key}");
            return null;
        }
        return dictionary[key];
    }
}
