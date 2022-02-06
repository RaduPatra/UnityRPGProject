using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Transform equipWeaponLocation;
    [SerializeField] private Transform equipHelmLocation;
    [SerializeField] private Transform equipChestLocation;
    [SerializeField] private Transform equipPantsLocation;
    [SerializeField] private Transform equipBootsLocation;
    [SerializeField] private Transform equipShieldLocation;

    private readonly Dictionary<ItemType, Transform> equipmentLocations = new Dictionary<ItemType, Transform>();

    public EquipmentInventory equipmentInventory;
    public Inventory hotbarInventory;
    public Inventory mainInventory;

    // public ItemEventChannel equipItemEventChannel;

    private void Awake()
    {
        SetupSlots();
    }
    private void Start()
    {
        //todo - ? move equip/unequip event in inventory instead of slot? (no need for many to one)
        foreach (var slot in equipmentInventory.EquipmentSlots)
        {
            slot.Value.OnSlotEquip += EquipItem;
            slot.Value.OnSlotUnequip += UnequipItem;
            EquipItem(slot.Value);
        }
        
        foreach (var slot in hotbarInventory.ItemList)
        {
            slot.OnSlotUnequip += UnequipActionItem;
        }
        
        foreach (var slot in mainInventory.ItemList)
        {
            slot.OnSlotUnequip += UnequipActionItem;
        }

        EquipActions();
    }

    private void SetupSlots()
    {
        equipmentLocations.Add(ItemType.Helm, equipHelmLocation);
        equipmentLocations.Add(ItemType.Chest, equipChestLocation);
        equipmentLocations.Add(ItemType.Pants, equipPantsLocation);
        equipmentLocations.Add(ItemType.Boots, equipBootsLocation);
        equipmentLocations.Add(ItemType.Weapon, equipWeaponLocation);
        equipmentLocations.Add(ItemType.Shield, equipShieldLocation);
    }

    public void EquipItem(InventorySlot slot)
    {
        if (!(slot.itemStack.item is EquipableItem item)) return;

        EquipItemOnCharacter(slot);

        //add to inventory if its allowed item
        if (!equipmentInventory.EquipmentSlots.ContainsKey(item.itemType)) return;
        var equipSlot = equipmentInventory.EquipmentSlots[item.itemType];
        slot.SwapContentsWith(equipSlot);
    }

    private void UnequipItem(ItemType itemType)
    {
        Debug.Log("unequip item");
        equipmentInventory.EquipmentSlots[itemType].ResetSlot();
        UnEquipSlotOnCharacter(itemType);
    }

    private void UnequipActionItem(ItemType itemType)
    {
        Debug.Log("unequip action item");
        var equippedActionItems = equipmentInventory.equippedActionItems;
        if (equippedActionItems.ContainsKey(itemType))
        {
            //unequip old slot
            var oldSlot = equippedActionItems[itemType]?.OwnerSlot;
            if (oldSlot != null)
                oldSlot.IsEquipped = false;
        }
        UnEquipSlotOnCharacter(itemType);
    }

    private void EquipActions()
    {
        foreach (var stack in equipmentInventory.equippedActionItems)
        {
            if (stack.Value == null || stack.Value.item == null) continue;
            var equipable = stack.Value.item as EquipableItem;
            if (equipable is null) continue;
            var equipLocation = equipmentLocations[equipable.itemType];
            Instantiate(equipable.itemPrefab, equipLocation);
        }
    }
    private void EquipItemOnCharacter(InventorySlot slot)
    {
        if (!(slot.itemStack.item is EquipableItem item)) return;
        var equipLocation = equipmentLocations[item.itemType];

        //unequip old item
        UnEquipSlotOnCharacter(item.itemType);
        
        //toggle or change action item
        var equippedActionItems = equipmentInventory.equippedActionItems;
        if (equippedActionItems.ContainsKey(item.itemType))
        {
            //unequip old slot
            UnequipActionItem(item.itemType);

            //unequip if same item
            if (equippedActionItems[item.itemType] == slot.itemStack)
            {
                equippedActionItems[item.itemType] = null;
                slot.IsEquipped = false;
                return;
            }
            equippedActionItems[item.itemType] = slot.itemStack;
            slot.IsEquipped = true;
        }
        //equip item on player
        if (slot.itemStack.item == null) return;
        Instantiate(item.itemPrefab, equipLocation);
    }
    
    private void UnEquipSlotOnCharacter(ItemType itemType)
    {
        var equipLocation = equipmentLocations[itemType];
        if (equipLocation.childCount != 0)
            Destroy(equipLocation.GetChild(0).gameObject);
    }
}
