using System;
using UnityEngine;

[Serializable]
public class EnemyAttack : ScriptableObject
{
    public float attackMinRange;
    public float attackMaxRange;
    public float attackAngle;
    public float animTransitionTime = .2f;
    public string attackAnimStateName;
    public ItemCategory attackHandType;
    public EnemyAttack nextComboAttack;
    public AnimatorOverrideController overrideController;


    public AnimationClip clipToOverride;
    public AnimationClip overrideClip;

    public virtual void Attack(EnemyAI enemy)
    {
        enemy.characterAnimator.IsInteracting = true;
        enemy.characterAnimator.OverrideDefaultAnimation(enemy.currentAttack.overrideController);
        enemy.characterAnimator.PlayAfterOneFrameCoroutine(enemy.currentAttack.attackAnimStateName, true, null,
            animTransitionTime);
        enemy.characterAnimator.SetCanDoComboBool(false);
    }

    public virtual void FinalizeAttack(EnemyAI enemy)
    {
    }
}

public class CreationSO<T> : ScriptableObject where T : new()
{
    public T Create()
    {
        return new T();
    }
}

public class TestCreationSO1 : CreationSO<TestA>
{
}