using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDamageCollider : StateMachineBehaviour
{
    public ItemColliderHolder colliderHolder;
    public bool isWeaponCollider;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (colliderHolder == null)
        {
            // Debug.Log("anim col set");
            colliderHolder = animator.gameObject.GetComponent<ItemColliderHolder>();
        }

        if (isWeaponCollider)
        {
            colliderHolder.CloseRightWeaponCollider();
        }
        else
        {
            colliderHolder.CloseShieldCollider();
        }
        // Debug.Log("test enter");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("test exit");
        // colliderHolder.CloseRightWeaponCollider();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}