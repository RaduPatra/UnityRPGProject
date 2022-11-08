using System;
using UnityEngine;

public class EnemyAttackState : EnemyState
{

    public EnemyChaseState chaseState;
    public float attackDistance = 4f;

    private float attackCooldown = 5f;
    public float currentAttackCooldown = 5f;
    public override EnemyState Tick(EnemyAIOld enemy)
    {

        if (currentAttackCooldown < 0)
        {
            Attack(enemy);
            currentAttackCooldown = attackCooldown;
            return chaseState;
        }
        
        var dist = Vector3.Distance(enemy.transform.position, enemy.enemyCurrentTarget.transform.position);

        if (dist > attackDistance)
            return chaseState;
        
        
        return this;
    }

    public void Attack(EnemyAIOld enemy)
    {
        enemy.characterAnimator.PlayAnimation(CharacterAnimator.lightAttack1, true);
    }

    private void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
    }
}