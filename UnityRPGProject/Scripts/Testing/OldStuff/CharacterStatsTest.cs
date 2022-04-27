/*using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsTest : MonoBehaviour
{
    private int currentHealth;
    private EquipmentManagerOld equipmentManagerOld;
    private Dictionary<InventorySlot, StatModifier> equipmentModifiers;

    /*[SerializeField] private int characterAttackDamage;
    [SerializeField] private int characterDefence;
    [SerializeField] private int characterHealth;
    public int CharacterAttackDamage { get; }
    public int CharacterDefence { get; }
    public int CharacterHealth { get; }#1#

    [SerializeField] private CharacterAttributes characterAttributes;
    [SerializeField] private VoidEventChannel onStatsChangeEventChannel;
    public CharacterAttributes CharacterAttributes => characterAttributes;

    private List<StatModifier> activeModifiers;

    private void Awake()
    {
        // currentHealth = CharacterAttributes.CharacterHealth;
        equipmentManagerOld = GetComponent<EquipmentManagerOld>();

        if (equipmentManagerOld != null)
        {
            equipmentManagerOld.OnUnequipItem += RemoveItemStats;
            equipmentManagerOld.OnEquipItem += AddItemStats;
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
        }#1#
    }

    private void AddItemStats(EquipableItem item)
    {
        characterAttributes.characterAttackDamage += item.meleeAttackDamage;
        characterAttributes.characterDefence += item.defenceBonus;
        onStatsChangeEventChannel.RaiseVoid();
    }

    private void RemoveItemStats(EquipableItem item)
    {
        characterAttributes.characterAttackDamage -= item.meleeAttackDamage;
        characterAttributes.characterDefence -= item.defenceBonus;
        onStatsChangeEventChannel.RaiseVoid();
    }

    /*public void AddItemStats(EquipableItem item)
    {
        characterStats.CharacterAttributes.CharacterAttackDamage += item.meleeAttackDamage;
        characterStats.CharacterAttributes.CharacterHealth += item.healthBonus;
        characterStats.CharacterAttributes.CharacterDefence += item.defenceBonus;
    }

    public void RemoveItemStats(EquipableItem item)
    {
        characterStats.CharacterAttributes.CharacterAttackDamage -= item.meleeAttackDamage;
        characterStats.CharacterAttributes.CharacterHealth -= item.healthBonus;
        characterStats.CharacterAttributes.CharacterDefence -= item.defenceBonus;
    }#1#
}*/