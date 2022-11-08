using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Category Database", menuName = "Databases/Category Database", order = 1)]
public class CategoryDatabase : Database<ItemCategory>
{
    [ContextMenu("Populate Database")]
    public override void PopulateDatabase()
    {
        base.PopulateDatabase();
    }
}