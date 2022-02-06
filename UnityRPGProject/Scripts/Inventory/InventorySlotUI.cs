using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//todo add a ItemDragHandler class into the actual item being dragged(implements drag events etc) and only implement the drop on the slot
public class InventorySlotUI : MonoBehaviour, ISlot
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image panelBackground;
    [SerializeField] private TextMeshProUGUI text;
    public Color defaultColor;
    public Color selectedColor;
    public InventoryUI InventoryUI { get; set; }
    public int SlotIndex { get; set; }

    private InventorySlot inventorySlot;

    public InventorySlot InventorySlot
    {
        //todo call updateslot in setter maybe?
        get => inventorySlot;
        set
        {
            Debug.Log("SET ");
            if (inventorySlot != null)
            {
                inventorySlot.OnSlotChanged -= UpdateUISlot;
                inventorySlot.OnEquipActionChange -= EquipActionChanged;
                inventorySlot.OnUseSlot -= UseSlot;
            }

            inventorySlot = value;
            UpdateUISlot(value);
            EquipActionChanged();

            if (inventorySlot != null)
            {
                inventorySlot.OnSlotChanged += UpdateUISlot;
                inventorySlot.OnEquipActionChange += EquipActionChanged;
                inventorySlot.OnUseSlot += UseSlot;
            }
        }
    }

    private void UseSlot(InventorySlot slot)
    {
    }

    private void Awake()
    {
        InventoryUI = GetComponentInParent<InventoryUI>();
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        Debug.Log("updated slot " + transform.name + " --- " + slot.itemStack.item);
        inventorySlot = slot;
        itemIcon.sprite = slot.itemStack.item ? slot.itemStack.item.itemIcon : null;
        itemIcon.gameObject.SetActive(itemIcon.sprite);
        text.gameObject.SetActive(false);
        // panelBackground.color = inventorySlot.itemStack.isEquipped ? selectedColor : defaultColor;
        if (slot.itemStack.item == null) return;
        if (!slot.itemStack.item.isStackable || slot.itemStack.quantity <= 1) return;
        text.gameObject.SetActive(true);
        text.SetText(slot.itemStack.quantity.ToString());
    }

    public void DropResponse()
    {
    }

    /*public void SwapWith(ISlot slot)
    {
        (inventorySlot, slot.InventorySlot) = (slot.InventorySlot, inventorySlot);
    }*/

    public bool CanAcceptSlot(InventorySlot slot)
    {
        return true;
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
            if (inventorySlot.itemStack.item is EquipableItem equipableItem)
            {
                // if (inventorySlot.IsEquipped)
                inventorySlot.OnSlotUnequip?.Invoke(equipableItem.itemType);
            }

            InventoryUI.itemDropEventChannel.Raise(InventorySlot.itemStack.item); //call drop event from holder
            InventorySlot.ResetSlot();
        }
        else if (IsRightButton(eventData))
        {
            //use
            InventoryUI.itemUseEventChannel.Raise(inventorySlot);
        }
    }

    private void EquipActionChanged()
    {
        panelBackground.color = inventorySlot.IsEquipped ? selectedColor : defaultColor;
    }
}