using UnityEngine;

public class LookToTargetAction : StateAction
{
    private EnemyAI enemy;

    public LookToTargetAction(Component context)
    {
        enemy = context.GetComponent<EnemyAI>();
    }

    public override void OnUpdate()
    {
        if (!enemy.characterAnimator.GetCanRotate()) return;

        var targetPos = enemy.currentTargetInfo.targetTransform.position;
        var direction = (targetPos - enemy.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0;
        targetRotation.z = 0;
        enemy.transform.rotation =
            Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.enemyAIConfig.rotationSpeed * Time.deltaTime);
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
        // enemy.CanRotate = () => true;
    }
}