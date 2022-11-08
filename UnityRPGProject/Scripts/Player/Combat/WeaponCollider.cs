using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public interface IDamageCollider
{
    public void EnableCollider();

    public void DisableCollider();

    // public void DisableCollider(AnimationEvent evt);
    public void Initialize(ItemWithAttributes item);
}


public class WeaponCollider : MonoBehaviour, IDamageCollider
{
    [SerializeField] private AttributeBaseSO particleAttribute;
    [SerializeField] private AttributeBaseSO poiseAttribute;
    [SerializeField] private LayerMask layerToIgnore;
    private CharacterStats characterStats;

    [SerializeField] private ItemWithAttributes weaponItem;
    private CharacterAnimator characterAnimator;
    private CollisionIgnorer collisionIgnorer;
    private CharacterTeam characterTeam;
    private Collider damageCollider;
    public TeamType teamToIgnore;

    private void Awake()
    {
        damageCollider = GetComponent<BoxCollider>();
        characterStats = GetComponentInParent<CharacterStats>();
        characterAnimator = GetComponentInParent<CharacterAnimator>();
        collisionIgnorer = GetComponentInParent<CollisionIgnorer>();
        characterTeam = GetComponentInParent<CharacterTeam>();

        Debug.Log("coll onenable");
    }

    private void Start()
    {
        // collisionIgnorer.IgnoreCollisionWithCollider(damageCollider);
    }

    public void DisableCollider(AnimationEvent evt)
    {
        var clipInfo = evt.animatorClipInfo;
        Debug.Log(clipInfo.clip.name);
        Debug.Log(clipInfo.weight);
        if (clipInfo.weight < 1) return;
        damageCollider.enabled = false;
        Debug.Log("disable collider");
    }

    public void Initialize(ItemWithAttributes weaponItem)
    {
        this.weaponItem = weaponItem;
        collisionIgnorer.IgnoreCollisionWithCollider(damageCollider);
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

    public bool hasCollided;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);
        Debug.Log(other.gameObject.layer);

        //prevent damaging yourself or your team
        var hitTeam = other.gameObject.GetComponentInParent<CharacterTeam>();
        if (hitTeam != null && characterTeam != null && hitTeam.teamType == characterTeam.teamType) return;

        if (hasCollided)
        {
            Debug.Log("has collided ");
            return;
        }

        // hasCollided = true;

        var damageable = other.collider.GetComponentInParent<IDamageable>();
        var shieldCollider = other.collider.GetComponent<ShieldCollider>();
        var attackDamage = characterStats.ActiveModifiers[StatType.AttackDamage];
        var poiseDamageStat = characterStats.ActiveModifiers[StatType.StaggerDamage];

        var poiseDamage = (weaponItem.GetAttribute<FloatAttributeData>(poiseAttribute)?.value ?? 0) + poiseDamageStat;
        var damageInfo = new DamageInfo
        {
            damageAmount = attackDamage,
            staggerDamageAmount = poiseDamage,
            damager = characterStats.gameObject
        };

        if (shieldCollider)
        {
            damageInfo.damageAmount = shieldCollider.ApplyShieldReduction(attackDamage);
            damageInfo.staggerDamageAmount = shieldCollider.ApplyShieldReduction(poiseDamage);
            var shieldUser = shieldCollider.gameObject.GetComponentInParent<IDamageable>();
            shieldUser?.Damage(damageInfo);
            Debug.Log("Hit Shield " + shieldUser);
            hasCollided = true;
        }
        else if (damageable != null)
        {
            Debug.Log("Hit Character " + damageable);
            damageable.Damage(damageInfo);
            hasCollided = true;
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
}

public enum TeamType
{
    Player,
    Enemy
}