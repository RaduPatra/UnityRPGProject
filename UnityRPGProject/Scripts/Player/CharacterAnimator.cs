using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;
using UnityEngine.UI;


public class CharacterAnimator : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public AnimationEventCallbacks animationEvents;
    [SerializeField] private float dampTime;
    public AnimatorOverrideController overrideController;

    #region Animation Names

    public static readonly int Horizontal = Animator.StringToHash("Horizontal");
    public static readonly int Vertical = Animator.StringToHash("Vertical");
    public static readonly int IsEmpty = Animator.StringToHash("isEmpty");
    public static readonly int IsInteractingAnim = Animator.StringToHash("isInteracting");
    public static readonly int IsAimingAnim = Animator.StringToHash("isAiming");
    public static readonly int Rolling = Animator.StringToHash("isRolling");

    private static readonly int CanDoCombo = Animator.StringToHash("canDoCombo");
    private static readonly int CanRotate = Animator.StringToHash("canRotate");

    public const string rightHandIdleDefault = "RightHandIdle2";
    public const string rightHandEmptyDefault = "RightHandEmpty";
    public const string leftHandIdleDefault = "LeftHandIdle2";
    public const string leftHandEmptyDefault = "LeftHandEmpty";
    public const string Falling = "Falling";
    public const string Landing = "Landing";
    public const string Roll = "Roll";
    public const string Jump = "Jump";

    public const string shieldStartBlock = "ShieldStartBlock";

    public const string lightAttack1 = "Attack";
    public const string lightAttack2 = "Attack2";
    public const string specialAttack = "SpecialAttack";
    public const string offHandShoot = "OffhandShoot";
    public const string leftHandCharge = "LeftHandCharge";
    public const string shieldBlockLoop = "ShieldBlockLoop";

    public const string chestOpenAnimation = "PlayerOpenChest";
    public const string takeItem = "TakeItem";
    public const string sitGround = "SitGround";
    public const string getUpGround = "GetUpGround";
    public const string damage = "TakeDamage";
    public const string damageStun = "TakeDamageStun";
    public const string death = "Death";
    public const string respawn = "Respawn";

    #endregion

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animationEvents = GetComponentInChildren<AnimationEventCallbacks>();

        if (animationEvents != null)
        {
            animationEvents.enableComboAnimEvent += EnableCombo;
            animationEvents.disableComboAnimEvent += DisableCombo;
            animationEvents.enableCanRotateAnimEvent += EnableCanRotate;
            animationEvents.disableCanRotateAnimEvent += DisableCanRotate;
        }
        
        if (overrideController != null)
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

    // public bool IsInteracting => animator.GetBool(IsInteracting);

    public void PlayAnimation(string animationState, bool isInteracting, /*, bool canPlaySameAnimation = true*/
        float transitionTime = -1,
        AnimationLayerData animationLayerData = null)
    {
        if (transitionTime == -1) transitionTime = dampTime;

        animator.applyRootMotion = isInteracting;

        animator.CrossFade(animationState, transitionTime);
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

        IsInteracting = isInteracting;
        Debug.Log("Played " + animationState + " Isinteracting: " + isInteracting);
    }

    public void PlayAnyStateTrigger(string triggerName, bool isInteracting = true)
    {
        animator.SetTrigger(triggerName);
        IsInteracting = isInteracting;
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
        get => animator.GetBool(Rolling);
        set
        {
            isRolling = value;
            animator.SetBool(Rolling, value);
        }
    }

    [SerializeField] private bool isAiming;
    public bool IsAiming
    {
        get => isAiming;
        set
        {
            isAiming = value;
            animator.SetBool(IsAimingAnim, value);
        }
    }

    public bool IsInteracting
    {
        get => animator.GetBool(IsInteractingAnim);
        set => animator.SetBool(IsInteractingAnim, value);
    }

    public void OverrideDefaultAnimation(AnimatorOverrideController animationOverride)
    {
        //overrideController = runtimeController
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

    //we cant CrossFade in the same frame when animation gets overriden
    public void PlayAfterOneFrameCoroutine(string animationState, bool isInteracting,
        AnimationLayerData animationLayerData = null, float transitionTime = .2f)
    {
        StartCoroutine(PlayAfterOneFrame(animationState, isInteracting,
            animationLayerData, transitionTime));
    }

    private IEnumerator PlayAfterOneFrame(string animationState, bool isInteracting,
        AnimationLayerData animationLayerData = null, float transitionTime = .2f)
    {
        yield return WaitFor.Frames(2);
        PlayAnimation(animationState, isInteracting, transitionTime, animationLayerData);
    }

    #region Animation events

    private void EnableCombo()
    {
        SetCanDoComboBool(true);
    }

    private void DisableCombo()
    {
        SetCanDoComboBool(false);
    }

    private void EnableCanRotate()
    {
        SetCanRotate(true);
    }

    private void DisableCanRotate()
    {
        SetCanRotate(false);
    }

    #endregion

    #region Testing

    [ContextMenu("test unequip (idle->empty)")]
    public void TestOverride()
    {
        animator.CrossFade("RightHandIdle", 0.2f);
        animator.CrossFade("RightHandEmpty", 0.2f);
    } 
    
    /*public void OverrideDefaultAnimationTest(AnimatorOverrideController animationOverride, string animationName)
    {
        var animatorOverrideController = overrideController;
        animatorOverrideController[animationName] = animationOverride[animationName];
        var x = animatorOverrideController[animationName];
    }*/

    #endregion
}


[Serializable]
public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity)
    {
    }

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