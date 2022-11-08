using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Text;
using Microsoft.SqlServer.Server;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public interface IEquipment
{
    public Dictionary<ItemCategory, Transform> EquipmentLocations { get; }
}


public class EquipmentManager : SerializedMonoBehaviour, IEquipment
{
    [SerializeField]
    private readonly Dictionary<ItemCategory, Transform> equipmentLocations = new Dictionary<ItemCategory, Transform>();

    public Dictionary<ItemCategory, Transform> EquipmentLocations => equipmentLocations;
    public EquipmentInventory equipmentArmorInventory;
    public EquipmentInventory equipmentWeaponInventory;

    public Inventory hotbarInventory;
    public Inventory mainInventory;

    [NonSerialized] public Action<ItemWithAttributes> OnEquipItem;
    [NonSerialized] public Action<ItemWithAttributes> OnUnequipItem;

    [SerializeField] AttributeBaseSO equipPrefabAttr;
    [SerializeField] private AttributeBaseSO equipActionAttr;

    private bool armorFirstLoad = true;
    private bool weaponFirstLoad = true;

    private void Awake()
    {
        SaveSystem.OnInitSaveData += InitSaveData;
        SaveSystem.OnBeforeLoad += InitLoadEquipment;
        SaveSystem.OnLoad += LoadEquipment;
    }

    private void OnDestroy()
    {
        SaveSystem.OnInitSaveData -= InitSaveData;
        SaveSystem.OnBeforeLoad -= InitLoadEquipment;
        SaveSystem.OnLoad -= LoadEquipment;
    }

    private void Start()
    {
        Debug.Log("eq man start");
    }

    private void LoadEquipment(SaveData saveData)
    {
        SetupEquippedWeaponInInventory(hotbarInventory);
        SetupEquippedWeaponInInventory(mainInventory);
    }

    private void InitLoadEquipment(SaveData saveData)
    {
        equipmentArmorInventory.equipmentSlots.Init(InitArmorTest);
        equipmentWeaponInventory.equipmentSlots.Init(InitWeaponTest);

        Dictionary<ItemCategory, InventorySlot> InitArmorTest()
        {
            // saveData = SaveSystem.GetDataFromFile();
            if (!armorFirstLoad)
            {
                foreach (var itemInfo in equipmentArmorInventory.equipmentSlots.ValueNoInit)
                {
                    var item = itemInfo.Value.GetItem();
                    if (item == null) continue;
                    UnequipItem(itemInfo.Value.itemStack);
                }
            }

            armorFirstLoad = false;

            // equipmentArmorInventory.equipmentSlots.Clear();
            var newInv = new Dictionary<ItemCategory, InventorySlot>();

            var db = SaveSystemManager.Instance.categoryDb;

            foreach (var key in saveData.equipmentArmorSave.Keys)
            {
                var cat = db.GetById(key);
                if (cat == null) continue;
                newInv[cat] = saveData.equipmentArmorSave[key];
            }

            SetupSlotCategs(newInv);

            foreach (var itemInfo in newInv)
            {
                itemInfo.Value.OnSlotAdd += EquipItem;
                itemInfo.Value.OnSlotRemove += UnequipItem;
                var item = itemInfo.Value.GetItem();
                if (item == null) continue;
                EquipItem(itemInfo.Value.itemStack);
            }

            return newInv;
        }

        Dictionary<ItemCategory, InventorySlot> InitWeaponTest()
        {
            // saveData = SaveSystem.GetDataFromFile();
            var db = SaveSystemManager.Instance.categoryDb;
            var newInv = new Dictionary<ItemCategory, InventorySlot>();
            if (!weaponFirstLoad)
            {
                foreach (var itemInfo in equipmentWeaponInventory.equipmentSlots.ValueNoInit)
                {
                    if (itemInfo.Value == null) continue;
                    var item = itemInfo.Value.GetItem();
                    if (item == null) continue;
                    UnequipItem(itemInfo.Value.itemStack);
                }
            }

            weaponFirstLoad = false;

            foreach (var key in saveData.equippedWeaponSave.Keys)
            {
                var cat = db.GetById(key);
                if (cat == null) continue;
                newInv[cat] = saveData.equippedWeaponSave[key];
            }

            SetupSlotCategs(newInv);
            return newInv;
        }
    }

    private void SetupSlotCategs(Dictionary<ItemCategory, InventorySlot> equipmentSlots)
    {
        foreach (var slot in equipmentSlots)
        {
            equipmentSlots[slot.Key].slotCategory = slot.Key;
        }
    }

    private void InitSaveData()
    {
        Debug.Log("eq man OnBeforeLoadTest");

        foreach (var armor in equipmentArmorInventory.equipmentSlots.ValueNoInit)
            SaveData.Current.equipmentArmorSave[armor.Key.Id] = armor.Value;

        foreach (var weapon in equipmentWeaponInventory.equipmentSlots.ValueNoInit)
            SaveData.Current.equippedWeaponSave[weapon.Key.Id] = weapon.Value;
    }

    private void SetupEquippedWeaponInInventory(Inventory inventory)
    {
        foreach (var slot in inventory.ItemList)
        {
            SetupEquippedWeapon(slot);
        }
    }

    private void SetupEquippedWeapon(InventorySlot slot) //cleanup
    {
        var item = slot.GetItem();
        if (item == null) return;
        var eqSlot = GetEquippedItemInfo(item, equipmentWeaponInventory.equipmentSlots.value,
            out var equippedCategory);
        if (eqSlot?.itemStack == null) return;
        var stack = eqSlot.itemStack;

        if (slot.itemStack.id != stack.id) return;
        /*-> we have to reassign the reference for it to update when toggling
         https://forum.unity.com/threads/losing-reference-when-hitting-play-not-a-use-serializefield-to-fix.690829/*/
        equipmentWeaponInventory.equipmentSlots.value[equippedCategory].itemStack = slot.itemStack;
        slot.itemStack.OnStackReset += UnequipItem;
        slot.itemStack.OnStackReset += RemoveWeapon;

        EquipItem(stack);
    }

    public void EquipItem(ItemStack stack)
    {
        Debug.Log("EquipArmor");
        var item = stack.item;
        if (item == null) return;
        var equippedGO = EquipItemOnCharacter(item);
        OnEquipItem?.Invoke(item);

        var attr = item.GetAttribute<EquipActionData>(equipActionAttr);
        // attr?.value.Equip(item, gameObject);
        if (equippedGO) attr?.value.ExecuteOnEquip(item, gameObject, equippedGO);
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

    public void RemoveWeapon(ItemStack stack)
    {
        var item = stack.item;
        var equippedWeapons = equipmentWeaponInventory.equipmentSlots.value;
        var equippedCategory =
            FindEquippedCategory(item, equippedWeapons);

        equippedWeapons[equippedCategory].itemStack = new ItemStack();
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