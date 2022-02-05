using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] private Transform itemsParent;
    [SerializeField] private EquipmentInventory equipmentInventory;
    public ItemEventChannel itemDropEventChannel;
    public ItemEventChannel itemPickupEventChannel;
    // public ItemEventChannel itemEquipEventChannel;

    private void Awake()
    {
        //subscribe all uiSlots to the OnSlotAdded so each slot can update its own UI
        var uiSlots = itemsParent.GetComponentsInChildren<EquipmentSlotUI>();
        var index = 0;
        foreach (var equipment in equipmentInventory.EquipmentSlots)
        {
            if (uiSlots[index].slotItemType == equipment.Key)
            {
                var slot = equipment.Value;
                uiSlots[index].InventorySlot = slot;
                // slot.OnSlotChanged?.Invoke(slot);
            }
            
            index++;
        }
    }
}