using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    public EnemyIdleState idleState;
    public EnemyAttackState attackState;
    public float stopDistance = 3f;
    public float attackDistance = 3f;
    public float rotationSpeed = 100f;
    public float chaseAngle = 120f;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    public float horizontalValue = 0;

    public bool isStrafing = false;

    public override EnemyState Tick(EnemyAIOld enemy)
    {
        enemy.enemyFOV.fovAngle = chaseAngle;
        // if (enemy.enemyFOV.IsTargetInFov(enemy.enemyCurrentTarget))
        {
            
            enemy.animator.SetFloat(Vertical, 1f, .2f, Time.deltaTime);
            enemy.animator.SetFloat(Horizontal, horizontalValue, .2f, Time.deltaTime);

            var targetPos = enemy.enemyCurrentTarget.position;
            enemy.lastTargetPosition = targetPos;
            enemy.navMeshAgent.SetDestination(targetPos);

            var direction = (targetPos - enemy.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation.x = 0;
            targetRotation.z = 0;
            enemy.transform.rotation =
                Quaternion.Slerp(enemy.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        horizontalValue = isStrafing ? 1 : 0;
        
        /*else
        {
            //go to search
            enemy.navMeshAgent.SetDestination(enemy.lastTargetPosition);
        }*/

        
        
        /*var dist = Vector3.Distance(enemy.transform.position, enemy.enemyCurrentTarget.transform.position);
        
        if (dist < attackDistance)
        {
            Debug.Log("enter attack");
            return attackState;
        }*/
        
        

        return this;

        /*
        var dist = Vector3.Distance(enemy.transform.position, enemy.enemyCurrentTarget.transform.position);

        if (dist < stopDistance)
        {
            Debug.Log("stopdist " + dist);
            enemy.navMeshAgent.isStopped = true;

            return this;
        }
        else
        {
            // enemy.navMeshAgent.isStopped = false;
            // enemy.navMeshAgent.SetDestination(enemy.enemyCurrentTarget.position);
            // return this;
        }*/
    }
}