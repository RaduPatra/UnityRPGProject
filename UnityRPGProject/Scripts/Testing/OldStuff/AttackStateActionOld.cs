using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class AttackStateActionOld : StateAction
{
    public EnemyAI enemy;
    public int combosPerformed;
    public string lastAttack;

    public AttackStateActionOld(Component context)
    {
        enemy = context.GetComponent<EnemyAI>();
    }

    public override void OnUpdate()
    {
        if (canCombo && enemy.characterAnimator.GetCanDoComboBool() && combosPerformed < enemy.enemyAIConfig.maxComboNumber)
        {
            /*enemy.playerAnimator.PlayAnimation(PlayerAnimator.lightAttack2, true);
            canCombo = false;*/

            // HandleCombo();
            HandleComboRand();
            DecideCombo();
            combosPerformed++;
        }
    }

    public override void OnStateEnter()
    {
        Debug.Log("attack enter");
        // enemy.CanRotate = () => enemy.characterAnimator.GetCanRotate();
        enemy.characterAnimator.PlayAnimation(CharacterAnimator.lightAttack1, true);
        lastAttack = CharacterAnimator.lightAttack1;
        combosPerformed = 0;
        DecideCombo();
    }

    public override void OnStateExit()
    {
        Debug.Log("attack exit");
        enemy.currentAttackCooldown = enemy.enemyAIConfig.attackCooldown;
    }

    private bool canCombo;

    private void DecideCombo()
    {
        var rand = Random.Range(0, 100);
        canCombo = enemy.enemyAIConfig.comboChance >= rand;
    }

    private void HandleCombo()
    {
        Debug.Log("Play enemy Combo");
        switch (lastAttack)
        {
            //todo - add support for custom combo chain list in the future
            case CharacterAnimator.lightAttack1:
                enemy.characterAnimator.PlayAnimation(CharacterAnimator.lightAttack2, true);
                enemy.characterAnimator.SetCanDoComboBool(false);
                lastAttack = CharacterAnimator.lightAttack2;
                break;
            case CharacterAnimator.lightAttack2:
                enemy.characterAnimator.PlayAnimation(CharacterAnimator.lightAttack1, true);
                enemy.characterAnimator.SetCanDoComboBool(false);
                lastAttack = CharacterAnimator.lightAttack1;
                break;
        }
    }

    private void HandleComboRand()
    {
        Debug.Log("Play enemy Combo");
        var currentCombo = "";
        var iterations = 0;
        do
        {
            // var randComboIndex = Random.Range(0, enemy.comboAnimNames.Count);
            // currentCombo = enemy.comboAnimNames[randComboIndex];
            iterations++;
        } while (currentCombo == lastAttack && iterations < 10);

        enemy.characterAnimator.PlayAnimation(currentCombo, true);
        enemy.characterAnimator.SetCanDoComboBool(false);
        lastAttack = currentCombo;
    }
}