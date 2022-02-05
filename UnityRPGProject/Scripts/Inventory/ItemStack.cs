using System;
using Sirenix.OdinInspector.Editor;

[Serializable]
public class ItemStack
{
    public ItemBase item;
    public int quantity;

    public ItemStack(ItemBase item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public ItemStack()
    {
        item = null;
        quantity = 0;
    }

    [field: NonSerialized]
    public InventorySlot OwnerSlot { get; set; }
}