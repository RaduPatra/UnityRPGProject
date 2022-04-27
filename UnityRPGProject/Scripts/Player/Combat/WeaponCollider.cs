using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public interface IDamageCollider
{
    public void EnableCollider();
    public void DisableCollider();
    public void Initialize(ItemWithAttributes item);
}


public class WeaponCollider : MonoBehaviour, IDamageCollider
{
    [SerializeField] private AttributeBaseSO particleAttribute;
    [SerializeField] private LayerMask layerToIgnore;
    private CharacterStats characterStats;
    private ItemWithAttributes weaponItem;
    private Collider damageCollider;

    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>();
        characterStats = GetComponentInParent<CharacterStats>();
        Debug.Log("coll onenable");
    }

    public void Initialize(ItemWithAttributes weaponItem)
    {
        this.weaponItem = weaponItem;
    }

    public void EnableCollider()
    {
        damageCollider.enabled = true;
        hasCollided = false;
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

    [SerializeField]
    private bool hasCollided;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);

        //prevent damaging yourself
        var stats = other.gameObject.GetComponent<CharacterStats>();
        if (stats == characterStats) return;
        if (hasCollided) return;
        hasCollided = true;
        // if (other.gameObject.layer != LayerMask.NameToLayer("Ground")) return;

        var damageable = other.collider.GetComponent<IDamageable>();
        var shieldCollider = other.collider.GetComponent<ShieldCollider>();
        var attackDamage = characterStats.ActiveModifiers[StatType.AttackDamage];

        if (shieldCollider)
        {
            var shieldDamageReduction = shieldCollider.DamageReduction;
            attackDamage = shieldCollider.ApplyShieldReduction(attackDamage);
            var shieldUser = shieldCollider.gameObject.GetComponentInParent<IDamageable>();
            shieldUser?.Damage(attackDamage);
            Debug.Log("Hit Shield " + shieldUser);

        }
        else if (damageable != null)
        {
            Debug.Log("Hit Character " + damageable);
            damageable.Damage(attackDamage);
        }

        var hitPoint = other.contacts[0].point;
        var hitNormal = other.contacts[0].normal;

        if (!weaponItem) return;
        var impactParticle = weaponItem.GetAttribute<ParticleAttributeData>(particleAttribute)?.value;
        if (impactParticle != null)
        {
            var ps = Instantiate(impactParticle, hitPoint + hitNormal / 7, Quaternion.identity);
            ps.Play();
        }
    }

    private static float ApplyShieldReduction(float attackDamage, float shieldDamageReduction)
    {
        return Math.Max(0, attackDamage - attackDamage * shieldDamageReduction);
    }
}