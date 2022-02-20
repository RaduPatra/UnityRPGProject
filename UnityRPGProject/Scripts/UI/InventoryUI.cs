using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // [SerializeField] private InventoryHolder inventoryHolder; // (inventoryHolder.inv -> inv)
    [SerializeField] public Inventory inventory;
    [SerializeField] public Transform itemsParent;
    public ItemEventChannel itemDropEventChannel;
    public ItemSlotEventChannel itemUseEventChannel;

    private void Awake()
    {
        //subscribe all uiSlots to the OnSlotAdded so each slot can update its own UI
        var uiSlots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
        for (var i = 0; i < inventory.ItemList.Count; i++)
        {
            var slot = inventory.ItemList[i];
            uiSlots[i].InventorySlot = slot;
            // uiSlots[i].InventoryUI = inventory;
            uiSlots[i].SlotIndex = i;
            // slot.OnSlotChanged?.Invoke(slot);
        }
    }
}