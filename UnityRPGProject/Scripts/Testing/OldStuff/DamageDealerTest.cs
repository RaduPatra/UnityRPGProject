using System;
using UnityEngine;


public class DamageDealerTest : MonoBehaviour
{
    public int damageAmount;
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        var damageInfo = new DamageInfo
        {
            damageAmount = damageAmount,
            damager = gameObject
        };
        damageable?.Damage(damageInfo);
    }
}