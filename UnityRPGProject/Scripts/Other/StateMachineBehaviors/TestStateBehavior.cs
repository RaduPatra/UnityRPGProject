using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class TestStateBehavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    public string testStateName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(testStateName + " entered");
        // var x = animator.GetCurrentAnimatorStateInfo(4).
        // var y = animator.GetCurrentAnimatorClipInfo(4)[0].
        
        var isName = stateInfo.shortNameHash == animator.GetCurrentAnimatorStateInfo(4).shortNameHash;
        Debug.Log("Is Name : " + isName);
        found = false;
    }

    private bool found;
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if (found) return;
        var isName = stateInfo.shortNameHash == animator.GetCurrentAnimatorStateInfo(4).shortNameHash;
        if (isName)
        {
            found = true;
        }
        Debug.Log("Is Name : " + isName);


    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(testStateName + " exit");
    }
}