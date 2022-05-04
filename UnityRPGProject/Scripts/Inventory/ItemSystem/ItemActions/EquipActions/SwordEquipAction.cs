using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SwordEquip Action", menuName = "ItemActions/SwordEquipAction", order = 1)]
public class SwordEquipAction : EquipItemAction
{
    [SerializeField] private ItemCategory rightHandCategory;

    public override void ExecuteOnEquip(ItemWithAttributes item, GameObject parentGO, GameObject gameObjectToEquip = null)
    {
        var playerAnimator = parentGO.GetComponent<PlayerAnimator>();
        if (!gameObjectToEquip) return;
        var weaponCol = gameObjectToEquip.GetComponent<WeaponCollider>();
        /*var equipmentManager = gameObjectToEquip.GetComponentInParent<EquipmentManager>();
        var rightHandTransform = equipmentManager.equipmentLocations[rightHandCategory];*/

        if (weaponCol == null) return;
        weaponCol.Initialize(item);
        playerAnimator.OverrideDefaultAnimation(actionAnimationOverride);
        playerAnimator.PlayAfterOneFrameCoroutine(PlayerAnimator.rightHandIdleDefault);
    }

    public override void ExecuteOnUnequip(ItemWithAttributes item, GameObject parentGO, GameObject gameObjectToEquip = null)
    {
        var playerAnimator = parentGO.GetComponent<PlayerAnimator>();
        playerAnimator.PlayAnimation(PlayerAnimator.rightHandEmptyDefault, false);
    }
}