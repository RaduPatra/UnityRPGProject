using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Category Database", menuName = "Databases/Category Database", order = 1)]
public class CategoryDatabase : Database<ItemCategory>
{
    [ContextMenu("Populate Database")]
    public void PopulateDatabase()
    {
        var items = Resources.LoadAll<ItemCategory>(path).ToList();
        database.Clear();
        foreach (var item in items)
        {
            database.Add(item.Id, item);
        }
    }
}