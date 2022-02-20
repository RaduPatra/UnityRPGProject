using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerManager playerManager;
    private EquipmentManager equipmentManager;
    private WeaponCollider weaponCollider;
    public EquipableItem CurrentWeapon { get; set; }

    [SerializeField] private InputManager inputManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        
        equipmentManager.OnEquipWeapon += LoadWeapon;
        inputManager.attackAction += AttackAction;
    }

    private void AttackAction()
    {
        if (playerManager.isInteracting) return;
        if (equipmentManager.equipmentInventory.equippedWeaponItems[ItemType.Sword] == null) return;
        /* attack and enable root motion.
         * Root motion gets disabled again on attack state exit.*/
        playerManager.PlayerAnimator.animator.SetBool(PlayerAnimator.RootMotionOn, true);
        playerManager.PlayerAnimator.PlayAnimation("Attack", true);
    }

    public void LoadWeapon(EquipableItem weapon, GameObject weaponGO)
    {
        if (weapon.itemType != ItemType.Sword && weapon.itemType != ItemType.Shield) return;
        weaponCollider = weaponGO.GetComponent<WeaponCollider>();
        CurrentWeapon = weapon;
        Debug.Log("load weapon " + weaponCollider);
    }

    //animation event
    public void OpenWeaponCollider()
    {
        if (weaponCollider == null) return;
        weaponCollider.EnableCollider();
    }

    //animation event
    public void CloseWeaponCollider()
    {
        if (weaponCollider == null) return;
        weaponCollider.DisableCollider();
    }
}