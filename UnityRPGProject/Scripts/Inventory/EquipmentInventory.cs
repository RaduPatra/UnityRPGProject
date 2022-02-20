using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/Equipment", order = 2)]
public class EquipmentInventory : SerializedScriptableObject
{
    public readonly Dictionary<ItemType, InventorySlot> equipmentArmorSlots = new Dictionary<ItemType, InventorySlot>();

    public readonly Dictionary<ItemType, ItemStack>
        equippedWeaponItems = new Dictionary<ItemType, ItemStack>();
    private void OnEnable()
    {
        foreach (var slot in equipmentArmorSlots)
        {
            equipmentArmorSlots[slot.Key].slotType = slot.Key;
        }
    }
}