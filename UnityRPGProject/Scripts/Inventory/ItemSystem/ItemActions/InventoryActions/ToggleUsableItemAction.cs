using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ToggleUsableItem Action", menuName = "ItemActions/ToggleUsableItemAction", order = 1)]
public class ToggleUsableItemAction : InventoryItemAction
{
    public override void Execute(GameObject user, InventorySlot slot)
    {
        var equipmentManager = user.GetComponent<EquipmentManager>();

        var item = slot.GetItem();
        var equippedWeaponItems = equipmentManager.equipmentInventory.equippedWeaponItems;
        var oldStack = equipmentManager.GetEquippedItemInfo(item, equippedWeaponItems, out var equippedCategory);
        if (oldStack == null) return;
        
        var oldSlot = oldStack.ParentSlot;
        equipmentManager.UnequipItem(oldStack);

        //unequip old slot
        if (oldSlot != null)
        {
            equippedWeaponItems[equippedCategory] = new ItemStack();
            oldSlot.OnSlotChanged?.Invoke(oldSlot);
            oldSlot.itemStack.OnStackReset -= equipmentManager.UnequipItem;
            oldSlot.itemStack.OnStackReset -= equipmentManager.RemoveWeapon;
        }

        if (slot.itemStack != null && oldStack.id == slot.itemStack.id) return;
        equippedWeaponItems[equippedCategory] = slot.itemStack;

        if (slot.itemStack != null)
        {
            slot.itemStack.OnStackReset += equipmentManager.UnequipItem;
            slot.itemStack.OnStackReset += equipmentManager.RemoveWeapon;
        }

        equipmentManager.EquipItem(slot.itemStack);
        
        
        slot.OnSlotChanged(slot);
    }

}