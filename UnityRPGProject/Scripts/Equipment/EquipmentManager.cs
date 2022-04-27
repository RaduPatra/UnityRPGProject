using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Text;
using Microsoft.SqlServer.Server;
using Sirenix.OdinInspector;
using UnityEngine;

public class EquipmentManager : SerializedMonoBehaviour
{
    [SerializeField]
    public readonly Dictionary<ItemCategory, Transform> equipmentLocations = new Dictionary<ItemCategory, Transform>();

    public EquipmentInventory equipmentInventory;
    public Inventory hotbarInventory;
    public Inventory mainInventory;

    [NonSerialized] public Action<ItemWithAttributes, GameObject> OnEquipWeapon;
    [NonSerialized] public Action<ItemCategory> OnUnequipWeapon;
    [NonSerialized] public Action<ItemWithAttributes> OnEquipItem;
    [NonSerialized] public Action<ItemWithAttributes> OnUnequipItem;

    private void Start()
    {
        Debug.Log("eq man start");
        InitSlots();
    }

    private void InitSlots()
    {
        //setup slot events/equip
        foreach (var slot in equipmentInventory.equipmentArmorSlots)
        {
            slot.Value.OnSlotAdd += EquipItem;
            slot.Value.OnSlotRemove += UnequipItem;
            EquipItem(slot.Value.itemStack);
        }

        foreach (var slot in mainInventory.ItemList)
        {
            FindStackParentSlot(slot);
        }

        foreach (var slot in hotbarInventory.ItemList)
        {
            FindStackParentSlot(slot);
        }
    }

    private void FindStackParentSlot(InventorySlot slot) //cleanup
    {
        var item = slot.GetItem();
        if (item == null) return;
        var stack = GetEquippedItemInfo(item, equipmentInventory.equippedWeaponItems, out var equippedCategory);
        if (stack == null) return;

        if (slot.itemStack.id != stack.id) return;
        /*-> we have to reassign the reference for it to update when swapping
         https://forum.unity.com/threads/losing-reference-when-hitting-play-not-a-use-serializefield-to-fix.690829/*/
        equipmentInventory.equippedWeaponItems[equippedCategory] = slot.itemStack;
        EquipItem(stack);
    }


    public AttributeBaseSO equipPrefabAttr;

    private GameObject EquipItemOnCharacter(ItemWithAttributes item)
    {
        var equipLocation = GetEquippedItemInfo(item, equipmentLocations, out var equippedCategory);
        var attr = item.GetAttribute<GameObjectData>(equipPrefabAttr);
        if (attr == null) return null;
        var equipper = equipLocation.GetComponent<IEquipper>();
        return equipper?.Equip(attr.value);
    }

    private void UnequipItemOnCharacter(ItemCategory itemType)
    {
        if (!equipmentLocations.ContainsKey(itemType)) return;
        var equipLocation = equipmentLocations[itemType];
        var equipper = equipLocation.GetComponent<IEquipper>();
        equipper?.Unequip();
    }

    public void UnequipItem(ItemStack stack) //onbeforechange
    {
        Debug.Log("UnequipArmor");
        var item = stack.item;
        if (item == null) return;
        var equippedCategory = FindEquippedCategory(item, equipmentLocations);
        UnequipItemOnCharacter(equippedCategory);
        OnUnequipItem?.Invoke(item);

        var equipItemAction = item.GetAttribute<EquipActionData>(equipActionAttr)?.value;
        if (equipItemAction)
            equipItemAction.ExecuteOnUnequip(item, gameObject);
    }

    public void RemoveWeapon(ItemStack stack) //onbeforechange
    {
        var item = stack.item;
        var equippedWeapons = equipmentInventory.equippedWeaponItems;
        var equippedCategory =
            FindEquippedCategory(item, equippedWeapons);

        equippedWeapons[equippedCategory] = new ItemStack();
    }


    public AttributeBaseSO equipActionAttr;

    public void EquipItem(ItemStack stack) //onafterchange
    {
        // Debug.Log("EquipArmor");
        var item = stack.item;
        if (item == null) return;
        var equippedGO = EquipItemOnCharacter(item);
        OnEquipItem?.Invoke(item);

        var attr = item.GetAttribute<EquipActionData>(equipActionAttr);
        // attr?.value.Equip(item, gameObject);
        if (equippedGO) attr?.value.ExecuteOnEquip(item, gameObject, equippedGO);
    }

    public ItemCategory FindEquippedCategory<T>(ItemWithAttributes item,
        IDictionary<ItemCategory, T> inventory)
    {
        var categories = item.GetCategoryAncestors();
        return categories.FirstOrDefault(inventory.ContainsKey);
    }

    public T GetEquippedItemInfo<T>(ItemWithAttributes item,
        IDictionary<ItemCategory, T> inventory, out ItemCategory cat)
    {
        cat = FindEquippedCategory(item, inventory);
        if (cat == null) return default;

        inventory.TryGetValue(cat, out var value);
        return value;
    }

    public Transform GetEquippedItemLocation(ItemWithAttributes item)
    {
        var equippedCategory = FindEquippedCategory(item, equipmentLocations);
        if (!equippedCategory) return null;
        // var equipLocation = equipmentLocations[equippedCategory];
        return !equipmentLocations.TryGetValue(equippedCategory, out var equipLocation) ? null : equipLocation;
    }
}