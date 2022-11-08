using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New enemy config", menuName = "Enemy AI Config", order = 1)]

public class EnemyAIConfig : ScriptableObject
{
    [Header("Movement")] public float rotationSpeed = 10f;

    [Header("Combat")]
    public float chaseAngle = 360f; 
    public float idleChaseAngle = 360f;


    public float idlePitch = 7f;
    public float chasePitch = 360f;
    
    public float chaseStrafeSpeed = 3f;
    public float strafeDirectionChangeCooldown = 2;
    public float strafeDistance = 5f;
    public float strafeDetectionDistance = 1f;
    public float combatDistance = 10f;

    [Header("Attack")] public float attackDistance = 3f;
    public float attackCooldown = 3f;
    public int comboChance = 50;
    public int maxComboNumber = 2;
    public List<EnemyAttack> enemyAttacks = new List<EnemyAttack>();

    [Header("Patrol")] public float switchWaypointTimer = 4f;
    public float backToIdleDistance = 40f;
    public float destReachedDistance = 1.7f;

    [Header("Death")] public ParticleSystem deathParticlesPrefab;
    public bool canRespawn;
    public List<ItemWithAttributes> enemyDropTable;
    public AttributeBaseSO dropPrefabAttr;
}