using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.U2D;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/Equipment", order = 1)]
public class EquipmentInventory : SerializedScriptableObject
{
    public  Dictionary<ItemCategory, InventorySlot> equipmentSlots = new Dictionary<ItemCategory, InventorySlot>();
    [NonSerialized]
    public Action inventoryChanged;
    private void OnEnable()
    {
        Setup();
    }

    public void Setup()
    {
        foreach (var slot in equipmentSlots)
        {
            equipmentSlots[slot.Key].slotCategory = slot.Key;
        }
    }


    [ContextMenu("Load item ids")]
    public void LoadItemIds()
    {
        foreach (var invItem in equipmentSlots)
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
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        // AssetDatabase.Refresh();
    }

}