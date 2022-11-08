using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryUI : MonoBehaviour , IInitializable
{
    [SerializeField] public Inventory inventory;//
    [SerializeField] public EquipmentInventory equipmentWeaponInventory;
    [SerializeField] public Transform itemsParent;
    public ItemEventChannel itemDropEventChannel;
    public ItemSlotEventChannel itemUseEventChannel;
    public AttributeBaseSO iconAttribute;
    public UIManager uiManager;

    public void Initialize()
    {
        uiManager = GetComponentInParent<UIManager>();
        SaveSystem.OnLoad += LoadInventoryUI;
    }

    public void Destroy()
    {
        SaveSystem.OnLoad -= LoadInventoryUI;
    }

    private void LoadInventoryUI(SaveData obj)
    {
        SetupUI();
    }

    private void SetupUI()
    {
        //subscribe all uiSlots to the OnSlotAdded so each slot can update its own UI
        var uiSlots = itemsParent.GetComponentsInChildren<InventorySlotUI>(true);
        for (var i = 0; i < inventory.ItemList.Count; i++)
        {
            uiSlots[i].Initialize(this);
            var slot = inventory.ItemList[i];
            uiSlots[i].InventorySlot = slot;
            uiSlots[i].SlotIndex = i;
        }
    }
}