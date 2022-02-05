using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public abstract class EffectBase : ScriptableObject
{
    public abstract void UseEffect(GameObject target);

    // public abstract void UseEffect(GameObject target);
    public abstract void UpdateEffect(float deltaTime);

    public float effectExpireTime = 5f;

    protected float timeRemaining;
    protected Action<EffectBase> stopEffect;
    
    public bool IsDone()
    {
        return timeRemaining <= 0;
    }
}