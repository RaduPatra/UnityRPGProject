using System;
using UnityEngine;


public class DamageDealerTest : MonoBehaviour
{
    public int damageAmount;
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        damageable?.Damage(damageAmount);
    }
}