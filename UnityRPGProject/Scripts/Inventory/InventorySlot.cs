using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;


public enum SlotType
{
    Any,
    Equipment
}

[Serializable]
public class InventorySlot
{
    [NonSerialized] public Action OnEquipActionChange;
    [NonSerialized] public Action<InventorySlot> OnSlotChanged;
    [NonSerialized] public Action<InventorySlot> OnBeforeSlotChanged;
    [NonSerialized] public Action<ItemType> OnSlotUnequip;

    public ItemStack itemStack;
    public ItemType slotType;
    public int slotIndex;

    private bool isEquipped;
    public bool IsEquipped
    {
        get => isEquipped;
        set
        {
            isEquipped = value;
            OnEquipActionChange?.Invoke();
        }
    }

    public InventorySlot Clone()
    {
        return MemberwiseClone() as InventorySlot;
    }

    public void ResetSlot()
    {
        OnBeforeSlotChanged?.Invoke(this);
        itemStack = new ItemStack();
        IsEquipped = false;
        OnSlotChanged?.Invoke(this);
    }

    public void DecreaseQuantity() //?move to itemstack
    {
        if (itemStack.quantity > 1)
        {
            itemStack.quantity--;
            OnSlotChanged?.Invoke(this);
        }
        else
        {
            ResetSlot();
        }
    }

    public void AssignItem(ItemStack item)
    {
        itemStack = item;
        itemStack.ParentSlot = this;
        OnSlotChanged?.Invoke(this);
    }

    public void SwapContentsWith(InventorySlot slot)
    {
        if (!(CanAcceptItem(slot.itemStack.item) && slot.CanAcceptItem(itemStack.item))) return;
        OnBeforeSlotChanged?.Invoke(this);
        slot.OnBeforeSlotChanged?.Invoke(slot);

        (itemStack.ParentSlot, slot.itemStack.ParentSlot) =
            (slot.itemStack.ParentSlot, itemStack.ParentSlot);
        (itemStack, slot.itemStack) = (slot.itemStack, itemStack);
        (IsEquipped, slot.IsEquipped) = (slot.IsEquipped, IsEquipped);

        OnSlotChanged?.Invoke(this);
        slot.OnSlotChanged?.Invoke(slot);
    }

    public bool CanAcceptItem(ItemBase item)
    {
        if (slotType == ItemType.Any || item == null) return true;
        var eqItem = item as EquipableItem;
        return eqItem != null && eqItem.itemType == slotType;
    }

    public EquipableItem TryGetEquipable()
    {
        var equipable = itemStack.item as EquipableItem;
        return equipable;
    }
}