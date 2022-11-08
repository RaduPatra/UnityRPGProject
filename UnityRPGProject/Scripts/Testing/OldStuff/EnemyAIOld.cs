using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;


public class EnemyAIOld : MonoBehaviour
{
    public EnemyState currentState;
    public Transform enemyCurrentTarget;
    public Vector3 lastTargetPosition;
    public EnemyFOV enemyFOV;
    public Animator animator;
    public CharacterAnimator characterAnimator;

    [ReadOnly]
    public NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<CharacterAnimator>();
        enemyFOV = GetComponentInParent<EnemyFOV>();
        animator = GetComponent<Animator>();
        navMeshAgent.updateRotation = false;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (currentState != null)
        {
            var nextState = currentState.Tick(this);
            if (nextState)
            {
                currentState = nextState;
            }
        }
    }

    public void SwitchToNextState()
    {
        
    }
}