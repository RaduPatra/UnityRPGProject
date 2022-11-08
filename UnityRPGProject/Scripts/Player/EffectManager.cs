using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private List<EffectBase> activeEffects = new List<EffectBase>();
    private List<EffectBase> effectsToRemove = new List<EffectBase>();


    private void Update()
    {
        UpdateEffects();
    }

    public void AddEffects(List<EffectBase> effects)
    {
        foreach (var effect in effects)
        {
            AddEffect(effect);
        }
    }
    private void AddEffect(EffectBase effect)
    {
        //only apply the last effect of the same type that was used
        var sameType = GetEffectOfSameType(effect);
        if (sameType != null)
        {
            effectsToRemove.Add(sameType);
        }

        activeEffects.Add(effect);
    }

    private EffectBase GetEffectOfSameType(EffectBase effect)
    {
        foreach (var activeEffect in activeEffects)
        {
            if (effect.GetType() != activeEffect.GetType()) continue;
            Debug.Log("same type " + effect.GetType());
            return activeEffect;
        }

        return null;
    }

    private void AddEffectToRemove(EffectBase effect)
    {
        effectsToRemove.Add(effect);
        Debug.Log("effect removed------------");
    }

    private void UpdateEffects()
    {
        foreach (var effect in activeEffects)
        {
            effect.UpdateEffect(Time.deltaTime);

            if (effect.IsDone())
            {
                AddEffectToRemove(effect);
            }
        }

        foreach (var effect in effectsToRemove)
        {
            activeEffects.Remove(effect);
        }

        effectsToRemove.Clear();
    }
}