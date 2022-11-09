using System.Collections;
using UnityEngine;

public abstract class EquipItemAction : ScriptableObject
{
    public abstract void ExecuteOnEquip(ItemWithAttributes item, GameObject parentGO, GameObject gameObjectToEquip = null);
    public abstract void ExecuteOnUnequip(ItemWithAttributes item, GameObject parentGO, GameObject gameObjectToEquip = null);
    
    public AnimatorOverrideController actionAnimationOverride;

    public float animationWeightTest = -1;
}