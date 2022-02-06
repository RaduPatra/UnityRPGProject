using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class InventorySlot
{
    public delegate void UpdateSlotEvent(InventorySlot slot);

    public delegate void UnequipSlotEvent(ItemType slot);

    public delegate void EquipActionEvent();

    [NonSerialized] public Action OnEquipActionChange;
    [NonSerialized] public UpdateSlotEvent OnSlotChanged;
    [NonSerialized] public UpdateSlotEvent OnSlotEquip;
    [NonSerialized] public UnequipSlotEvent OnSlotUnequip;
    [NonSerialized] public Action<InventorySlot> OnUseSlot;

    public ItemStack itemStack;

    [SerializeField] private bool isEquipped;

    public bool IsEquipped
    {
        get => isEquipped;
        set
        {
            isEquipped = value;
            Debug.Log(OnEquipActionChange);
            OnEquipActionChange?.Invoke();
        }
    }

    public InventorySlot Clone()
    {
        return MemberwiseClone() as InventorySlot;
    }

    public void ResetSlot()
    {
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
        itemStack.OwnerSlot = this;
        OnSlotChanged?.Invoke(this);
    }

    public void SwapContentsWith(InventorySlot slot)
    {
        (itemStack.OwnerSlot, slot.itemStack.OwnerSlot) =
            (slot.itemStack.OwnerSlot, itemStack.OwnerSlot);
        (itemStack, slot.itemStack) = (slot.itemStack, itemStack);
        (IsEquipped, slot.IsEquipped) = (slot.IsEquipped, IsEquipped);

        OnSlotChanged?.Invoke(this);
        slot.OnSlotChanged?.Invoke(slot);
    }
}