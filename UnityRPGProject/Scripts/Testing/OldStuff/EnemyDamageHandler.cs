using System;
using UnityEngine;


public class EnemyDamageHandler : MonoBehaviour
{
    private CharacterAnimator characterAnimator;
    private ParticleSystem particleSystem;
    private Health enemyHealth;

    public bool gotDamaged;

    private void Awake()
    {
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDamage += HandleDamage;
        characterAnimator = GetComponent<CharacterAnimator>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void HandleDamage(DamageInfo damageInfo)
    {
        if (!enemyHealth.IsDead)
        {
            characterAnimator.PlayAnyStateTrigger(CharacterAnimator.damage);
        }
    }

    private void PlayDamageParticle()
    {
        particleSystem.Play();
    }
}