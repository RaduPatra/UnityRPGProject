using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DamageHandler : MonoBehaviour //todo rename to damage handler
{
    [SerializeField] public float maxStagger;
    [SerializeField] public float currentStagger;

    // [SerializeField] public float poiseDrainTest;
    [SerializeField] private float staggerRegenRate;
    [SerializeField] private float staggerRegenStartTime;

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float particleYOffset = 1f;

    private Health health;
    private CharacterAnimator characterAnimator;
    private CharacterStats characterStats;
    private float currentStaggerRegenTimer;

    public Action OnStagger;

    private void Awake()
    {
        health = GetComponent<Health>();
        characterAnimator = GetComponent<CharacterAnimator>();
        characterStats = GetComponent<CharacterStats>();
        health.OnDamage += HandleDamage;
        currentStagger = maxStagger;
    }

    private void HandleDamage(DamageInfo damageInfo)
    {
        // SetPoise(currentPoise - poiseDrainTest);

        if (characterStats != null)
        {
            damageInfo.staggerDamageAmount =
                characterStats.CalculateDamageReduction(damageInfo.staggerDamageAmount, StatType.Poise);
        }

        SetStaggerValue(currentStagger - damageInfo.staggerDamageAmount);
        currentStaggerRegenTimer = 0;
        if (currentStagger > 0)
        {
            characterAnimator.PlayAnyStateTrigger(CharacterAnimator.damage, characterAnimator.IsInteracting);
        }
        else
        {
            characterAnimator.PlayAnyStateTrigger(CharacterAnimator.damageStun);
            OnStagger?.Invoke();
            currentStagger = maxStagger;
        }

        PlayDamageParticle();
    }


    private void Update()
    {
        RegenStagger();
    }

    private void RegenStagger()
    {
        currentStaggerRegenTimer += Time.deltaTime;
        if (currentStaggerRegenTimer < staggerRegenStartTime) return;
        var staggerToRegen = currentStagger + staggerRegenRate * Time.deltaTime;
        // Debug.Log(staminaToRegen + " " + staminaRegenRate + " " + Time.deltaTime);
        SetStaggerValue(Mathf.Min(maxStagger, staggerToRegen));
    }

    private void SetStaggerValue(float value)
    {
        currentStagger = value;
    }


    private void PlayDamageParticle()
    {
        var transform1 = transform;
        var blood = Instantiate(particleSystem, transform1.position + new Vector3(0, particleYOffset, 0),
            Random.rotation, transform1);
        blood.Play();
        // particleSystem.Play();
    }
}