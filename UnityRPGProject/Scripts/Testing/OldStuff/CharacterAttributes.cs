using System;
using System.Collections.Generic;
using UnityEngine;



[Serializable]

public class StatModifierList
{
    public List<StatModifier> statModifiers = new List<StatModifier>();
}


// [CreateAssetMenu(fileName = "New Attributes", menuName = "CharacterAttributes")]
public class CharacterAttributes : ScriptableObject
{
    public int characterAttackDamage;
    public int characterDefence;
}