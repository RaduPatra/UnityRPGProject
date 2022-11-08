using UnityEngine;

public class ControllerAnimatorMover : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponentInParent<CharacterController>();
    }

    private void OnAnimatorMove()
    {
        if (!animator.applyRootMotion) return;
        var velocity = animator.deltaPosition;
        controller.Move(velocity);
    }
}