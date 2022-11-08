using System.Collections;
using UnityEngine;

public class DeadStateAction : StateAction
{
    private EnemyAI enemy;
    private ParticleSystem particleSystem;
    private CollisionIgnorer collisionIgnorer;
    public DeadStateAction(StateMachineOwner context)
    {
        enemy = context.GetComponent<EnemyAI>();
        particleSystem = context.GetComponent<ParticleSystem>();
        collisionIgnorer = context.GetComponent<CollisionIgnorer>();
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnStateEnter()
    {
        enemy.characterAnimator.PlayAnimation(CharacterAnimator.death, true);
        enemy.StartCoroutine(HandleDeath());
        collisionIgnorer.ToggleAllColliders(false);
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(3f);
        var ps = Object.Instantiate(enemy.enemyAIConfig.deathParticlesPrefab, enemy.transform.position, Quaternion.identity);
        ps.Play();
        yield return new WaitForSeconds(.1f);

        //drop random item
        var dropIndex = Random.Range(0, enemy.enemyAIConfig.enemyDropTable.Count);
        var itemToDrop = enemy.enemyAIConfig.enemyDropTable[dropIndex];
        InventoryHolder.DropItemOnGroundAtTransform(itemToDrop, enemy.enemyAIConfig.dropPrefabAttr, enemy.transform, -1);
        
        Object.Destroy(enemy.gameObject);
        Debug.Log("destroyed");
    }
    public override void OnStateExit()
    {
        enemy.currentTargetInfo.health = null;
    }
}