using System;
using UnityEngine;

[RequireComponent(typeof(IEquipment))]
public class ItemColliderHolder : MonoBehaviour
{
    [SerializeField] private ItemCategory leftHandCategory;
    [SerializeField] private ItemCategory rightHandCategory;

    private Transform leftHandLocation;
    private Transform rightHandLocation;
    public Transform shieldCollider;
    private CharacterAnimator characterAnimator;
    private IEquipment equipmentManager;

    // public bool debug;
    private void Awake()
    {
        equipmentManager = GetComponent<IEquipment>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void Start()
    {
        characterAnimator.animationEvents.openRightWeaponColliderAnimEvent += OpenRightWeaponCollider;
        characterAnimator.animationEvents.closeRightWeaponColliderAnimEvent += CloseRightWeaponCollider;
        characterAnimator.animationEvents.openLeftWeaponColliderAnimEvent += OpenLeftWeaponCollider;
        characterAnimator.animationEvents.closeLeftWeaponColliderAnimEvent += CloseLeftWeaponCollider;

        leftHandLocation = equipmentManager.EquipmentLocations[leftHandCategory];
        rightHandLocation = equipmentManager.EquipmentLocations[rightHandCategory];
    }

    private void OpenCollider(Transform location)
    {
        // if (debug) return;
        var weaponCollider = location.GetComponentInChildren<IDamageCollider>();
        weaponCollider?.EnableCollider();
    }

    private void CloseCollider(Transform location)
    {
        // if (debug) return;
        var weaponCollider = location.GetComponentInChildren<IDamageCollider>();
        weaponCollider?.DisableCollider();
    }

    #region Animation Events

    private void OpenRightWeaponCollider()
    {
        OpenCollider(rightHandLocation);
    }

    public void CloseRightWeaponCollider()
    {
        CloseCollider(rightHandLocation);
    }
    


    private void OpenLeftWeaponCollider()
    {
        OpenCollider(leftHandLocation);
    }

    private void CloseLeftWeaponCollider()
    {
        CloseCollider(leftHandLocation);
    }

    #endregion

    public void OpenShieldCollider()
    {
        OpenCollider(shieldCollider);
    }

    public void CloseShieldCollider()
    {
        CloseCollider(shieldCollider);
    }
}

public class EnemyItemColliderHolder : MonoBehaviour
{
    private Transform leftHandLocation;
    private Transform rightHandLocation;

    private void Awake()
    {
    }
}