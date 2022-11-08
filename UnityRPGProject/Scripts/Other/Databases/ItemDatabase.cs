using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;



[CreateAssetMenu(fileName = "New Item Database", menuName = "Databases/Item Database", order = 1)]
public class ItemDatabase : Database<ItemWithAttributes>
{
    [ContextMenu("Populate Database")]
    public override void PopulateDatabase()
    {
        base.PopulateDatabase();
    }
}

public interface IDatabaseItem
{
    public string Id { get; }
}



/*public class ItemDatabase : ScriptableObject
 {
     public List<ItemWithAttributes> database;
 
     [ContextMenu("Populate Database")]
     public void PopulateDatabase()
     {
         database = Resources.LoadAll<ItemWithAttributes>("Items").ToList();
     }
 
     public ItemWithAttributes GetById(string id)
     {
         return database.Find(x => x.id == id);
     }
     
     public ItemCategory GetCategoryById(string id)
     {
         return default;
         // return database.Find(x => x.id == id);
     }
 }*/