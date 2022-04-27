using System.Collections;
using UnityEngine;

public abstract class EquipItemAction : ScriptableObject
{
    public abstract void ExecuteOnEquip(ItemWithAttributes item, GameObject parentGO, GameObject gameObjectToEquip = null);
    public abstract void ExecuteOnUnequip(ItemWithAttributes item, GameObject parentGO, GameObject gameObjectToEquip = null);
    
    public AnimatorOverrideController actionAnimationOverride;

    public float animationWeightTest = -1;
    /*protected void PlayAnimation(PlayerAnimator playerAnimator, string animationName)
    {
        var animatorOverrideController = playerAnimator.overrideController;
        var animator = playerAnimator.animator;
        animatorOverrideController[animationName] = actionAnimationOverride[animationName];
        playerAnimator.StartCoroutine(PlayAfterOneFrame(animator, animationName));
    }

    protected static IEnumerator PlayAfterOneFrame(Animator animator, string animationName)
    {
        yield return null;
        animator.CrossFade(animationName, 0.2f);
    }*/
}