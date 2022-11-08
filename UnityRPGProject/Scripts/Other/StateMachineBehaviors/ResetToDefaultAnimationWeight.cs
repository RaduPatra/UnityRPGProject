using UnityEngine;

public class ResetToDefaultAnimationWeight : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    public bool isFirstStateEnter = true;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isFirstStateEnter)
        {
            isFirstStateEnter = false;
            return;
        }
        var playerAnimator = animator.GetComponentInParent<CharacterAnimator>();
        ResetToDefaultWeight(playerAnimator, layerIndex);
    }
    private static void ResetToDefaultWeight(CharacterAnimator playerAnimator, int layerIndex)
    {
        var defaultWeighs = playerAnimator.DefaultLayerWeights;
        playerAnimator.animator.SetLayerWeight(layerIndex, defaultWeighs[layerIndex]);
    }
}