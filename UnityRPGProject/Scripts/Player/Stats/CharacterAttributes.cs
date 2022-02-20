using System;
using UnityEngine;

public enum StatType
{
    AttackDamage,
    Defence
}

[Serializable]
public class StatModifier
{
    public StatType type;
    public float value;
    public StatModifier(StatType type, float value)
    {
        this.type = type;
        this.value = value;
    }
}


[CreateAssetMenu(fileName = "New Attributes", menuName = "Attributes")]
public class CharacterAttributes : ScriptableObject
{
    public int characterAttackDamage;
    public int characterDefence;
}