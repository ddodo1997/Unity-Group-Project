using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;

public class ArmorTable : DataTable
{
    private readonly Dictionary<string, EquipmentData> dictionary = new Dictionary<string, EquipmentData>();

    public override void Load(string fileName)
    {
        var path = string.Format(PathFormats.tables, fileName);

        var textAsset = Resources.Load<TextAsset>(path);

        var list = LoadCSV<EquipmentData>(textAsset.text);  //��1�� ����ü �������� �������ž�

        dictionary.Clear();
        foreach (var armor in list)
        {
            if (!dictionary.ContainsKey(armor.Id))
            {
                dictionary.Add(armor.Id, armor);
            }
            else
            {
                Debug.Log($"{armor.Id} is �ߺ�!");
            }
        }
    }

    public EquipmentData Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            Debug.LogError($"{this} Get Error{key}");
            return null;
        }
        return dictionary[key];
    }
}
