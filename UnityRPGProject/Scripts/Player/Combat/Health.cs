using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int currentHealth;
    private CharacterStats characterStats;
    [SerializeField] private IntEventChannel onHealthChangeEventChannel;
    [SerializeField] private IntEventChannel onMaxHealthChangeEventChannel;

    private void Awake()
    {
        currentHealth = maxHealth;
        characterStats = GetComponentInParent<CharacterStats>();

        if (onHealthChangeEventChannel != null && onMaxHealthChangeEventChannel != null)
        {
            onHealthChangeEventChannel.Raise(currentHealth);
            onMaxHealthChangeEventChannel.Raise(maxHealth);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Math.Min(currentHealth + amount, maxHealth);
        Debug.Log("Heal , current health: " + currentHealth);
    }

    public void Damage(int amount)
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