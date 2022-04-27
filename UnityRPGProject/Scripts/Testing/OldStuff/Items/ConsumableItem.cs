using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Consumable", menuName = "Consumables/Consumable", order = 1)]
public class ConsumableItem : ItemBase
{
    
    public EffectBase[] effects;
    // public EffectBase useEffect;
    
    public void Use(GameObject target)
    {
        foreach (var effect in effects)
        {
            effect.UseEffect(target);
        }
    }
    

    /*public void UpdateConsumable(float deltaTime)
    {
        useEffect.UpdateEffect(deltaTime);
    }*/
}