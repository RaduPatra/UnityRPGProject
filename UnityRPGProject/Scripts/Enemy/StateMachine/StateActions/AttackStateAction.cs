using UnityEngine;

public class AttackStateAction : StateAction
{
    public EnemyAI enemy;
    public int combosPerformed;
    public string lastAttack;

    public AttackStateAction(Component context)
    {
        enemy = context.GetComponent<EnemyAI>();
        enemy.characterAnimator.animationEvents.shootProjectileAnimEvent += ShootProjectile;
    }
    
    public override void OnUpdate()
    {
        // Debug.Log("attack action update");
        // var x = enemy.characterAnimator.GetCanDoComboBool();
        // Debug.Log(x);
        if (canCombo && enemy.characterAnimator.GetCanDoComboBool() && combosPerformed < enemy.enemyAIConfig.maxComboNumber)
        {
            HandleComboRand();
            DecideCombo();
            combosPerformed++;
        }
    }

    public override void OnStateEnter()
    {
        // enemy.characterAnimator.OverrideDefaultAnimation(enemy.currentAttack.overrideController);
        // enemy.characterAnimator.OverrideDefaultAnimationSimple(enemy.currentAttack.overrideController);

        Debug.Log("attack enter");

        enemy.currentAttack.Attack(enemy);
        combosPerformed = 0;
        DecideCombo();
    }

    public override void OnStateExit()
    {
        Debug.Log("attack exit");
        enemy.currentAttackCooldown = enemy.enemyAIConfig.attackCooldown + Random.value * 1.5f;;
        enemy.currentAttack = null;
        enemy.enemyStaggered = false;
    }

    private bool canCombo;

    private void DecideCombo()
    {
        var rand = Random.Range(0, 100);
        canCombo = enemy.enemyAIConfig.comboChance >= rand;
    }

    private void HandleComboRand()
    {
        enemy.currentAttack = enemy.currentAttack.nextComboAttack;
        //check if in right combo distance, angle before attacking
        if (enemy.currentAttack != null)
            enemy.currentAttack.Attack(enemy);
    }

    private void ShootProjectile()
    {
        if (enemy.currentAttack != null)
            enemy.currentAttack.FinalizeAttack(enemy);
    }
}