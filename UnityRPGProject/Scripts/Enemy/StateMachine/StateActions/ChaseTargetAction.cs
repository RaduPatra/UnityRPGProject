using UnityEngine;

public class ChaseTargetAction : StateAction
{
    private static readonly int Vertical = Animator.StringToHash("Vertical");

    private EnemyAI enemy;

    public ChaseTargetAction(Component context)
    {
        enemy = context.GetComponent<EnemyAI>();
    }

    public override void OnUpdate()
    {
        // if (enemy.enemyFOV.IsTargetInFov(enemy.enemyCurrentTarget))
        if (enemy.characterAnimator.IsInteracting)
        {
            enemy.navMeshAgent.velocity = Vector3.zero;
            return;
        }

        enemy.characterAnimator.animator.SetFloat(Vertical, 1f, .2f, Time.deltaTime);
        var targetPos = enemy.currentTargetInfo.targetTransform.position;
        // enemy.lastTargetPosition = targetPos;
        enemy.navMeshAgent.SetDestination(targetPos);
    }

    public override void OnStateEnter()
    {
        enemy.enemyFOV.fovAngle = enemy.enemyAIConfig.chaseAngle;
        enemy.enemyFOV.fovPitch = enemy.enemyAIConfig.chasePitch;

        enemy.navMeshAgent.isStopped = false;
    }

    public override void OnStateExit()
    {
        Debug.Log("chase exit");
        enemy.characterAnimator.animator.SetFloat(Vertical, 0, .2f, Time.deltaTime);
        // enemy.animator.SetFloat(Horizontal, 0, .2f, Time.deltaTime);
        // enemy.navMeshAgent.SetDestination(enemy.transform.position);
        enemy.navMeshAgent.ResetPath();

    }
}