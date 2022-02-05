using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

enum ActionItemType
{
    Weapon,
    Shield
}

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] private Transform equipWeaponLocation;
    [SerializeField] private Transform equipHelmLocation;
    [SerializeField] private Transform equipChestLocation;
    [SerializeField] private Transform equipPantsLocation;
    [SerializeField] private Transform equipBootsLocation;
    [SerializeField] private Transform equipShieldLocation;

    //todo - make equipable inventory so with key=type,value = invslot and in here delete equip info and just make key=type,val = transform


    private readonly Dictionary<ItemType, Transform> equipmentLocations = new Dictionary<ItemType, Transform>();

    // private readonly Dictionary<ItemType, ItemStack> equippedActionItems = new Dictionary<ItemType, ItemStack>();

    

    public Dictionary<ItemType, Transform> EquipmentLocations => equipmentLocations;

    public EquipmentInventory equipmentInventory;

    // public ItemEventChannel equipItemEventChannel;


    private void Awake()
    {
        SetupSlots();
    }

    private void Start()
    {
        foreach (var slot in equipmentInventory.EquipmentSlots)
        {
            slot.Value.OnSlotEquip += EquipItem;
            slot.Value.OnSlotUnequip += UnequipItem;
            EquipItem(slot.Value);
        }

        EquipActions();
        /*foreach (var slot in equipmentInventory.equippedActionItems)
        {
            // EquipItem(slot);
        }*/
    }

    private void SetupSlots()
    {
        EquipmentLocations.Add(ItemType.Helm, equipHelmLocation);
        EquipmentLocations.Add(ItemType.Chest, equipChestLocation);
        EquipmentLocations.Add(ItemType.Pants, equipPantsLocation);
        EquipmentLocations.Add(ItemType.Boots, equipBootsLocation);
        EquipmentLocations.Add(ItemType.Weapon, equipWeaponLocation);
        EquipmentLocations.Add(ItemType.Shield, equipShieldLocation);

        /*equippedActionItems.Add(ItemType.Weapon, new ItemStack());
        equippedActionItems.Add(ItemType.Shield, new ItemStack());*/


        /*equipmentInventory.equippedActionSlots.Add(ItemType.Weapon, null);
        equipmentInventory.equippedActionSlots.Add(ItemType.Shield, null);*/
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

    public void UnequipItem(ItemType itemType)
    {
        equipmentInventory.EquipmentSlots[itemType].ResetSlot();
        UnEquipSlotOnCharacter(itemType);
    }

    void EquipActions()
    {
        foreach (var stack in equipmentInventory.equippedActionItems)
        {
            if (stack.Value == null || stack.Value.item == null) continue;
            var equipable = stack.Value.item as EquipableItem;
            if (equipable is null) continue;
            var equipLocation = EquipmentLocations[equipable.itemType];
            Instantiate(equipable.itemPrefab, equipLocation);
        }
    }
    private void EquipItemOnCharacter(InventorySlot slot)
    {
        if (!(slot.itemStack.item is EquipableItem item)) return;
        var equipLocation = EquipmentLocations[item.itemType];

        //unequip old item
        UnEquipSlotOnCharacter(item.itemType);
        
        //toggle or change action item
        var equippedActionItems = equipmentInventory.equippedActionItems;
        if (equippedActionItems.ContainsKey(item.itemType))
        {
            //unequip old slot
            var oldSlot = equippedActionItems[item.itemType]?.OwnerSlot;
            if (oldSlot != null)
                oldSlot.IsEquipped = false;
            
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
        var equipLocation = EquipmentLocations[itemType];
        if (equipLocation.childCount != 0)
            Destroy(equipLocation.GetChild(0).gameObject);
    }
}
