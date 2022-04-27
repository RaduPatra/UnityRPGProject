using System;
using Sirenix.OdinInspector.Editor;

[Serializable]
public class ItemStack
{
    // public ItemTest item;
    public ItemWithAttributes item;
    public int quantity;
    public string id;

    [NonSerialized]
    public Action<ItemStack> OnStackReset;

    public ItemStack(ItemWithAttributes item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
        id = Guid.NewGuid().ToString();
    }

    public ItemStack()
    {
        item = null;
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
        quantity = 0;
        OnStackReset = null;
        id = Guid.NewGuid().ToString();
    }

    [field: NonSerialized] public InventorySlot ParentSlot { get; set; }
    // public InventorySlot ParentSlot;
}