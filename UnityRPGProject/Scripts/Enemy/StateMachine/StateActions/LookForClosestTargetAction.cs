using System.Net.Configuration;
using UnityEngine;

public class LookForClosestTargetAction : StateAction
{
    private EnemyAI enemy;

    public LookForClosestTargetAction(Component context)
    {
        enemy = context.GetComponent<EnemyAI>();
    }

    public override void OnUpdate()
    {
        var closestTarget = enemy.enemyFOV.GetClosestTarget();
        if (closestTarget && !enemy.currentTargetInfo.health)
        {
            Debug.Log("Found Target");
            enemy.SetTarget(closestTarget);
            // enemy.lastTargetPosition = closestTarget.position;
        }
    }

    public override void OnStateEnter()
    {
        enemy.currentTargetInfo.health = null;
    }

    public override void OnStateExit()
    {
    }
}