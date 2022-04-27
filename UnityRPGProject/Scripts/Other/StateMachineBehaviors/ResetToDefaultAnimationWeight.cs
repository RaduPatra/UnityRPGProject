using UnityEngine;

public class ResetToDefaultAnimationWeight : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    public bool isFirstStateEnter = true;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("SetAnimationWeight");
        if (isFirstStateEnter)
        {
            Debug.Log("SetAnimationWeight First");

            isFirstStateEnter = false;
            return;
        }
        var playerAnimator = animator.GetComponent<PlayerAnimator>();
        ResetToDefaultWeight(playerAnimator, layerIndex);
        
        
    }
    private static void ResetToDefaultWeight(PlayerAnimator playerAnimator, int layerIndex)
    {
        var defaultWeighs = playerAnimator.DefaultLayerWeights;
        playerAnimator.animator.SetLayerWeight(layerIndex, defaultWeighs[layerIndex]);
    }
}