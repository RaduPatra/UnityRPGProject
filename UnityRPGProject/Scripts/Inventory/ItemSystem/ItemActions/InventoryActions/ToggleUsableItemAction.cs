using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ToggleUsableItem Action", menuName = "ItemActions/ToggleUsableItemAction", order = 1)]
public class ToggleUsableItemAction : InventoryItemAction
{
    public override void Execute(GameObject user, InventorySlot slot)
    {
        var playerManager = user.GetComponent<PlayerManager>();
        if (playerManager.IsInteracting) return;


        var equipmentManager = user.GetComponent<EquipmentManager>();


        var item = slot.GetItem();
        var equippedWeaponItems = equipmentManager.equipmentWeaponInventory.equipmentSlots;
        var eqSlot = equipmentManager.GetEquippedItemInfo(item, equippedWeaponItems, out var equippedCategory);
        var oldStack = eqSlot?.itemStack;
        if (oldStack == null) return;

        var oldSlot = oldStack.ParentSlot;
        equipmentManager.UnequipItem(oldStack);

        //unequip old slot
        if (oldSlot != null)
        {
            equippedWeaponItems[equippedCategory].itemStack = new ItemStack();
            oldSlot.OnSlotChanged?.Invoke(oldSlot);
            oldSlot.itemStack.OnStackReset -= equipmentManager.UnequipItem;
            oldSlot.itemStack.OnStackReset -= equipmentManager.RemoveWeapon;
        }

        if (slot.itemStack != null && oldStack.id == slot.itemStack.id) return;
        equippedWeaponItems[equippedCategory].itemStack = slot.itemStack;

        if (slot.itemStack != null)
        {
            slot.itemStack.OnStackReset += equipmentManager.UnequipItem;
            slot.itemStack.OnStackReset += equipmentManager.RemoveWeapon;
        }

        equipmentManager.EquipItem(slot.itemStack);

        slot.OnSlotChanged(slot);
    }
}