using System;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class EnemyLocationManager : MonoBehaviour
{
    public List<string> enemyIds;
    public Transform enemyParent;
    private int aliveEnemyCount;

    private void Awake()
    {
        SaveSystem.OnLoad += DestroyKilledEnemies;
    }

    private void OnDestroy()
    {
        SaveSystem.OnLoad -= DestroyKilledEnemies;
        
    }

    private void Start()
    {
    }

    private void DestroyKilledEnemies(SaveData saveData)
    {
        var enemies = GetComponentsInChildren<EnemyAI>();
        foreach (var enemy in enemies)
        {
            enemy.enemyHealth.OnDeath += UpdateLocation;
            enemyIds.Add(enemy.GetComponent<GameObjectId>().Id);
        }

        aliveEnemyCount = enemyIds.Count;


        if (aliveEnemyCount > 0 && saveData.nonRespawnableEnemies.Contains(enemyIds[0]))
        {
            // Destroy(gameObject);
            aliveEnemyCount = 0;
            OnLocationCleared?.Invoke();
           
            Destroy(enemyParent.gameObject);
            // transform.Clear();
        }
    }


    public Action OnLocationCleared = delegate { };

    private void UpdateLocation()
    {
        aliveEnemyCount--;
        if (aliveEnemyCount == 0)
        {
            SaveData.Current.nonRespawnableEnemies.AddRange(enemyIds);
            OnLocationCleared?.Invoke();
        }
    }
}