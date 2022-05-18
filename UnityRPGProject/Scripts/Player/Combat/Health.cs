using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private FloatEventChannel onHealthChangeEventChannel;
    [SerializeField] private FloatEventChannel onMaxHealthChangeEventChannel;
    public Action OnDeath;
    private float currentHealth;
    private CharacterStats characterStats;

    private void Awake()
    {
        currentHealth = maxHealth;
        characterStats = GetComponentInParent<CharacterStats>();
    }

    private void Start()
    {
        SetupEvents();
    }

    private void SetupEvents()
    {
        if (onHealthChangeEventChannel == null || onMaxHealthChangeEventChannel == null) return;
        onMaxHealthChangeEventChannel.Raise(maxHealth);
        onHealthChangeEventChannel.Raise(currentHealth);
    }

    public void Heal(float amount)
    {
        currentHealth = Math.Min(currentHealth + amount, maxHealth);
        Debug.Log("Heal , current health: " + currentHealth);
    }

    public void Damage(float amount)
    {
        if (characterStats != null)
        {
            amount = characterStats.CalculateDamageReduction(amount);
        }

        currentHealth = Math.Max(currentHealth - amount, 0);

        if (onHealthChangeEventChannel != null)
            onHealthChangeEventChannel.Raise(currentHealth);

        Debug.Log("Damage , current health: " + currentHealth);

        if (currentHealth == 0)
        {
            //handle death
            OnDeath?.Invoke();
            Debug.Log("Ded");
        }
    }
}