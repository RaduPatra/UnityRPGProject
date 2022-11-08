using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShieldCollider : MonoBehaviour, IDamageCollider
{
    private Collider shieldCollider;
    private ItemWithAttributes currentItem;
    [SerializeField] private float damageReduction;
    [SerializeField] private AttributeBaseSO shieldDamageReductionAttribute;
    public ItemWithAttributes CurrentItem => currentItem;
    public float DamageReduction => damageReduction;


    private void Awake()
    {
        shieldCollider = GetComponent<BoxCollider>();
    }

    public void DisableCollider(AnimationEvent evt)
    {
        throw new NotImplementedException();
    }

    public void Initialize(ItemWithAttributes item)
    {
        currentItem = item;
        damageReduction = 0;
        var shieldData  = item.GetAttribute<FloatAttributeData>(shieldDamageReductionAttribute);
        if (shieldData != null) damageReduction = shieldData.value;
    }

    public void EnableCollider()
    {
        shieldCollider.enabled = true;
    }

    public void DisableCollider()
    {
        shieldCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("shield collided " + other);
    }
    
    public float ApplyShieldReduction(float attackDamage)
    {
        return Math.Max(0, attackDamage - attackDamage * damageReduction);
    }
}