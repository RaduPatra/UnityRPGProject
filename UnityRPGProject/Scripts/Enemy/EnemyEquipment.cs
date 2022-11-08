using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyEquipment : SerializedMonoBehaviour, IEquipment
{
    [SerializeField]
    private readonly Dictionary<ItemCategory, Transform> equipmentLocations = new Dictionary<ItemCategory, Transform>();
    public Dictionary<ItemCategory, Transform> EquipmentLocations => equipmentLocations;
    
    public Transform GetEquippedItemLocation(ItemCategory category)
    {
        if (!category) return null;
        // var equipLocation = equipmentLocations[equippedCategory];
        return !equipmentLocations.TryGetValue(category, out var equipLocation) ? null : equipLocation;
    }
    
    public ItemCategory FindEquippedCategory<T>(ItemWithAttributes item,
        IDictionary<ItemCategory, T> inventory)
    {
        var categories = item.GetCategoryAncestors();
        return categories.FirstOrDefault(inventory.ContainsKey);
    }
}