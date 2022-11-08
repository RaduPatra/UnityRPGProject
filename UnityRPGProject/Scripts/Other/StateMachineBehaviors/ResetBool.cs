using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    public string boolName;
    public bool boolStatus;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("start enter reset bool" + boolName + " " + boolStatus + " " + layerIndex);
        animator.SetBool(boolName, boolStatus);
        // Debug.Log("reset bool enter");
    }
}