using System;
using System.Collections.Generic;

[Serializable]
public class InventoryContainer
{
    public List<InventorySlot> itemList = new List<InventorySlot>();
    public void Setup(ItemCategory inventoryCategory)
    {
        for (var i = 0; i < itemList.Count; i++)
        {
            itemList[i].itemStack.ParentSlot = itemList[i];
            itemList[i].slotCategory = inventoryCategory;
        }
    }
}