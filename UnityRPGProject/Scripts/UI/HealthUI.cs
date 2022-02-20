using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private IntEventChannel onHealthChangeEventChannel;
    [SerializeField] private IntEventChannel onMaxHealthChangeEventChannel;
    [SerializeField] private TextMeshProUGUI healthStatText;
    [SerializeField] private Slider healthSlider;

    private int currentHealth;
    private int maxHealth;

    private void Awake()
    {
        onHealthChangeEventChannel.Listeners += UpdateHealth;
        onMaxHealthChangeEventChannel.Listeners += UpdateMaxHealth;
    }

    private void UpdateHealth(int amount)
    {
        healthStatText.text = "Health: " + amount;
        healthSlider.value = maxHealth != 0 ? amount / maxHealth : 0;
    }

    private void UpdateMaxHealth(int max)
    {
        maxHealth = max;
    }
}