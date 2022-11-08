using UnityEngine;

public class CombatDicisionAction : StateAction
{
    public EnemyAI enemy;

    public CombatDicisionAction(Component context)
    {
        enemy = context.GetComponent<EnemyAI>();
    }

    public override void OnUpdate()
    {
        if (enemy.currentAttack == null && !enemy.IsInAttackCooldown)
            GetAttack();
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
        enemy.enemyStaggered = false;
    }

    private void GetAttack()
    {
        // Debug.Log("Play enemy Combo");
        EnemyAttack currentAttack = null;
        var angleToTarget = enemy.GetAngleToTarget();
        for (var i = 0; i < 10; i++)
        {
            var randComboIndex = Random.Range(0, enemy.enemyAIConfig.enemyAttacks.Count);
            currentAttack = enemy.enemyAIConfig.enemyAttacks[randComboIndex];
            if (currentAttack == enemy.currentAttack) continue;

            if (enemy.GetDistToTarget() > currentAttack.attackMinRange
                && enemy.GetDistToTarget() < currentAttack.attackMaxRange
                && angleToTarget < currentAttack.attackAngle / 2)
                break;

            currentAttack = null;
        }

        enemy.currentAttack = currentAttack;
    }
}


/*private void GetAttack()
     {
         Debug.Log("Play enemy Combo");
         EnemyAttack currentCombo = null;
         var iterations = 0;
         do
         {
             var randComboIndex = Random.Range(0, enemy.comboAnimNames.Count);
             currentCombo = enemy.enemyAttacks[randComboIndex];
             iterations++;
         } while (currentCombo == lastAttack && iterations < 10);
 
         if (currentCombo == null) return;
         enemy.characterAnimator.PlayAnimation(currentCombo.attackAnimStateName, true);
         enemy.characterAnimator.SetCanDoComboBool(false);
         lastAttack = currentCombo;
     }*/