using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private FloatEventChannel onStaminaChangeEventChannel;
    [SerializeField] private FloatEventChannel onMaxStaminaChangeEventChannel;
    [SerializeField] private TextMeshProUGUI staminaStatText;
    [SerializeField] private Slider healthSlider;

    private float currentStamina;
    private float maxStamina;

    private void Awake()
    {
        onStaminaChangeEventChannel.Listeners += UpdateStamina;
        onMaxStaminaChangeEventChannel.Listeners += UpdateMaxStamina;
    }

    private void UpdateStamina(float amount)
    {
        staminaStatText.text = "" + (int)amount;
        healthSlider.value = maxStamina != 0 ? (amount / maxStamina) : 0;
    }

    private void UpdateMaxStamina(float max)
    {
        maxStamina = max;
    }
}