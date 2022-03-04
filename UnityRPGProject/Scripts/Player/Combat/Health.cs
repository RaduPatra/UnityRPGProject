using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth;
    private float currentHealth;
    private CharacterStats characterStats;
    [SerializeField] private FloatEventChannel onHealthChangeEventChannel;
    [SerializeField] private FloatEventChannel onMaxHealthChangeEventChannel;

    private void Awake()
    {
        currentHealth = maxHealth;
        characterStats = GetComponentInParent<CharacterStats>();
    }

    private void Start()
    {
        if (onHealthChangeEventChannel != null && onMaxHealthChangeEventChannel != null)
        {
            onMaxHealthChangeEventChannel.Raise(maxHealth);
            onHealthChangeEventChannel.Raise(currentHealth);
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Math.Min(currentHealth + amount, maxHealth);
        Debug.Log("Heal , current health: " + currentHealth);
    }

    public void Damage(float amount)
    {
        amount = characterStats != null ? characterStats.CalculateDamageReduction(amount) : amount;

        currentHealth = Math.Max(currentHealth - amount, 0);

        if (onHealthChangeEventChannel != null)
            onHealthChangeEventChannel.Raise(currentHealth);

        Debug.Log("Damage , current health: " + currentHealth);

        if (currentHealth == 0)
        {
            //handle death
            Debug.Log("Ded");
        }
    }
}