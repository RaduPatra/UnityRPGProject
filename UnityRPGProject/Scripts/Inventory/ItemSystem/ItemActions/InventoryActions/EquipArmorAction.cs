using UnityEngine;


[CreateAssetMenu(fileName = "New EquipArmor Action", menuName = "ItemActions/EquipArmorAction", order = 1)]

public class EquipArmorAction : InventoryItemAction
{
    public override void Execute(GameObject user, InventorySlot slot)
    {
        var equipmentManager = user.GetComponent<EquipmentManager>();
        var item = slot.GetItem();
        var inventory = equipmentManager.equipmentArmorInventory.equipmentSlots.value;
        var equippedCategory = equipmentManager.FindEquippedCategory(item, inventory);

        if (equippedCategory == null) return;
        var oldSlot = equipmentManager.equipmentArmorInventory.equipmentSlots.value[equippedCategory];
        slot.SwapSlot(oldSlot);
    }
}