using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventories/InventoryTestLazy", order = 1)]
public class Inventory : InventoryBase
{
    public LazyValueTest<InventoryContainer> lazyInventoryContainer = new LazyValueTest<InventoryContainer>();
    public List<InventorySlot> ItemList => lazyInventoryContainer.value.itemList;

    public ItemCategory inventoryCategory;

    [NonSerialized]
    public Action<ItemWithAttributes> OnPickup;

    public void Init(InventoryContainer inventoryContainer) //called in on before load
    {
        lazyInventoryContainer.Init(() => InitContainer(inventoryContainer));

        InventoryContainer InitContainer(InventoryContainer saveDataInv)
        {
            var newInv = saveDataInv;
            newInv.Setup(inventoryCategory);
            return newInv;
        }
    }

    public bool TryAddToEmptySlot(ItemWithAttributes item)
    {
        var slot = FindEmptySlot();
        if (slot == null) return false;
        slot.AddItem(new ItemStack(item, 1));
        OnPickup?.Invoke(item);
        return true;
    }

    public bool TryAddToStack(ItemWithAttributes item)
    {
        var slot = FindStack(item);
        if (slot == null) return false;
        slot.AddItem(new ItemStack(item,
            slot.itemStack.quantity +
            1));
        OnPickup?.Invoke(item);
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
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
        
        // AssetDatabase.Refresh();
    }
}