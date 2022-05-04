using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private InputManager inputManager;

    [SerializeField] private ItemCategory mainHandItemCategory;
    [SerializeField] private ItemCategory offhandItemCategory;

    [SerializeField] private AttributeBaseSO rightClickActionAttribute;
    [SerializeField] private AttributeBaseSO leftClickActionAttribute;
    [SerializeField] private AttributeBaseSO middleClickActionAttribute;
    public string LastAttack { get; set; }
    public bool ChargePerformed { get; set; }
    public bool CanCastSpell { get; set; }

    private PlayerManager playerManager;
    private PlayerAnimator playerAnimator;
    private EquipmentManager equipmentManager;
    private WeaponCollider weaponCollider;
    private Dictionary<ItemCategory, WeaponCollider> slotColliders = new Dictionary<ItemCategory, WeaponCollider>();

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerAnimator = playerManager.PlayerAnimator;
        equipmentManager = GetComponent<EquipmentManager>();

        inputManager.attackAction += MainHandLeftClickAction;
        inputManager.specialAttackAction += MainHandMiddleClickAction;
        inputManager.blockActionStart += OffHandRightClickActionStart;
        inputManager.blockActionPerformed += OffHandRightClickActionPerformed;
        inputManager.blockCancelledAction += OffHandRightClickActionCancelled;
    }

    private void MainHandLeftClickAction()
    {
        var action = GetHandAction(mainHandItemCategory, leftClickActionAttribute);
        if (!action) return;
        var item = GetHandItem(mainHandItemCategory);
        action.StartAction(item, gameObject);
    }

    private void MainHandMiddleClickAction()
    {
        var action = GetHandAction(mainHandItemCategory, middleClickActionAttribute);
        if (!action) return;
        var item = GetHandItem(mainHandItemCategory);
        action.StartAction(item, gameObject);
    }

    private void OffHandRightClickActionStart()
    {
        var action = GetHandAction(offhandItemCategory, rightClickActionAttribute);
        if (!action) return;
        var item = GetHandItem(offhandItemCategory);
        action.StartAction(item, gameObject);
    }

    private void OffHandRightClickActionPerformed()
    {
        var action = GetHandAction(offhandItemCategory, rightClickActionAttribute);
        if (!action) return;
        var item = GetHandItem(offhandItemCategory);
        action.PerformedAction(item, gameObject);
    }

    private void OffHandRightClickActionCancelled()
    {
        var action = GetHandAction(offhandItemCategory, rightClickActionAttribute);
        if (!action) return;
        var item = GetHandItem(offhandItemCategory);
        action.CancelledAction(item, gameObject);
    }

    private ItemGameplayActions GetHandAction(ItemCategory itemCategory, AttributeBaseSO handActionAttribute)
    {
        var item = GetHandItem(itemCategory);
        if (item == null) return null;
        var attribute = item.GetAttribute<GameplayItemActionsData>(handActionAttribute)?.value;
        return attribute;
    }

    private ItemWithAttributes GetHandItem(ItemCategory itemCategory)
    {
        return equipmentManager.equipmentInventory.equippedWeaponItems[itemCategory].item;
    }

    #region Animation Events

    public void EnableCombo()
    {
        playerAnimator.SetCanDoComboBool(true);
    }

    public void DisableCombo()
    {
        playerAnimator.SetCanDoComboBool(false);
    }
    
    public void ShootProjectile()
    {
        var action = GetHandAction(offhandItemCategory, rightClickActionAttribute);
        if (!action) return;
        var item = GetHandItem(offhandItemCategory);
        action.FinalizeAction(item, gameObject);
    }

    #endregion
    
    public ParticleSystem impactParticles;

    [ContextMenu("play particle")]
    public void TestPlay()
    {
        impactParticles.Play();
    }

}