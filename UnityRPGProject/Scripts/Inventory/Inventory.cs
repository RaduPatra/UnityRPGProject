using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

[Serializable]
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/Inventory", order = 1)]
public class Inventory : InventoryBase
{
    [SerializeField] public InventoryContainer inventoryContainer = new InventoryContainer();
    public List<InventorySlot> ItemList => inventoryContainer.itemList;

    public ItemCategory inventoryCategory;

    [NonSerialized] public Action inventoryChanged;

    public void Setup()
    {
        for (var i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].itemStack.ParentSlot = ItemList[i];
            ItemList[i].slotCategory = inventoryCategory;
        }
    }

    public bool TryAddToEmptySlot(ItemWithAttributes item)
    {
        var slot = FindEmptySlot();
        if (slot == null) return false;
        slot.AddItem(new ItemStack(item, 1));
        return true;
    }

    public bool TryAddToStack(ItemWithAttributes item)
    {
        var slot = FindStack(item);
        if (slot == null) return false;
        slot.AddItem(new ItemStack(item,
            slot.itemStack.quantity +
            1));
        return true;
    }

    public InventorySlot FindStack(ItemWithAttributes item)
    {
        return ItemList.Find(slot => slot.itemStack.item == item && slot.itemStack.quantity < item.maxStack);
    }

    public InventorySlot FindEmptySlot()
    {
        return ItemList.Find(slot => slot.itemStack.item == null);
    }

    #region Testing

    [ContextMenu("get hash")]
    public void GetHashTest()
    {
        foreach (var item in ItemList)
        {
            var hc = item.itemStack.GetHashCode();
            Debug.Log(hc);
        }
    }

    /*public void SwapTest(int index, int index2)
    {
        (itemList[index], itemList[index2]) = (itemList[index2], itemList[index]);
    }*/

    #endregion


    //call this if you load items from inspector
    //TODO - add this to onvalidate
    [ContextMenu("Load item ids")]
    public void LoadItemIds()
    {
        foreach (var slot in ItemList)
        {
            var stack = slot.itemStack;
            if (stack == null) continue;
            if (stack.item != null)
                stack.itemId = stack.item.Id;
        }
    }

    [ContextMenu("refresh asset test")]
    public void RefreshAssetTest()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        // AssetDatabase.Refresh();
    }
}

[Serializable]
public class InventoryContainer
{
    public List<InventorySlot> itemList = new List<InventorySlot>();
}

public abstract class InventoryBase : SerializedScriptableObject
{
}