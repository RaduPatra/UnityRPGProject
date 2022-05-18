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
    public UIManager uiManager;

    private void Start()
    {
        SetupUISlots();
        equipmentInventory.inventoryChanged += SetupUISlots;
    }

    private void SetupUISlots()
    {
        uiManager = GetComponentInParent<UIManager>();

        equipmentSlots = itemsParent.GetComponentsInChildren<EquipmentSlotUI>();
        var index = 0;

        foreach (var equipmentSlot in equipmentSlots)
        {
            var cat = equipmentSlot.slotCategory;
            var slot = equipmentInventory.equipmentSlots[cat];
            equipmentSlot.InventorySlot = slot;
        }

        /*foreach (var equipment in equipmentInventory.equipmentArmorSlots)
        {
            if (equipmentSlots[index].slotCategory == equipment.Key)
            {
                var slot = equipment.Value;
                equipmentSlots[index].InventorySlot = slot;
                // slot.OnSlotChanged?.Invoke(slot);
            }

            index++;
        }*/
    }

    /*void UpdateSlotAtIndex(InventorySlot slot)
    {
        equipmentSlots[slot.slotIndex].UpdateUISlot(slot);
    }*/
}