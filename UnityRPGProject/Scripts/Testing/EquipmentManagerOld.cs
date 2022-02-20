using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class EquipmentManagerOld : MonoBehaviour
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

    private PlayerAttack playerAttack;
    private CharacterStatsTest characterStatsTest;

    // public Action<InventorySlot> OnEquipmentChange;
    public Action<EquipableItem> OnUnequipItem;
    public Action<EquipableItem> OnEquipItem;
    // public ItemTypeEventChannel unequipItemEventChannel;
    

    /*
     [SerializeField] private ItemSlotEventChannel equipmentChangedEventChannel;
     
     public ItemEventChannel equipItemEventChannel;*/

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        characterStatsTest = GetComponent<CharacterStatsTest>();
        // unequipItemEventChannel.Listeners += UnequipItemOnCharacter;
    }

    private void Start()
    {
        SetupSlotLocations();
        InitSlots();
    }


    private void InitSlots()
    {
        //setup slot events/equip
        //todo - ? move equip/unequip event in inventory instead of slot? (no need for many to one)
        foreach (var slot in equipmentInventory.equipmentArmorSlots)
        {
            // slot.Value.OnSlotEquip += EquipItem;
            // slot.Value.OnSlotUnequip += UnequipItem;
            var eqItem = slot.Value.itemStack.item as EquipableItem;
            if (eqItem != null)
                EquipItemOnCharacter(eqItem);
            // EquipItem(slot.Value);
        }

        foreach (var slot in equipmentInventory.equipmentArmorSlots)
        {
            slot.Value.OnSlotChanged += EquipItem;
            slot.Value.OnBeforeSlotChanged += ChangeStatsTest;
            // slot.Value.OnSlotUnequipTest += UnequipItemOnCharacter;
        }
        /*foreach (var slot in hotbarInventory.ItemList)
        {
            slot.OnSlotUnequip += UnequipActionItem;
        }

        foreach (var slot in mainInventory.ItemList)
        {
            slot.OnSlotUnequip += UnequipActionItem;
        }*/


        //equip actions
        foreach (var stack in equipmentInventory.equippedWeaponItems)
        {
            if (stack.Value == null || stack.Value.item == null) continue;
            var equipable = stack.Value.item as EquipableItem;
            if (equipable is null) continue;
            EquipItemOnCharacter(equipable);
        }
    }

    void ChangeStatsTest(InventorySlot slot)
    {
        if (slot.slotType == ItemType.Any) return;
        var equippedItem = GetEquippedItemFromType(slot.slotType);
        if (equippedItem != null)
            OnUnequipItem?.Invoke(equippedItem);
    }
    /*void OnItemChangeTest(InventorySlot slot)
    {
        if (!(slot.itemStack.item is EquipableItem item)) return;
        var equippedActionItemsTest = equipmentInventory.equippedActionsTest;
        if (!equippedActionItemsTest.ContainsKey(item.itemType)) return;
        equippedActionItemsTest[item.itemType].slot = null;
        // -> onchange
        equippedActionItemsTest[item.itemType].slot = slot;
        // -> onchange
    }*/

    private void SetupSlotLocations()
    {
        equipmentLocations.Add(ItemType.Helm, equipHelmLocation);
        equipmentLocations.Add(ItemType.Chest, equipChestLocation);
        equipmentLocations.Add(ItemType.Pants, equipPantsLocation);
        equipmentLocations.Add(ItemType.Boots, equipBootsLocation);
        equipmentLocations.Add(ItemType.Sword, equipWeaponLocation);
        equipmentLocations.Add(ItemType.Shield, equipShieldLocation);
    }

    public void EquipItem(InventorySlot slot)
    {
        if (slot.slotType == ItemType.Any) return;
        UnequipItemOnCharacter(slot.slotType);
        ChangeStatsTest(slot);
        if (!(slot.itemStack.item is EquipableItem item)) return;
        if (!equipmentInventory.equipmentArmorSlots.ContainsKey(item.itemType)) return;
        EquipItemOnCharacter(item);
    }

    public void EquipActionItemTest(InventorySlot slot)
    {
        if (!(slot.itemStack.item is EquipableItem item)) return;
        if (slot.itemStack.item == null) return;
        var equippedActionItems = equipmentInventory.equippedWeaponItems;
        if (!equippedActionItems.ContainsKey(item.itemType)) return;
        UnequipItemOnCharacter(item.itemType);
        ChangeStatsTest(slot);

        EquipActionItem(slot, item, equippedActionItems);
    }

    private void EquipActionItem(InventorySlot slot, EquipableItem item,
        IDictionary<ItemType, ItemStack> equippedActionItems)
    {
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
        EquipItemOnCharacter(item);
    }

    private void UnequipActionItem(ItemType itemType)
    {
        Debug.Log("unequip action item");

        var equippedActionItems = equipmentInventory.equippedWeaponItems;
        if (!equippedActionItems.ContainsKey(itemType)) return;
        var oldSlot = equippedActionItems[itemType]?.ParentSlot;
        if (oldSlot != null)
            oldSlot.IsEquipped = false;
    }

    private void EquipItemOnCharacter(EquipableItem item)
    {
        var equipLocation = equipmentLocations[item.itemType];
        var weaponGO = Instantiate(item.equipItemPrefab, equipLocation);

        if (item.itemType == ItemType.Sword)
            playerAttack.LoadWeapon(item, weaponGO);
        
        OnEquipItem?.Invoke(item);
    }

    private void UnequipItemOnCharacter(ItemType itemType)
    {
        if (!equipmentLocations.ContainsKey(itemType)) return;
        var equipLocation = equipmentLocations[itemType];
        if (equipLocation.childCount != 0)
            Destroy(equipLocation.GetChild(0).gameObject);
        /*var equippedItem = GetEquippedItemFromType(itemType);
        OnUnequipItem?.Invoke(equippedItem);*/
    }

    private EquipableItem GetEquippedItemFromType(ItemType itemType)
    {
        EquipableItem item = null;
        if (equipmentInventory.equippedWeaponItems.ContainsKey(itemType))
        {
            if (equipmentInventory.equippedWeaponItems[itemType] != null)
                item = equipmentInventory.equippedWeaponItems[itemType].item as EquipableItem;
        }
        else if (equipmentInventory.equipmentArmorSlots.ContainsKey(itemType))
        {
            item = equipmentInventory.equipmentArmorSlots[itemType].itemStack.item as EquipableItem;
        }
        return item;
    }
}