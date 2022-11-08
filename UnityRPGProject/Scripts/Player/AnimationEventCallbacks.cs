using System;
using UnityEngine;

public class AnimationEventCallbacks : MonoBehaviour
{
    public Action enableComboAnimEvent = delegate { };
    public Action disableComboAnimEvent = delegate { };
    public Action shootProjectileAnimEvent = delegate { };
    public Action openRightWeaponColliderAnimEvent = delegate { };
    public Action closeRightWeaponColliderAnimEvent = delegate { };
    public Action openLeftWeaponColliderAnimEvent = delegate { };
    public Action closeLeftWeaponColliderAnimEvent = delegate { };
    public Action enableCanRotateAnimEvent = delegate { };
    public Action disableCanRotateAnimEvent = delegate { };


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void EnableCombo()
    {
        enableComboAnimEvent?.Invoke();
    }

    public void DisableCombo()
    {
        disableComboAnimEvent?.Invoke();
    }

    public void ShootProjectile()
    {
        shootProjectileAnimEvent?.Invoke();
    }

    public void OpenRightWeaponCollider()
    {
        openRightWeaponColliderAnimEvent?.Invoke();
    }

    public void CloseRightWeaponCollider()
    {
        closeRightWeaponColliderAnimEvent?.Invoke();
    }


    public void OpenLeftWeaponCollider()
    {
        openLeftWeaponColliderAnimEvent?.Invoke();
    }

    public void CloseLeftWeaponCollider()
    {
        closeLeftWeaponColliderAnimEvent?.Invoke();
    }

    public void EnableCanRotate()
    {
        enableCanRotateAnimEvent?.Invoke();
    }

    public void DisableCanRotate()
    {
        disableCanRotateAnimEvent?.Invoke();
    }

    public void TestEvent(AnimationEvent evt)
    {
        /*Debug.Log("---------------------------");

        var currClipInfo = animator.GetCurrentAnimatorClipInfo(4)[0];
        Debug.Log("Current-------------");
        Debug.Log(currClipInfo.clip.name);
        Debug.Log(currClipInfo.weight);

        
        
        Debug.Log("Event-------------");
        var evtClipInfo = evt.animatorClipInfo;
        Debug.Log(evtClipInfo.clip.name);
        Debug.Log(evtClipInfo.weight);
        Debug.Log("disable collider");

        Debug.Log("---------------------------");*/
    }
}