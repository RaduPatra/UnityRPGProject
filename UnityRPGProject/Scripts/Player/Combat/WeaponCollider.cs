using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class WeaponCollider : MonoBehaviour
{
    private Collider damageCollider;
    private PlayerAttack playerAttack;
    private CharacterStats characterStats;
    private EquipmentManagerOld equipmentManagerOld;

    public Transform testTransform;

    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>();
        playerAttack = GetComponentInParent<PlayerAttack>();
        characterStats = GetComponentInParent<CharacterStats>();
        Debug.Log("coll onenable");
    }

    public void EnableCollider()
    {
        damageCollider.enabled = true;
        isFirstCollision = true;
        Debug.Log("enable collider");
    }

    public void DisableCollider()
    {
        damageCollider.enabled = false;
        Debug.Log("disable collider");
    }
    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        var damageable = other.GetComponent<IDamageable>();
        damageable?.Damage(currentWeapon.itemDamage);
    }*/

    private bool isFirstCollision = true;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);

        if (!isFirstCollision) return;
        isFirstCollision = false;
        
        var damageable = other.collider.GetComponent<IDamageable>();
        // damageable?.Damage(playerAttack.CurrentWeapon.itemDamage);
        // damageable?.Damage(characterStats.CharacterAttributes.characterAttackDamage);
        damageable?.Damage(characterStats.ActiveModifiers[StatType.AttackDamage]);
        
        var hitPoint = other.contacts[0].point;
        var hitNormal = other.contacts[0].normal;
        var ps = Instantiate(playerAttack.CurrentWeapon.impactParticle, hitPoint + hitNormal / 7, Quaternion.identity);
        ps.Play();
    }
}