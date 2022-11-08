using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : StateAction
{
    private readonly EnemyAI enemy;
    private readonly List<Transform> waypoints = new List<Transform>();
    private float currentWaypointTimer;
    private int currentWaypointIndex;

    public PatrolAction(Component context)
    {
        enemy = context.GetComponent<EnemyAI>();
        for (var i = 0; i < enemy.parentWaypointTransform.childCount; i++)
        {
            waypoints.Add(enemy.parentWaypointTransform.GetChild(i));
        }
    }

    private bool destinationReached;

    public override void OnUpdate()
    {
        currentWaypointTimer -= Time.deltaTime;

        if (enemy.characterAnimator.IsInteracting)
        {
            enemy.navMeshAgent.velocity = Vector3.zero;
            return;
        }
        if (currentWaypointTimer < 0 && destinationReached)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            enemy.currentTargetInfo.targetTransform = waypoints[currentWaypointIndex];
            destinationReached = false;
            enemy.navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        var distFromWaypoint = Vector3.Distance(enemy.transform.position, waypoints[currentWaypointIndex].position);
        if (distFromWaypoint < enemy.enemyAIConfig.destReachedDistance)
        {
            enemy.characterAnimator.animator.SetFloat(CharacterAnimator.Vertical, 0, .2f, Time.deltaTime);
            if (destinationReached) return;
            destinationReached = true;
            currentWaypointTimer = enemy.enemyAIConfig.switchWaypointTimer;
        }
        else
        {
            enemy.characterAnimator.animator.SetFloat(CharacterAnimator.Vertical, 1f, .2f, Time.deltaTime);
        }
    }

    public override void OnStateEnter()
    {
        enemy.targetDied = false;
        enemy.enemyFOV.fovAngle = enemy.enemyAIConfig.idleChaseAngle;
        enemy.enemyFOV.fovPitch = enemy.enemyAIConfig.idlePitch;
        var closestWaypoint = GetClosestWaypoint();
        destinationReached = false;
        currentWaypointIndex = closestWaypoint.GetSiblingIndex();
        currentWaypointTimer = 9999f;
        enemy.currentTargetInfo.targetTransform = waypoints[currentWaypointIndex];
        var x = enemy.navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        Debug.Log("set dest " + x);
    }

    private Transform GetClosestWaypoint()
    {
        return Utils.GetClosestTransformInList(enemy.transform, waypoints);
    }

    public override void OnStateExit()
    {
        enemy.navMeshAgent.ResetPath();
    }
}