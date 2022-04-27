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
    [SerializeField] public EquipmentInventory equipmentInventory;
    public ItemEventChannel itemDropEventChannel;
    public ItemEventChannel itemPickupEventChannel;
    private EquipmentSlotUI[] equipmentSlots;
    public AttributeBaseSO iconAttribute;

    private void Start()
    {
        SetupUISlots();
    }

    private void SetupUISlots()
    {
        equipmentSlots = itemsParent.GetComponentsInChildren<EquipmentSlotUI>();
        var index = 0;
        foreach (var equipment in equipmentInventory.equipmentArmorSlots)
        {
            if (equipmentSlots[index].slotCategory == equipment.Key)
            {
                var slot = equipment.Value;
                equipmentSlots[index].InventorySlot = slot;
                // slot.OnSlotChanged?.Invoke(slot);
            }

            index++;
        }
    }

    /*void UpdateSlotAtIndex(InventorySlot slot)
    {
        equipmentSlots[slot.slotIndex].UpdateUISlot(slot);
    }*/
}