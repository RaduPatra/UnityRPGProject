using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.UIElements;


public enum SlotType
{
    Any,
    Equipment
}

[Serializable]
public class InventorySlot
{
    [NonSerialized] public Action<InventorySlot> OnSlotChanged;
    [NonSerialized] public Action<ItemStack> OnSlotRemove;
    [NonSerialized] public Action<ItemStack> OnSlotAdd;
    public string id = Guid.NewGuid().ToString();

    public ItemStack itemStack;
    // public int slotIndex;
    public ItemCategory slotCategory;

    public InventorySlot Clone()
    {
        return MemberwiseClone() as InventorySlot;
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
            RemoveItem();
        }
    }

    public ItemWithAttributes GetItem()
    {
        return itemStack.item;
    }

    // this = drop slot(destination), slot = dragged slot(source)
    public void SwapSlot(InventorySlot slot)
    {
        if (!(CanAcceptItem(slot.itemStack.item) && slot.CanAcceptItem(itemStack.item)))
        {
            Debug.Log("failed");
            return;
        }


        if (slotCategory == slot.slotCategory)
        {
            (itemStack, slot.itemStack) = (slot.itemStack, itemStack);
            (itemStack.ParentSlot, slot.itemStack.ParentSlot) =
                (slot.itemStack.ParentSlot, itemStack.ParentSlot);
            OnSlotChanged?.Invoke(this);
            slot.OnSlotChanged?.Invoke(slot);
        }
        else
        {
            var removedDestinationStack = itemStack.Clone();
            var removedSourceStack = slot.itemStack.Clone();
            RemoveItem();
            slot.RemoveItem();
            
            AddItem(removedSourceStack);
            slot.AddItem(removedDestinationStack);
        }
    }

    public void RemoveItem()
    {
        Debug.Log("remove");

        OnSlotRemove?.Invoke(itemStack);
        itemStack.ResetStack();
        // itemStack = new ItemStack();
        OnSlotChanged?.Invoke(this);
    }

    public void AddItem(ItemStack stack)
    {
        Debug.Log("add");

        OnSlotAdd?.Invoke(stack);
        itemStack = stack;
        itemStack.ParentSlot = this;
        OnSlotChanged?.Invoke(this);
    }

    public bool CanAcceptItem(ItemWithAttributes item)
    {
        return item == null || item.HasCategory(slotCategory);
    }
    
}