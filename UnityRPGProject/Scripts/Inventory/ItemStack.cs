using System;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;

[Serializable]
public class ItemStack : ISerializationCallbackReceiver
{
    // public ItemTest item;
    public ItemWithAttributes item;
    public string itemId;
    public int quantity;
    public string id;

    [NonSerialized] public Action<ItemStack> OnStackReset;

    public ItemStack(ItemWithAttributes item, int quantity)
    {
        this.item = item;
        itemId = item.Id;
        this.quantity = quantity;
        id = Guid.NewGuid().ToString();
    }

    public ItemStack()
    {
        item = null;
        itemId = "";
        quantity = 0;
        id = Guid.NewGuid().ToString();
    }
    
    public ItemStack Clone()
    {
        return MemberwiseClone() as ItemStack;
    }

    public void ResetStack()
    {
        OnStackReset?.Invoke(this);
        item = null;
        itemId = "";
        quantity = 0;
        OnStackReset = null;
        id = Guid.NewGuid().ToString();
    }

    public void OnBeforeSerialize()
    {
        // Debug.Log("Test OnBeforeSerialize" + id + " ");
    }

    public void OnAfterDeserialize()
    {
        // Debug.Log("Test OnAfterDeserialize" + id + " ");
        if (itemId == "") return;
        var saveManager = SaveSystemManager.Instance;
        if (saveManager == null) return;
        var db = saveManager.itemDb;
        if (db == null) return;
        var foundItem = db.GetById(itemId);
        item = foundItem;
    }

    [field: NonSerialized] public InventorySlot ParentSlot { get; set; }
    // public InventorySlot ParentSlot;
}