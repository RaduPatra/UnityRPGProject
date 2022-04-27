using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/Equipment", order = 2)]
public class EquipmentInventory : InventoryBase
{
    public readonly Dictionary<ItemCategory, InventorySlot> equipmentArmorSlots = new Dictionary<ItemCategory, InventorySlot>();

    public readonly Dictionary<ItemCategory, ItemStack>
        equippedWeaponItems = new Dictionary<ItemCategory, ItemStack>();
    
    // public  Dictionary<ItemCategory, EquippedInfo>
    //     equippedWeaponsTest = new Dictionary<ItemCategory, EquippedInfo>();
    private void OnEnable()
    {
        foreach (var slot in equipmentArmorSlots)
        {
            // equipmentArmorSlots[slot.Key].slotType = slot.Key;
            equipmentArmorSlots[slot.Key].slotCategory = slot.Key;
        }
    }
}

public class EquippedInfo
{
    public ItemStack stack;
    public InventorySlot lastSlot;
}