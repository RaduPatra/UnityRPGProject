using System;
using UnityEngine;

public class PlayerHealthLoader : MonoBehaviour
{
    private Health health;
    private GameObjectId gameObjectId;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnHealthChange += SaveHealth;
        gameObjectId = GetComponentInParent<GameObjectId>();
        SaveSystem.OnLoad += LoadHealth;
        SaveSystem.OnBeforeLoad += BeforeLoad;
        SaveSystem.OnInitSaveData += InitSaveData;
    }

    private void SaveHealth(float amount)
    {
        SaveData.Current.healthData = amount;//!
    }

    private void OnDestroy()
    {
        SaveSystem.OnLoad -= LoadHealth;
        SaveSystem.OnBeforeLoad -= BeforeLoad;
        SaveSystem.OnInitSaveData -= InitSaveData;
    }

    private void InitSaveData()
    {
        // SaveData.Current.healthValueData[gameObjectId.Id] = health.CurrentHealth;
        SaveData.Current.healthData = health.CurrentHealth;
    }

    private void BeforeLoad(SaveData obj)
    {
        health.SetupEvents();
    }

    private void LoadHealth(SaveData saveData)
    {
        // var thisObjectHealth = saveData.healthValueData[gameObjectId.Id];
        var thisObjectHealth = saveData.healthData;
        health.SetHealth(thisObjectHealth);
        health.IsDead = false;
    }
}