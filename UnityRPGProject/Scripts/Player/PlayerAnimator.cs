using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;
using UnityEngine.UI;


public class PlayerAnimator : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [SerializeField] private float dampTime;
    public AnimatorOverrideController overrideController;
    private string currentState;
    private PlayerManager playerManager;

    public static readonly int Horizontal = Animator.StringToHash("Horizontal");
    public static readonly int Vertical = Animator.StringToHash("Vertical");
    public static readonly int IsEmpty = Animator.StringToHash("isEmpty");
    public static readonly int IsInteracting = Animator.StringToHash("isInteracting");
    public static readonly int IsAiming = Animator.StringToHash("isAiming");
    public static readonly int Rolling = Animator.StringToHash("isRolling");

    private static readonly int CanDoCombo = Animator.StringToHash("canDoCombo");
    private static readonly int CanRotate = Animator.StringToHash("canRotate");

    public const string rightHandIdleDefault = "RightHandIdle2";
    public const string rightHandEmptyDefault = "RightHandEmpty";
    public const string leftHandIdleDefault = "LeftHandIdle2";
    public const string leftHandEmptyDefault = "LeftHandEmpty";

    public const string shieldStartBlock = "ShieldStartBlock";
    
    public const string lightAttack1 = "Attack";
    public const string lightAttack2 = "Attack2";
    public const string specialAttack = "SpecialAttack";
    public const string offHandShoot = "OffhandShoot";
    public const string leftHandCharge = "LeftHandCharge";
    public const string shieldBlockLoop = "ShieldBlockLoop";
    
    public const string chestOpenAnimation = "PlayerOpenChest";
    public const string takeItem = "TakeItem";
    
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerManager = GetComponent<PlayerManager>();
        animator.runtimeAnimatorController = overrideController;
        SetupDefaultLayerWeights();
    }

    public void UpdateLocomotionAnimation(float verticalMove, float horizontalMove)
    {
        //updates the locomotion animation blend tree
        animator.SetFloat(Vertical, verticalMove, dampTime, Time.deltaTime);
        animator.SetFloat(Horizontal, horizontalMove, dampTime, Time.deltaTime);
    }

    public float CurrentAnimationWeight { get; private set; }
    public void PlayAnimation(string animationState, bool isInteracting, bool canPlaySameAnimation = true,
        AnimationLayerData animationLayerData = null)
    {
        /*Reset current state if empty state was entered( - aka current animation is done playing),
         so we can play the same animation in a row.*/

        if (!canPlaySameAnimation)
        {
            if (animator.GetBool(IsEmpty))
                currentState = null;
            animator.SetBool(IsEmpty, false);

            //don't play the same animation if its already playing
            if (animationState == currentState) return;
        }

        currentState = animationState;
        animator.applyRootMotion = isInteracting;
        animator.CrossFade(animationState, dampTime);
        if (animationLayerData != null)
        {
            var layerName = animationLayerData.layerName;
            var layerWeight = animationLayerData.layerWeight;
            if (layerName != "" && layerWeight != -1f)
            {
                var layerIndex = animator.GetLayerIndex(layerName);
                animator.SetLayerWeight(layerIndex, layerWeight);
            }
        }
        
        /*isInteracting - used in code to prevent certain actions
         from happening while an animation with isInteracting set to true is playing
        (such as preventing moving while attacking with rootmotion)*/
        animator.SetBool(IsInteracting, isInteracting);
        Debug.Log("Played " + animationState);
    }

    public bool GetCanDoComboBool()
    {
        return animator.GetBool(CanDoCombo);
    }

    public void SetCanDoComboBool(bool status)
    {
        animator.SetBool(CanDoCombo, status);
    }
    
    public bool GetCanRotate()
    {
        return animator.GetBool(CanRotate);
    }

    public void SetCanRotate(bool status)
    {
        animator.SetBool(CanRotate, status);
    }

    private bool isRolling;
    public bool IsRolling
    {
        get => playerManager.PlayerAnimator.animator.GetBool(Rolling);
        set
        {
            isRolling = value;
            playerManager.PlayerAnimator.animator.SetBool(Rolling, value);
        }
    }

    /*public void OverrideDefaultAnimationTest(AnimatorOverrideController animationOverride, string animationName)
    {
        var animatorOverrideController = overrideController;
        animatorOverrideController[animationName] = animationOverride[animationName];
        var x = animatorOverrideController[animationName];
    }*/

    public void OverrideDefaultAnimation(AnimatorOverrideController animationOverride)
    {
        if (animationOverride.runtimeAnimatorController != overrideController.runtimeAnimatorController) return;
        
        var clipOverrides = new AnimationClipOverrides(animationOverride.overridesCount);
        var mainClipOverrides = new AnimationClipOverrides(overrideController.overridesCount);

        animationOverride.GetOverrides(clipOverrides);
        overrideController.GetOverrides(mainClipOverrides);

        foreach (var clipOverride in clipOverrides)
        {
            if (clipOverride.Value != null)
            {
                mainClipOverrides[clipOverride.Key.name] = clipOverride.Value;
            }
        }
        overrideController.ApplyOverrides(mainClipOverrides);
    }

    
    public List<float> DefaultLayerWeights { get; } = new List<float>();
    private void SetupDefaultLayerWeights()
    {
        for (var i = 0; i < animator.layerCount; i++)
        {
            var layerWeight = animator.GetLayerWeight(i);
            DefaultLayerWeights.Add(layerWeight);
        }
    }

    //we can only CrossFade one animation per frame
    private IEnumerator PlayAfterOneFrame(string animationName, AnimationLayerData animationLayerData = null)
    {
        yield return WaitFor.Frames(2);
        PlayAnimation(animationName, false, true, animationLayerData);
    }

    public void PlayAfterOneFrameCoroutine(string animationName, AnimationLayerData animationLayerData = null)
    {
        StartCoroutine(PlayAfterOneFrame(animationName, animationLayerData));
    }


    #region Testing
    [ContextMenu("test unequip (idle->empty)")]
    public void TestOverride()
    {
        animator.CrossFade("RightHandIdle", 0.2f);
        animator.CrossFade("RightHandEmpty", 0.2f);
    }

    #endregion
}


[Serializable]
public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity) {}

    public AnimationClip this[string name]
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            var index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
    
    public AnimationClip this[AnimationClip name]
    {
        get { return this.Find(x => x.Key == name).Value; }
        set
        {
            var index = this.FindIndex(x => x.Key == name);
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }

}

public static class WaitFor
{
    public static IEnumerator Frames(int frameCount)
    {
        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }
}