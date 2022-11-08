using UnityEngine;
using UnityEngine.UI;

public class WorldHealthUI : MonoBehaviour
{
    [SerializeField] private GameObjectEventChannel canvasGoEventChannel;

    [SerializeField] private Slider healthSlider;
    private Health health;

    private void Awake()
    {
        health = GetComponentInParent<Health>();
        health.OnHealthChange += UpdateHealth;
        health.OnDestroyed += HandleDeath;
        canvasGoEventChannel.Listeners += SetupCanvas;
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        health.OnHealthChange -= UpdateHealth;
        health.OnDestroyed -= HandleDeath;
        canvasGoEventChannel.Listeners -= SetupCanvas;
    }

    private void UpdateHealth(float amount)
    {
        healthSlider.value = health.maxHealth != 0 ? amount / health.maxHealth : 0;
    }

    private void SetupCanvas(GameObject canvasGO)
    {
        transform.SetParent(canvasGO.transform);
    }
}