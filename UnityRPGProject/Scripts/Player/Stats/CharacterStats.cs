using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem.Controls;


public enum StatType
{
    AttackDamage,
    Defence,
    MagicDamage
}

public class CharacterStats : SerializedMonoBehaviour
{
    private EquipmentManager equipmentManager;

    // private Dictionary<InventorySlot, StatModifier> equipmentModifiers;

    // [SerializeField] private CharacterAttributes characterAttributes;
    // [SerializeField] private VoidEventChannel onStatsChangeEventChannel;

    [SerializeField] private StatModifierEventChannel onStatModifierChangeEventChannel;
    // public CharacterAttributes CharacterAttributes => characterAttributes;

    [SerializeField] private Dictionary<StatType, float> activeModifiers = new Dictionary<StatType, float>();

    public Dictionary<StatType, float> ActiveModifiers => activeModifiers;


    [SerializeField] private AttributeBaseSO statModifiersAttribute;

    private void Awake()
    {
        // Debug.Log("char stats awake");

        equipmentManager = GetComponent<EquipmentManager>();
        if (equipmentManager != null)
        {
            Setup();
        }
    }

    private void Setup()
    {
        equipmentManager.OnEquipItem += AddItemStatsTest;
        equipmentManager.OnUnequipItem += RemoveItemStatsTest;
    }

    private void Start()
    {
        // Debug.Log("char stats start");
        foreach (var modifier in activeModifiers)
        {
            if (onStatModifierChangeEventChannel != null)
                onStatModifierChangeEventChannel.Raise(new StatModifier(modifier.Key, modifier.Value));
        }
    }

    public float CalculateDamageReduction(float damage/*, float defenceBonus*/)
    {
        Debug.Log("Initial Damage " + damage);
        var defenceBonus = ActiveModifiers[StatType.Defence];

        if (defenceBonus < damage / 2)
        {
            damage -= defenceBonus;
            Debug.Log("First Damage Reduction" + damage);
        }
        else
        {
            damage = Mathf.Clamp01(damage / (defenceBonus * 4)) * damage;
            Debug.Log("Second Damage Reduction" + damage);

        }

        return damage;
    }

    private void AddItemStatsTest(ItemWithAttributes item)
    {
        Debug.Log("add stats enable");
        var attr = item.GetAttribute<StatModifierListData>(statModifiersAttribute);
        if (attr == null) return;
        var statModifiers = attr.value;

        foreach (var modifier in statModifiers)
        {
            activeModifiers[modifier.type] += modifier.value;
            if (onStatModifierChangeEventChannel != null)
                onStatModifierChangeEventChannel.Raise(new StatModifier(modifier.type, activeModifiers[modifier.type]));
        }
    }

    private void RemoveItemStatsTest(ItemWithAttributes item)
    {
        var attr = item.GetAttribute<StatModifierListData>(statModifiersAttribute);
        if (attr == null) return;
        var statModifiers = attr.value;

        foreach (var modifier in statModifiers)
        {
            activeModifiers[modifier.type] -= modifier.value;
            if (onStatModifierChangeEventChannel != null)
                onStatModifierChangeEventChannel.Raise(new StatModifier(modifier.type, activeModifiers[modifier.type]));
        }
    }

    /*private bool CanChangeStats(InventorySlot slot)
    {
        var item = slot.GetItem();
        if (item == null) return false;
        if (!item.IsEquipment()) return false;

        equipmentManager.equipmentInventory.equipmentArmorSlots.TryGetValue(item.itemType, out var slot1);
        equipmentManager.equipmentInventory.equippedWeaponItems.TryGetValue(item.itemType, out var stack1);
        return slot1 == slot || stack1 != null && stack1.id == slot.itemStack.id;
    }*/
}