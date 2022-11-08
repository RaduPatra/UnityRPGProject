using System;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyChaseState chaseState;
    // public float rayLength = 10f;
    // public LayerMask playerLayerMask;


    public Transform closestTarget;
    public override EnemyState Tick(EnemyAIOld enemy)
    {
        closestTarget = enemy.enemyFOV.GetClosestTarget();
        if (closestTarget)
        {
            Debug.Log("Found Target");
            enemy.enemyCurrentTarget = closestTarget;
            enemy.lastTargetPosition = closestTarget.position;
            return chaseState;
        }
        return this;
    }

}