using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

[Serializable]
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/Inventory", order = 2)]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<InventorySlot> itemList = new List<InventorySlot>();
    public List<InventorySlot> ItemList => itemList;

    private void OnEnable()
    {
        for (var i = 0; i < itemList.Count; i++)
        {
            itemList[i].itemStack.ParentSlot = itemList[i];
            itemList[i].slotType = ItemType.Any;
            itemList[i].slotIndex = i;
        }
    }

    public bool TryAddToEmptySlot(ItemBase item)
    {
        var slot = FindEmptySlot();
        if (slot == null) return false;
        slot.AssignItem(new ItemStack(item, 1));
        return true;
    }

    public bool TryAddToStack(ItemBase item)
    {
        var slot = FindStack(item);
        if (slot == null) return false;
        slot.AssignItem(new ItemStack(item,
            slot.itemStack.quantity +
            1));
        return true;
    }

    public InventorySlot FindStack(ItemBase item)
    {
        return itemList.Find(slot => slot.itemStack.item == item && slot.itemStack.quantity < item.maxStack);
    }

    public InventorySlot FindEmptySlot()
    {
        return itemList.Find(slot => slot.itemStack.item == null);
    }

    public void SwapTest(int index, int index2)
    {
        (itemList[index], itemList[index2]) = (itemList[index2], itemList[index]);
    }
}