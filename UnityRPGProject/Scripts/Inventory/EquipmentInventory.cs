using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/EquipmentLazyTest", order = 1)]
public class EquipmentInventory : SerializedScriptableObject
{
    [OdinSerialize] [NonSerialized] public LazyValue<Dictionary<ItemCategory, InventorySlot>> equipmentSlots;

    public Dictionary<ItemCategory, InventorySlot>
        DefaultEquipmentSlots = new Dictionary<ItemCategory, InventorySlot>();

    [ContextMenu("Load item ids")]
    public void LoadItemIds()
    {
        foreach (var invItem in DefaultEquipmentSlots)
        {
            var slot = invItem.Value;
            if (slot.itemStack != null)
            {
                if (slot.itemStack.item != null)
                {
                    slot.itemStack.itemId = slot.itemStack.item.Id;
                }
            }
        }
    }

    [ContextMenu("refresh asset test")]
    public void RefreshAssetTest()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
        // AssetDatabase.Refresh();
    }
}