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
    [SerializeField] private StatModifierEventChannel onStatModifierChangeEventChannel;

    [SerializeField] private Dictionary<StatType, float> activeModifiers = new Dictionary<StatType, float>();
    public Dictionary<StatType, float> ActiveModifiers => activeModifiers;

    [SerializeField] private AttributeBaseSO statModifiersAttribute;

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
        equipmentManager.OnEquipItem += AddItemStatsTest;
        equipmentManager.OnUnequipItem += RemoveItemStatsTest;
    }

    private void Start()
    {
        foreach (var modifier in activeModifiers)
        {
            if (onStatModifierChangeEventChannel != null)
                onStatModifierChangeEventChannel.Raise(new StatModifier(modifier.Key, modifier.Value));
        }
    }

    public float CalculateDamageReduction(float damage)//Valheim like damage reduction
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
        Debug.Log("add stats");
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
        Debug.Log("remove stats");

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
}