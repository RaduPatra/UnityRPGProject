using UnityEngine;

[CreateAssetMenu(fileName = "New enemy attack", menuName = "Enemy Attacks/MeleeAttack", order = 1)]

public class MeleeAttack : EnemyAttack
{
    public override void Attack(EnemyAI enemy)
    {
        base.Attack(enemy);
        
        
        
    }
}