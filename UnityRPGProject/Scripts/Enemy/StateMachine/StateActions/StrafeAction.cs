using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class StrafeAction : StateAction
{
    private EnemyAI enemy;
    private float strafeSpeed;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    public float horizontalValue = 0;
    public float strafeDirection = 1;
    public bool isStrafing = false;

    public StrafeAction(Component context)
    {
        enemy = context.GetComponent<EnemyAI>();
        strafeSpeed = enemy.enemyAIConfig.chaseStrafeSpeed;
    }

    private Vector3 moveDir;

    public override void OnUpdate()
    {
        if (enemy.characterAnimator.IsInteracting)
        {
            enemy.navMeshAgent.velocity = Vector3.zero; //?
            return;
        }

        DecideStrafeDirection();
        horizontalValue = strafeDirection;
        enemy.characterAnimator.animator.SetFloat(Horizontal, horizontalValue, .2f, Time.deltaTime);

        var enemyTransform = enemy.transform;
        var enemyPos = enemyTransform.position;
        var dirToTarget = (enemy.currentTargetInfo.targetTransform.position - enemyPos).normalized;
        dirToTarget.y = 0;
        var cross = Vector3.Cross(enemyTransform.up, dirToTarget);
        moveDir = cross * strafeDirection;
        if (enemy.GetDistToTarget() < enemy.enemyAIConfig.attackDistance)
        {
            moveDir = -dirToTarget;
        }

        enemy.navMeshAgent.Move(moveDir * enemy.enemyAIConfig.chaseStrafeSpeed * Time.deltaTime);

        // var endPoint = enemyPos + cross * strafeSpeed;
        // enemy.navMeshAgent.SetDestination(endPoint + enemy.enemyCurrentTarget.transform.position);
    }

    private void DecideStrafeDirection()
    {
        var dir = enemy.transform.right * strafeDirection;
        if (Physics.SphereCast(enemy.transform.position, .5f, dir,
            out var hitInfo, enemy.enemyAIConfig.strafeDetectionDistance))
        {
            strafeDirection *= -1;
            enemy.currentStrafeDirectionChangeCooldown =
                enemy.enemyAIConfig.strafeDirectionChangeCooldown + Random.value * 1.5f;
        }

        if (enemy.IsInStrafeDirectionChangeCooldown) return;
        var rand = Random.Range(0, 2);
        if (rand < 1)
            strafeDirection = -1;
        else
            strafeDirection = 1;

        enemy.currentStrafeDirectionChangeCooldown = enemy.enemyAIConfig.strafeDirectionChangeCooldown;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
        Debug.Log("chase strafe exit");
        enemy.characterAnimator.animator.SetFloat(Horizontal, 0, 0, Time.deltaTime);
    }
}