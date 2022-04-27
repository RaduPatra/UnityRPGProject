using System;
using UnityEngine;

public class ItemColliderHolder : MonoBehaviour
{
    [SerializeField] private ItemCategory leftHandCategory;
    [SerializeField] private ItemCategory rightHandCategory;

    private Transform leftHandLocation;
    private Transform rightHandLocation;
    public Transform shieldCollider;

    private void Awake()
    {
        var equipmentManager = GetComponent<EquipmentManager>();
        leftHandLocation = equipmentManager.equipmentLocations[leftHandCategory];
        rightHandLocation = equipmentManager.equipmentLocations[rightHandCategory];
    }

    private void OpenCollider(Transform location)
    {
        var weaponCollider = location.GetComponentInChildren<IDamageCollider>();
        weaponCollider?.EnableCollider();
    }

    private void CloseCollider(Transform location)
    {
        var weaponCollider = location.GetComponentInChildren<IDamageCollider>();
        weaponCollider?.DisableCollider();
    }

    #region Animation Events

    public void OpenRightWeaponCollider()
    {
        OpenCollider(rightHandLocation);
    }

    public void CloseRightWeaponCollider()
    {
        CloseCollider(rightHandLocation);
    }

    public void OpenLeftWeaponCollider()
    {
        OpenCollider(leftHandLocation);
    }

    public void CloseLeftWeaponCollider()
    {
        CloseCollider(leftHandLocation);
    }

    public void OpenShieldCollider()
    {
        OpenCollider(shieldCollider);
    }

    public void CloseShieldCollider()
    {
        CloseCollider(shieldCollider);
    }

    #endregion
}