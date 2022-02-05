using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    public string isInteractingBool;
    public bool isInteractingStatus;
    
    public string isEmptyBool;
    public bool isEmptyStatus;
    
    public string rootMotionBool;
    public bool rootMotionBoolStatus;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isInteractingBool, isInteractingStatus);
        animator.SetBool(isEmptyBool, isEmptyStatus);
        animator.SetBool(rootMotionBool, rootMotionBoolStatus);
    }
}