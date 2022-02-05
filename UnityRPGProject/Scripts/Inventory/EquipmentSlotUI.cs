using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//todo add a ItemDragHandler class into the actual item being dragged(implements drag events etc) and only implement the drop on the slot
public class EquipmentSlotUI : MonoBehaviour, ISlot, IPointerEnterHandler, IPointerExitHandler
{
    private EquipmentUI EquipmentUI { get; set; }
    public ItemType slotItemType;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image panelBackground;
    [SerializeField] private Color acceptedColor;
    [SerializeField] private Color deniedColor;
    [SerializeField] private Color defaultColor;

    private InventorySlot inventorySlot;

    public InventorySlot InventorySlot
    {
        get => inventorySlot;
        set
        {
            if (inventorySlot != null)
            {
                inventorySlot.OnSlotChanged -= UpdateUISlot;
            }

            inventorySlot = value;

            if (inventorySlot != null)
            {
                inventorySlot.OnSlotChanged += UpdateUISlot;
            }
        }
    }

    private void Awake()
    {
        EquipmentUI = GetComponentInParent<EquipmentUI>();
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        Debug.Log("updated slot " + transform.name + " --- " + slot.itemStack.item);
        InventorySlot = slot;
        itemIcon.sprite = slot.itemStack.item ? slot.itemStack.item.itemIcon : null;
        itemIcon.gameObject.SetActive(itemIcon.sprite);
    }

    public void DropResponse()
    {
        if (inventorySlot.itemStack.item != null)
            inventorySlot.OnSlotEquip?.Invoke(inventorySlot);
        else inventorySlot.OnSlotUnequip?.Invoke(slotItemType);
    }

    public bool CanAcceptSlot(InventorySlot slot)
    {
        var equipable = slot.itemStack.item as EquipableItem;
        panelBackground.color = defaultColor;
        return equipable && equipable.itemType == slotItemType || slot.itemStack.item == null;
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
            EquipmentUI.itemDropEventChannel.Raise(InventorySlot.itemStack.item); //call drop event from holder
            InventorySlot.ResetSlot();
            inventorySlot.OnSlotUnequip?.Invoke(slotItemType);
        }
        else if (IsRightButton(eventData))
        {
            var success = EquipmentUI.itemPickupEventChannel.RaiseBool(InventorySlot.itemStack.item);
            if (!success) return;
            InventorySlot.ResetSlot();
            inventorySlot.OnSlotUnequip?.Invoke(slotItemType);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        var slot = eventData.pointerDrag.GetComponent<ISlot>();
        panelBackground.color = deniedColor;
        if (!(slot.InventorySlot.itemStack.item is EquipableItem equipableItem)) return;
        if (equipableItem.itemType == slotItemType)
            panelBackground.color = acceptedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panelBackground.color = defaultColor;
    }
}