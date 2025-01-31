using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;

public class WeaponTable : DataTable
{
    private readonly Dictionary<string, WeaponData> dictionary = new Dictionary<string, WeaponData>();

    public override void Load(string fileName)
    {
        var path = string.Format(PathFormats.tables, fileName);

        var textAsset = Resources.Load<TextAsset>(path);

        var list = LoadCSV<WeaponData>(textAsset.text);  //씨1발 도대체 무슨일이 벌어진거야

        dictionary.Clear();
        foreach (var weapon in list)
        {
            if (!dictionary.ContainsKey(weapon.Id))
            {
                dictionary.Add(weapon.Id, weapon);
            }
            else
            {
                Debug.Log($"{weapon.Id} is 중복!");
            }
        }
    }

    public WeaponData Get(string key)
    {
        if (!dictionary.ContainsKey(key))
        {
            Debug.LogError($"{this} Get Error{key}");
            return null;
        }
        return dictionary[key];
    }


    public List<WeaponData> GetList()
    {
        List<WeaponData> list = new List<WeaponData>();

        foreach (var armor in dictionary.Values)
        {
            list.Add(armor);
        }

        return list;
    }

    public List<WeaponData> GetList(WeaponType type)
    {
        var result = from l in GetList()
                     where l.Type == type
                     select l;

        return result.ToList();
    }

    public List<WeaponData> GetList(EquipRate rate)
    {
        var result = from l in GetList()
                     where l.Rate == rate
                     select l;

        return result.ToList();
    }
}
