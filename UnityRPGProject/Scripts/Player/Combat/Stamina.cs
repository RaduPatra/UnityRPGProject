using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaRegenRate;
    [SerializeField] private float staminaRegenStartTime;
    [SerializeField] private FloatEventChannel onStaminaChangeEventChannel;
    [SerializeField] private FloatEventChannel onMaxStaminaChangeEventChannel;
    private CharacterStats characterStats;
    private float currentStaminaRegenTimer;


    public float CurrentStamina { get; private set; }

    private void Awake()
    {
        CurrentStamina = maxStamina;
        characterStats = GetComponentInParent<CharacterStats>();
    }

    private void Start()
    {
        SetupEvents();
    }

    private void SetupEvents()
    {
        if (onStaminaChangeEventChannel == null || onMaxStaminaChangeEventChannel == null) return;
        onMaxStaminaChangeEventChannel.Raise(maxStamina);
        onStaminaChangeEventChannel.Raise(CurrentStamina);
    }
    
    private void Update()
    {
        RegenStamina();
    }

    private void RegenStamina()
    {
        currentStaminaRegenTimer += Time.deltaTime;
        if (currentStaminaRegenTimer < staminaRegenStartTime) return;
        var staminaToRegen = CurrentStamina + staminaRegenRate * Time.deltaTime;
        // Debug.Log(staminaToRegen + " " + staminaRegenRate + " " + Time.deltaTime);
        SetStamina(Mathf.Min(maxStamina, staminaToRegen));
    }

    public void Damage(float amount)
    {
        currentStaminaRegenTimer = 0;
        SetStamina(Math.Max(CurrentStamina - amount, 0));
        // Debug.Log("Damage , current stamina: " + CurrentStamina);
        /*if (currentStamina == 0)
        {
            //handle death
            OnDeath?.Invoke();
            Debug.Log("Ded");
        }*/
    }

    private void SetStamina(float amount)
    {
        CurrentStamina = amount;
        if (onStaminaChangeEventChannel != null)
            onStaminaChangeEventChannel.Raise(CurrentStamina);
    }
    
    public void DrainItemStamina(ItemWithAttributes item, AttributeBaseSO staminaConsumptionAttribute)
    {
        float staminaConsumption = 0;
        var staminaConsumptionData = item.GetAttribute<FloatAttributeData>(staminaConsumptionAttribute);
        if (staminaConsumptionData != null) staminaConsumption = staminaConsumptionData.value;
        Damage(staminaConsumption);
    }
    
}