using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//todo add a ItemDragHandler class into the actual item being dragged(implements drag events etc) and only implement the drop on the slot
public class InventorySlotUI : MonoBehaviour, ISlot, IPointerExitHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image panelBackground;
    [SerializeField] private TextMeshProUGUI text;
    public Color defaultColor;
    public Color selectedColor;
    public InventoryUI InventoryUI { get; set; }
    public int SlotIndex { get; set; }

    private InventorySlot inventorySlot;
    private ItemCategory equippedCategory;


    public InventorySlot InventorySlot
    {
        get => inventorySlot;
        set
        {
            if (inventorySlot != null)
            {
                inventorySlot.OnSlotChanged -= UpdateUISlot;
                // inventorySlot.OnEquipActionChange -= EquipActionChanged;
            }

            inventorySlot = value;
            UpdateUISlot(value);
            // EquipActionChanged();

            if (inventorySlot == null) return;
            inventorySlot.OnSlotChanged += UpdateUISlot;
            // inventorySlot.OnEquipActionChange += EquipActionChanged;
        }
    }

    private void Awake()
    {
        InventoryUI = GetComponentInParent<InventoryUI>();
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        // Debug.Log("updated slot " + transform.name + " --- " + slot.itemStack.item);
        inventorySlot = slot;

        var item = slot.GetItem();

        Sprite icon = null;
        if (item != null)
        {
            var attr = item.GetAttribute<SpriteAttributeData>(InventoryUI.iconAttribute);
            icon = attr?.value;
        }

        itemIcon.sprite = icon;

        itemIcon.gameObject.SetActive(itemIcon.sprite);
        text.gameObject.SetActive(false);

        panelBackground.color = defaultColor;
        foreach (var equippedWeapon in InventoryUI.equipmentWeaponInventory.equipmentSlots)
        {
            if (slot.itemStack.id != equippedWeapon.Value.itemStack.id) continue;
            panelBackground.color = selectedColor;
            break;
        }
        
        // UpdateEquippedSlots(slot);


        // panelBackground.color = inventorySlot.IsEquipped ? selectedColor : defaultColor;
        if (slot.itemStack.item == null) return;
        if (!slot.itemStack.item.isStackable || slot.itemStack.quantity <= 1) return;
        text.gameObject.SetActive(true);
        text.SetText(slot.itemStack.quantity.ToString());
    }

    private bool IsRightButton(PointerEventData eventData)
    {
        return eventData.button == PointerEventData.InputButton.Right;
    }

    private bool IsMiddleButton(PointerEventData eventData)
    {
        return eventData.button == PointerEventData.InputButton.Middle;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventorySlot.itemStack.item == null) return;

        if (IsMiddleButton(eventData))
        {
            var itemStack = inventorySlot.itemStack;
            InventoryUI.itemDropEventChannel.Raise(itemStack.item); //call drop event from holder
            InventorySlot.RemoveItem();
        }
        else if (IsRightButton(eventData))
        {
            InventoryUI.itemUseEventChannel.Raise(inventorySlot);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var item = inventorySlot.GetItem();
        if (item == null) return;
        InventoryUI.uiManager.ItemInfoUI.ShowItemInfo(item);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryUI.uiManager.ItemInfoUI.HideItemInfo();
    }
}