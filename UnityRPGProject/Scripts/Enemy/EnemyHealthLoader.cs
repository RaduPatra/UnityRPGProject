using System;
using UnityEngine;

public class EnemyHealthLoader : MonoBehaviour
{
    private Health health;


    private void Awake()
    {
        health = GetComponent<Health>();
        SaveSystem.OnLoad += LoadHealth;
    }

    private void LoadHealth(SaveData saveData)
    {
        // var thisObjectHealth = saveData.healthValueData[gameObjectId.Id];
        health.SetHealth(health.maxHealth);
        //!!! we can not do this because set health will set  the current health save data for the player, instead of object
        //either save every object id health, or make separate health with no save? idk
        health.IsDead = false;
    }
}