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
    private Dictionary<KeyCode, InventorySlot> keySlots = new Dictionary<KeyCode, InventorySlot>();

    public List<InventorySlot> ItemList => itemList;

    //todo - replace param with predicate?
    public InventorySlot FindStack(ItemBase item)
    {
        return itemList.Find(slot => slot.itemStack.item == item && slot.itemStack.quantity < item.maxStack);
    }

    public InventorySlot FindEmptySlot()
    {
        return itemList.Find(slot => slot.itemStack.item == null);
    }

    private void OnEnable()
    {
        foreach (var slot in itemList)
        {
            slot.itemStack.OwnerSlot = slot;
        }
    }

    public void SwapTest(int index, int index2)
    {
        (itemList[index], itemList[index2]) = (itemList[index2], itemList[index]);
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
            1)); //? update method instead of creating new one maybe , assign for new , update for update
        return true;
    }
}