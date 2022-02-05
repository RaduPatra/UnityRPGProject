using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/Equipment", order = 2)]
public class EquipmentInventory : SerializedScriptableObject
{
    [SerializeField]
    private Dictionary<ItemType, InventorySlot> equipmentSlots = new Dictionary<ItemType, InventorySlot>();
    public Dictionary<ItemType, InventorySlot> EquipmentSlots => equipmentSlots;
    
    public readonly Dictionary<ItemType, ItemStack>
        equippedActionItems = new Dictionary<ItemType, ItemStack>();
    
    
    /*public readonly Dictionary<ItemType, InventorySlot>
        equippedActionSlots = new Dictionary<ItemType, InventorySlot>();

    public void SlotChanged(InventorySlot slot)
    {
        foreach (var actionSlot in equippedActionSlots)
        {
            if (actionSlot.Value != slot) continue;
            equippedActionSlots[actionSlot.Key] = slot;
            return;
        }
    }*/
}