using System;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;


public abstract class StateMachineOwner : MonoBehaviour
{
}

public class EnemyAI : StateMachineOwner
{
    public StateType currentState;
    private StateMachine stateMachine;
    private StateFactory stateFactory;
    public EnemyFOV enemyFOV;
    public NavMeshAgent navMeshAgent;
    public Health enemyHealth;

    public EnemyAIConfig enemyAIConfig;
    public CharacterAnimator characterAnimator;
    /*public Vector3 lastTargetPosition;
    public Transform closestTarget;*/

    public Transform parentWaypointTransform;
    [Header("Debug")]
    public float currentStrafeDirectionChangeCooldown = 2;
    public float currentAttackCooldown = 3f;
    public EnemyCurrentTargetInfo currentTargetInfo;
    public EnemyAttack currentAttack;
    public bool targetDied;
    public DamageInfo lastDamageInfo;
    public float currDistToTargetTest;
    public float currAngleToTargetTest;

    private bool isEnemyDead = false;
    private Vector3 directionFromEnemyToTarget;

    public bool IsInAttackCooldown => currentAttackCooldown > 0;
    public bool IsInStrafeDirectionChangeCooldown => currentStrafeDirectionChangeCooldown > 0;


    private void Awake()
    {
        enemyFOV = GetComponent<EnemyFOV>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<CharacterAnimator>();
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDeath += HandleEnemyDeath;
        enemyHealth.OnDamage += HandleDamage;
        GetComponent<DamageHandler>().OnStagger += HandleStagger;
        // navMeshAgent.updateRotation = false;
        currentStrafeDirectionChangeCooldown += Random.value * 2;
        
        stateMachine = new StateMachine();
    }

    public bool enemyStaggered;

    private void HandleStagger()
    {
        enemyStaggered = true;
    }

    private void Start()
    {
        stateFactory = new StateFactory(this);
        SetupStates();
    }

    private void SetupStates()
    {
        // var idleState = stateFactory.GetState(StateType.Idle);
        var patrolState = stateFactory.GetState(StateType.Patrol);
        var chaseState = stateFactory.GetState(StateType.Chase);
        var chaseStrafeState = stateFactory.GetState(StateType.ChaseStrafe);
        var strafeState = stateFactory.GetState(StateType.Strafe);
        var combatState = stateFactory.GetState(StateType.Combat);
        var attackState = stateFactory.GetState(StateType.Attack);
        var deadState = stateFactory.GetState(StateType.Dead);


        stateMachine.AddTransition(patrolState, chaseState, IdleChaseCondition);
        stateMachine.AddTransition(chaseState, combatState, CombatChaseCondition);

        //combat states
        stateMachine.AddTransition(combatState, attackState, AttackCondition);
        stateMachine.AddTransition(combatState, strafeState, StrafeCondition);
        stateMachine.AddTransition(combatState, chaseStrafeState, CombatChaseCondition);


        stateMachine.AddTransition(attackState, combatState, 
            () => !characterAnimator.IsInteracting || enemyStaggered);
        stateMachine.AddTransition(chaseStrafeState, combatState, BackToCombatCondition);
        stateMachine.AddTransition(strafeState, combatState, () => !StrafeCondition());

        //back to chase
        stateMachine.AddTransition(combatState, chaseState, () => !CombatChaseCondition());
        stateMachine.AddTransition(chaseState, patrolState, () => GetDistToTarget() > enemyAIConfig.backToIdleDistance);


        stateMachine.AddAnyStateTransition(deadState, () => enemyHealth.IsDead && deadState.stateType != currentState);
        stateMachine.AddAnyStateTransition(patrolState, () => targetDied && !characterAnimator.IsInteracting);
        stateMachine.SwitchToState(patrolState);
    }

    private bool IsTargetDead()
    {
        var isDead = false;
        if (currentTargetInfo.health) isDead = currentTargetInfo.health.IsDead;
        return isDead;
    }

    private void HandleEnemyDeath()
    {
        isEnemyDead = true;
    }

    private void Update()
    {
        currDistToTargetTest = GetDistToTarget();
        currAngleToTargetTest = GetAngleToTarget();
        if (IsTargetDead()) targetDied = true;

        currentStrafeDirectionChangeCooldown -= Time.deltaTime;
        currentAttackCooldown -= Time.deltaTime;
        if (stateMachine.currentState != null) currentState = stateMachine.currentState.stateType;
        stateMachine.OnUpdate();
    }

    public float GetDistToTarget()
    {
        return !currentTargetInfo.targetTransform
            ? default
            : Vector3.Distance(currentTargetInfo.targetTransform.position, transform.position);
    }

    public float GetAngleToTarget()
    {
        if (!currentTargetInfo.targetTransform) return default;
        var targetPos = currentTargetInfo.targetTransform.position + new Vector3(0, 1, 0);
        var transform1 = transform;
        directionFromEnemyToTarget = (targetPos - transform1.position).normalized;
        var angleToTarget = Vector3.Angle(transform1.forward, directionFromEnemyToTarget);
        return angleToTarget;
    }

    private void HandleDamage(DamageInfo damageInfo)
    {
        lastDamageInfo = damageInfo;
        var collisionIgnorer = lastDamageInfo.damager.GetComponent<CollisionIgnorer>();
        if (!collisionIgnorer) return;
        foreach (var coll in collisionIgnorer.allColliders)
        {
            var colliderLayer = coll.gameObject.layer;
            var isTargetLayer = Utils.IsInLayerMask(colliderLayer, enemyFOV.targetColliders);
            if (isTargetLayer)
            {
                SetTarget(coll.gameObject.transform);
                return;
            }
        }
    }

    public void SetTarget(Transform target)
    {
        currentTargetInfo.targetTransform = target;
        currentTargetInfo.health = target.GetComponentInParent<Health>();
    }

    #region Conditions

    private bool BackToCombatCondition()
    {
        var a = !CombatChaseCondition();
        var b = AttackCondition();
        var c = StrafeCondition();
        return a || b || c;
    }

    private bool AttackCondition()
    {
        return currentAttack != null && IsTargetInFov();
    }

    private bool StrafeCondition()
    {
        return GetDistToTarget() < enemyAIConfig.strafeDistance && IsInAttackCooldown && IsTargetInFov();
    }

    private bool IsTargetInFov()
    {
        return currentTargetInfo != null && enemyFOV.IsTargetInFov(currentTargetInfo.targetTransform);
    }

    private bool IdleChaseCondition()
    {
        return currentTargetInfo != null && enemyFOV.IsTargetValid(currentTargetInfo.targetTransform);
    }

    private bool CombatChaseCondition()
    {
        var isInCombatDistance = GetDistToTarget() < enemyAIConfig.combatDistance;
        return isInCombatDistance && IsTargetInFov();
    }

    #endregion

    private void OnDestroy()
    {
        Debug.Log("enemy destroyed");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 5);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, directionFromEnemyToTarget * 5f);
    }
}

[Serializable]
public class EnemyCurrentTargetInfo
{
    public Transform targetTransform;
    public Health health;
}