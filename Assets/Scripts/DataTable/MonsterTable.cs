using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.Linq;
using Unity.VisualScripting;

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

    public List<MonsterStatus> GetList()
    {
        List<MonsterStatus > list = new List<MonsterStatus>();

        foreach (var monster in dictionary.Values)
        { 
            list.Add(monster); 
        }

        return list;
    }

    public List<MonsterStatus> GetList(int stage)
    {
        var result = from l in GetList()
                        where l.Stage == stage
                        select l;

        return result.ToList();
    }

    public List<MonsterStatus> GetBossList()
    {
        var result = from l in GetList()
                     where l.Rate == MonsterStatus.Rating.Boss
                     select l;

        return result.ToList();
    }

    public List<MonsterStatus> GetEliteList(int stage)
    {
        var result = from l in GetList()
                     where l.Stage == stage && l.Rate == MonsterStatus.Rating.Elite
                     select l;

        return result.ToList();
    }
}
