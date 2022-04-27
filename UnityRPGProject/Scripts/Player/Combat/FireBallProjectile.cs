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
    private CharacterStats casterStats;

    public void Initialize(ItemWithAttributes item, GameObject projectileCaster)
    {
        projectileDamage = 0;
        var projectileData  = item.GetAttribute<FloatAttributeData>(projectileDamageAttribute);
        if (projectileData != null) projectileDamage = projectileData.value;
        casterStats = projectileCaster.GetComponent<CharacterStats>();
    }
    private void Start()
    {
        Destroy(gameObject, 7f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (hasCollided) return;
        hasCollided = true;
        Debug.Log(other + "   collision enter");
        var hitPoint = other.contacts[0].point;
        var hitNormal = other.contacts[0].normal;


        var ps = Instantiate(impactParticle, hitPoint + hitNormal / unitsAwayFromNormal, Quaternion.identity);
        ps.Play();

        // var characterStats = other.gameObject.GetComponent<CharacterStats>();
        var damageable = other.collider.GetComponent<IDamageable>();
        var totalDamage = CalculateProjectileDamage(casterStats.ActiveModifiers[StatType.MagicDamage]);
        damageable?.Damage(totalDamage);

        Destroy(gameObject);
    }

    private float CalculateProjectileDamage(float magicStatValue)
    {
        var totalDamage = projectileDamage * (1 + magicStatValue / 100);
        return totalDamage;
    }
}