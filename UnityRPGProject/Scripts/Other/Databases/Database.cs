using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Database<T> : SerializedScriptableObject where T : ScriptableObject, IDatabaseItem
{
    // public List<T> database;
    // [DictionaryDrawerSettings(IsReadOnly = true)]
    public string path = "Items";
    [Sirenix.OdinInspector.ReadOnly]
    public readonly Dictionary<string, T> database = new Dictionary<string, T>();
    public T GetById(string id)
    {
        database.TryGetValue(id, out var item);
        return item;
        // return database.Find(x => x.Id == id);
        
    }
    
    [ContextMenu("Populate Database")]
    public virtual void PopulateDatabase()
    {
        var items = Resources.LoadAll<T>(path).ToList();
        database.Clear();
        foreach (var item in items)
        {
            database.Add(item.Id, item);
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
        
    }
}