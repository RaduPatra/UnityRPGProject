using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem impactParticle;
    [SerializeField] private int unitsAwayFromNormal = 5;

    //temp variable - do damage based on stats in the future
    [SerializeField] private float projectileDamage = 5;
    [SerializeField] private AttributeBaseSO projectileDamageAttribute;
    private bool hasCollided;
    public CharacterStats casterStats;
    public CharacterTeam characterTeam;
    [SerializeField] private LayerMask layerToIgnore;


    public void Initialize(ItemWithAttributes item, GameObject projectileCaster)
    {
        if (item)
        {
            projectileDamage = 0;
            var projectileData = item.GetAttribute<FloatAttributeData>(projectileDamageAttribute);
            if (projectileData != null) projectileDamage = projectileData.value;
        }

        casterStats = projectileCaster.GetComponent<CharacterStats>();
        characterTeam = projectileCaster.GetComponent<CharacterTeam>();
        projectileCaster.GetComponent<CollisionIgnorer>().IgnoreCollisionWithCollider(GetComponent<Collider>());
    }

    private void Start()
    {
        Destroy(gameObject, 7f);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        var hitPoint = other.contacts[0].point;
        var hitNormal = other.contacts[0].normal;

        var ps = Instantiate(impactParticle, hitPoint + hitNormal / unitsAwayFromNormal, Quaternion.identity);
        ps.Play();
        
        
        var hitTeam = other.gameObject.GetComponentInParent<CharacterTeam>();
        if (hitTeam != null && hitTeam.teamType == characterTeam.teamType) return;

        if (hasCollided) return;
        hasCollided = true;

        /*if ((layerToIgnore & (1 << other.gameObject.layer)) != 0)
        {
            Debug.Log("same layer");
            return;
        }*/

        Debug.Log(other + "   collision enter");
        

        // var characterStats = other.gameObject.GetComponent<CharacterStats>();
        var damageable = other.collider.GetComponentInParent<IDamageable>();
        var totalDamage = CalculateProjectileDamage(casterStats.ActiveModifiers[StatType.MagicDamage]);

        var damageInfo = new DamageInfo
        {
            damageAmount = totalDamage,
            damager = casterStats.gameObject
        };
        damageable?.Damage(damageInfo);

        // Destroy(gameObject);
    }

    private float CalculateProjectileDamage(float magicStatValue)
    {
        var totalDamage = projectileDamage * (1 + magicStatValue / 100);
        return totalDamage;
    }
}