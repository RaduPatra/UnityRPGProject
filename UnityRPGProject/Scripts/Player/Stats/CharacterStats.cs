using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EquipmentManager equipmentManager;
    private Dictionary<InventorySlot, StatModifier> equipmentModifiers;

    [SerializeField] private CharacterAttributes characterAttributes;
    [SerializeField] private VoidEventChannel onStatsChangeEventChannel;
    [SerializeField] private StatModifierEventChannel onStatModifierChangeEventChannel;
    public CharacterAttributes CharacterAttributes => characterAttributes;

    private Dictionary<StatType, float> activeModifiers = new Dictionary<StatType, float>();

    private void Awake()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        if (equipmentManager != null)
        {
            Setup();
        }
    }

    private void Setup()
    {
        activeModifiers.Add(StatType.AttackDamage, 0);
        activeModifiers.Add(StatType.Defence, 0);
        
        foreach (var slot in equipmentManager.equipmentInventory.equipmentArmorSlots)
        {
            // slot.Value.OnSlotChanged += AddItemStats;
            // slot.Value.OnBeforeSlotChanged += RemoveItemStats;
            slot.Value.OnSlotChanged += AddItemStatsTest;
            slot.Value.OnBeforeSlotChanged += RemoveItemStatsTest;
        }

        foreach (var slot in equipmentManager.hotbarInventory.ItemList)
        {
            // slot.OnSlotChanged += AddItemStats;
            // slot.OnBeforeSlotChanged += RemoveItemStats;
            slot.OnSlotChanged += AddItemStatsTest;
            slot.OnBeforeSlotChanged += RemoveItemStatsTest;
        }

        foreach (var slot in equipmentManager.mainInventory.ItemList)
        {
            // slot.OnSlotChanged += AddItemStats;
            // slot.OnBeforeSlotChanged += RemoveItemStats;
            slot.OnSlotChanged += AddItemStatsTest;
            slot.OnBeforeSlotChanged += RemoveItemStatsTest;
        }
    }

    public int CalculateDamageReduction(int amount)
    {
        if (CharacterAttributes.characterDefence == 0) return amount;
        amount -= CharacterAttributes.characterDefence / 2;
        return amount;
    }

    private void AddStatModifiers(EquipableItem item)
    {
        /*foreach (var modifier in item.statModifiers)
        {
            var statsOfType = activeModifiers.FindAll(x => x.type == modifier.type);

            foreach (var stat in statsOfType)
            {
                stat.
            }
        }*/
    }

    private void AddItemStatsTest(InventorySlot slot)
    {
        if (CanChangeStats(slot) == false) return;
        var item = slot.TryGetEquipable();
        if (item == null) return;

        foreach (var modifier in item.statModifiers)
        {
            activeModifiers[modifier.type] += modifier.value;
            onStatModifierChangeEventChannel.Raise(new StatModifier(modifier.type, activeModifiers[modifier.type]));
        }
    }

    private void RemoveItemStatsTest(InventorySlot slot)
    {
        if (CanChangeStats(slot) == false) return;
        var item = slot.TryGetEquipable();
        if (item == null) return;

        foreach (var modifier in item.statModifiers)
        {
            activeModifiers[modifier.type] -= modifier.value;
            onStatModifierChangeEventChannel.Raise(new StatModifier(modifier.type, activeModifiers[modifier.type]));
        }
    }

    private void AddItemStats(InventorySlot slot)
    {
        if (CanChangeStats(slot) == false) return;
        var item = slot.TryGetEquipable();
        if (item == null) return;

        characterAttributes.characterAttackDamage += item.meleeAttackDamage;
        characterAttributes.characterDefence += item.defenceBonus;
        onStatsChangeEventChannel.RaiseVoid();
    }

    private void RemoveItemStats(InventorySlot slot)
    {
        if (CanChangeStats(slot) == false) return;
        var item = slot.TryGetEquipable();
        if (item == null) return;

        characterAttributes.characterAttackDamage -= item.meleeAttackDamage;
        characterAttributes.characterDefence -= item.defenceBonus;
        onStatsChangeEventChannel.RaiseVoid();
    }

    private bool CanChangeStats(InventorySlot slot)
    {
        var item = slot.TryGetEquipable();
        if (item == null) return false;

        equipmentManager.equipmentInventory.equipmentArmorSlots.TryGetValue(item.itemType, out var slot1);
        equipmentManager.equipmentInventory.equippedWeaponItems.TryGetValue(item.itemType, out var stack1);
        return slot1 == slot || stack1 != null && stack1.id == slot.itemStack.id;
    }
}