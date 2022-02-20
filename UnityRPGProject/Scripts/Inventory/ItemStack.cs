using System;
using Sirenix.OdinInspector.Editor;

[Serializable]
public class ItemStack
{
    public ItemBase item;
    public int quantity;
    public string id;

    public ItemStack(ItemBase item, int quantity)
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

    [field: NonSerialized]
    public InventorySlot ParentSlot { get; set; }
}