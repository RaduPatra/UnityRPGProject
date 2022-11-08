using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public float maxHealth;
    [SerializeField] private FloatEventChannel onHealthChangeEventChannel;
    [SerializeField] private FloatEventChannel onMaxHealthChangeEventChannel;
    public Action OnDeath;
    public Action OnDestroyed;
    

    public Action<DamageInfo> OnDamage;
    public Action<float> OnHealthChange;
    // private float currentHealth;

    [field: SerializeField, ReadOnly] public float CurrentHealth { get; set; }
    private CharacterStats characterStats;
    private GameObjectId gameObjectId;


    [field: SerializeField] public bool IsDead { get; set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        characterStats = GetComponentInParent<CharacterStats>();
        // gameObjectId = GetComponentInParent<GameObjectId>();
        /*SaveSystem.OnLoad += LoadHealth;
        SaveSystem.OnBeforeLoad += BeforeLoad;
        SaveSystem.OnInitSaveData += InitSaveData;*/
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }

    /*private void BeforeLoad(SaveData obj)
    {
        SetupEvents();
    }

    private void InitSaveData()
    {
        // SaveData.Current.healthData = currentHealth;
        SaveData.Current.healthValueData[gameObjectId.Id] = CurrentHealth;
    }

    private void LoadHealth(SaveData saveData)
    {
        var thisObjectHealth = saveData.healthValueData[gameObjectId.Id];
        SetHealth(thisObjectHealth);
        IsDead = false;
    }*/

    private void Start()
    {
        // SetupEvents();
        // SaveData.Current.healthData = currentHealth;
        
        // OnHealthChange?.Invoke(maxHealth);//temp
    }

    public void SetupEvents()
    {
        if (onHealthChangeEventChannel == null || onMaxHealthChangeEventChannel == null) return;
        onMaxHealthChangeEventChannel.Raise(maxHealth);
        onHealthChangeEventChannel.Raise(CurrentHealth);
    }

    public void Heal(float amount)
    {
        SetHealth(Math.Min(CurrentHealth + amount, maxHealth));
        Debug.Log("Heal , current health: " + CurrentHealth);
        IsDead = false;
    }

    public void HealToMax()
    {
        SetHealth(maxHealth);
        Debug.Log("Heal max , current health: " + CurrentHealth);
        IsDead = false;
    }

    public void SetHealth(float amount)
    {
        CurrentHealth = amount;
        if (onHealthChangeEventChannel != null)
            onHealthChangeEventChannel.Raise(amount);
        OnHealthChange?.Invoke(amount);
    }

    public void Damage(DamageInfo damageInfo)
    {
        if (characterStats != null)
        {
            damageInfo.damageAmount = characterStats.CalculateDamageReduction(damageInfo.damageAmount);
        }

        SetHealth(Math.Max(CurrentHealth - damageInfo.damageAmount, 0));

        Debug.Log("Damage , current health: " + CurrentHealth);

        
        if (IsDead) return;

        if (CurrentHealth <= 0)
        {
            //handle death
            OnDeath?.Invoke();
            IsDead = true;
            Debug.Log("Ded");
        }

        if (IsDead) return;

        OnDamage?.Invoke(damageInfo);
    }
}


[Serializable]
public class DamageInfo
{
    public float damageAmount;
    public float staggerDamageAmount;
    public GameObject damager;
}