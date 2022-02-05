using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;
using UnityEngine.UI;


public class PlayerAnimator : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [SerializeField] private float dampTime;
    private string currentState;
    private PlayerManager playerManager;

    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int IsEmpty = Animator.StringToHash("isEmpty");
    private static readonly int IsInteracting = Animator.StringToHash("isInteracting");
    public static readonly int RootMotionOn = Animator.StringToHash("rootMotion");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
    }

    public void UpdateLocomotionAnimation(float verticalMove)
    {
        //updates the locomotion animation blend tree
        //animator.SetFloat(Horizontal, horizontalMove, dampTime, Time.deltaTime);
        animator.SetFloat(Vertical, verticalMove, dampTime, Time.deltaTime);
    }

    public void PlayAnimation(string animationState, bool isInteracting)
    {
        /*Reset current state if empty state was entered( - aka current animation is done playing),
         so we can play the same animation in a row.*/
        if (animator.GetBool(IsEmpty))
            currentState = null;

        animator.SetBool(IsEmpty, false);
        //don't play the same animation if its already playing
        if (animationState == currentState) return;

        currentState = animationState;
        animator.CrossFade(animationState, dampTime);

        /*isInteracting - used in code to prevent certain actions when an animation with isInteracting set to true is playing
        (such as preventing jumping while attacking, etc)*/
        animator.SetBool(IsInteracting, isInteracting);
        //Debug.Log("play anim: " + animationState);
    }

    private void Update()
    {
        playerManager.isInteracting = animator.GetBool(IsInteracting);
        animator.applyRootMotion = animator.GetBool(RootMotionOn);
    }
}