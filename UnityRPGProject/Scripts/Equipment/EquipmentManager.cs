using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
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
    public Action<EquipableItem, GameObject> OnEquipWeapon;

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
            slot.Value.OnSlotChanged += EquipArmor;
            slot.Value.OnBeforeSlotChanged += UnequipArmor;
            EquipArmor(slot.Value);
        }

        //equip actions
        foreach (var stack in equipmentInventory.equippedWeaponItems)
        {
            if (stack.Value == null || stack.Value.item == null) continue;
            var equipable = stack.Value.item as EquipableItem;
            if (equipable is null) continue;
            EquipItemOnCharacter(equipable);
        }

        foreach (var slot in mainInventory.ItemList)
        {
            slot.OnSlotUnequip += UnequipWeaponOnDrop;
            FindStackParentSlot(slot);
        }

        foreach (var slot in hotbarInventory.ItemList)
        {
            slot.OnSlotUnequip += UnequipWeaponOnDrop;
            FindStackParentSlot(slot);
        }
    }

    private void FindStackParentSlot(InventorySlot slot)
    {
        var item = slot.TryGetEquipable();
        if (item == null) return;

        if (!equipmentInventory.equippedWeaponItems.TryGetValue(item.itemType, out var stack)) return;
        if (stack == null) return;
        if (slot.itemStack.id == stack.id)
            equipmentInventory.equippedWeaponItems[item.itemType].ParentSlot = slot;
    }

    private void SetupSlotLocations()
    {
        equipmentLocations.Add(ItemType.Helm, equipHelmLocation);
        equipmentLocations.Add(ItemType.Chest, equipChestLocation);
        equipmentLocations.Add(ItemType.Pants, equipPantsLocation);
        equipmentLocations.Add(ItemType.Boots, equipBootsLocation);
        equipmentLocations.Add(ItemType.Sword, equipWeaponLocation);
        equipmentLocations.Add(ItemType.Shield, equipShieldLocation);
    }

    private void EquipItemOnCharacter(EquipableItem item)
    {
        if (!equipmentInventory.equipmentArmorSlots.ContainsKey(item.itemType) &&
            !equipmentInventory.equippedWeaponItems.ContainsKey(item.itemType)) return;
        var equipLocation = equipmentLocations[item.itemType];
        var weaponGO = Instantiate(item.equipItemPrefab, equipLocation);

        OnEquipWeapon?.Invoke(item, weaponGO);
    }

    private void UnequipItemOnCharacter(ItemType itemType)
    {
        if (!equipmentLocations.ContainsKey(itemType)) return;
        var equipLocation = equipmentLocations[itemType];
        if (equipLocation.childCount != 0)
        {
            Destroy(equipLocation.GetChild(0).gameObject);
        }
    }

    private void UnequipArmor(InventorySlot slot) //onbeforechange
    {
        if (CanChangeArmor(slot) == false) return;
        var item = slot.TryGetEquipable();
        if (item == null) return;
        UnequipItemOnCharacter(item.itemType);
    }

    private void EquipArmor(InventorySlot slot) //onafterchange
    {
        if (CanChangeArmor(slot) == false) return;
        var item = slot.TryGetEquipable();
        if (item == null) return;
        EquipItemOnCharacter(item);
    }

    public void ToggleWeaponAction(InventorySlot slot)
    {
        if (CanToggleSlot(slot) == false) return;
        var item = slot.TryGetEquipable();
        if (item == null) return;
        var equippedWeaponItems = equipmentInventory.equippedWeaponItems;
        UnequipItemOnCharacter(item.itemType);

        var oldSlot = equippedWeaponItems[item.itemType]?.ParentSlot;
        if (oldSlot != null)
            oldSlot.IsEquipped = false;

        oldSlot?.OnBeforeSlotChanged?.Invoke(oldSlot);

        //unequip if same item
        if (equippedWeaponItems[item.itemType] != null)
            Debug.Log(equippedWeaponItems[item.itemType].GetHashCode());
        if (slot.itemStack != null)
            Debug.Log(slot.itemStack.GetHashCode());

        if (slot.itemStack != null && equippedWeaponItems[item.itemType] != null &&
            equippedWeaponItems[item.itemType].id == slot.itemStack.id)
        {
            equippedWeaponItems[item.itemType] = null;
            slot.IsEquipped = false;
            return;
        }

        equippedWeaponItems[item.itemType] = slot.itemStack;
        slot.IsEquipped = true;
        EquipItemOnCharacter(item);
        slot.OnSlotChanged(slot);
    }

    private void UnequipWeaponOnDrop(ItemType itemType)
    {
        UnequipItemOnCharacter(itemType);
        equipmentInventory.equippedWeaponItems[itemType] = null;
    }

    private bool CanChangeArmor(InventorySlot slot)
    {
        var item = slot.TryGetEquipable();
        if (item == null) return false;
        if (!equipmentInventory.equipmentArmorSlots.ContainsKey(item.itemType)) return false;
        return equipmentInventory.equipmentArmorSlots[item.itemType] == slot;
    }

    private bool CanToggleSlot(InventorySlot slot)
    {
        var item = slot.TryGetEquipable();
        if (item == null) return false;
        var equippedWeaponItems = equipmentInventory.equippedWeaponItems;
        return equippedWeaponItems.ContainsKey(item.itemType);
    }

    public void EquipArmorAction(InventorySlot slot)
    {
        var item = slot.TryGetEquipable();
        if (item == null) return;
        if (!equipmentInventory.equipmentArmorSlots.ContainsKey(item.itemType)) return;
        var oldSlot = equipmentInventory.equipmentArmorSlots[item.itemType];
        slot.SwapContentsWith(oldSlot);
    }
}