using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentUI : MonoBehaviour, IInitializable
{
    [SerializeField] private Transform itemsParent;
    [SerializeField] public EquipmentInventory equipmentInventory;
    public ItemEventChannel itemDropEventChannel;
    public ItemEventChannel itemPickupEventChannel;
    private EquipmentSlotUI[] equipmentSlots;
    public AttributeBaseSO iconAttribute;
    public UIManager uiManager;

    public void Initialize()
    {
        SaveSystem.OnLoad += LoadEquipmentUI;
    }

    public void Destroy()
    {
        SaveSystem.OnLoad -= LoadEquipmentUI;
    }

    private void LoadEquipmentUI(SaveData obj)
    {
        SetupUISlots();
    }

    private void SetupUISlots()
    {
        uiManager = GetComponentInParent<UIManager>();

        equipmentSlots = itemsParent.GetComponentsInChildren<EquipmentSlotUI>(true);
        var index = 0;

        foreach (var equipmentSlot in equipmentSlots)
        {
            equipmentSlot.Initialize(this);
            var cat = equipmentSlot.slotCategory;
            var slot = equipmentInventory.equipmentSlots.value[cat];
            equipmentSlot.InventorySlot = slot;
        }
    }

    /*void UpdateSlotAtIndex(InventorySlot slot)
    {
        equipmentSlots[slot.slotIndex].UpdateUISlot(slot);
    }*/
}