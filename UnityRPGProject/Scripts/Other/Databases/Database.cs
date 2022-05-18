using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Database<T> : SerializedScriptableObject where T : ScriptableObject, IDatabaseItem
{
    // public List<T> database;
    // [DictionaryDrawerSettings(IsReadOnly = true)]
    public string path = "Items";
    [Sirenix.OdinInspector.ReadOnly]
    public readonly Dictionary<string, T> database;
    public T GetById(string id)
    {
        database.TryGetValue(id, out var item);
        return item;
        // return database.Find(x => x.Id == id);
    }
}