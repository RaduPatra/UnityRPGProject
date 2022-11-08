using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private FloatEventChannel onHealthChangeEventChannel;
    [SerializeField] private FloatEventChannel onMaxHealthChangeEventChannel;
    [SerializeField] private TextMeshProUGUI healthStatText;
    [SerializeField] private Slider healthSlider;

    private float currentHealth;
    private float maxHealth;

    private void Awake()
    {
        onHealthChangeEventChannel.Listeners += UpdateHealth;
        onMaxHealthChangeEventChannel.Listeners += UpdateMaxHealth;
    }

    private void UpdateHealth(float amount)
    {
        healthStatText.text = "" + (int)amount;
        healthSlider.value = maxHealth != 0 ? amount / maxHealth : 0;
    }

    private void UpdateMaxHealth(float max)
    {
        maxHealth = max;
    }
}

/*public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemyGOs;
    private void Awake()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            enemyGOs.Add(transform.GetChild(i).gameObject);
        }

    }
}*/