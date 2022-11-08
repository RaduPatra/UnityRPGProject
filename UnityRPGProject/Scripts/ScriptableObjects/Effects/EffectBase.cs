using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EffectBase : ScriptableObject
{
    public virtual void UseEffect(GameObject target)
    {
        var ps = Instantiate(effectParticlePrefab, target.transform);
        var main = ps.main;
        main.startColor = effectColor;
        ps.Play();
    }
    
    public abstract void UpdateEffect(float deltaTime);

    public float effectExpireTime = 5f;

    protected float timeRemaining = -1;
    //protected Action<EffectBase> stopEffect;
    [SerializeField] protected ParticleSystem effectParticlePrefab;
    [SerializeField] protected Color effectColor;
    public bool IsDone()
    {
        return timeRemaining <= 0;
    }
}