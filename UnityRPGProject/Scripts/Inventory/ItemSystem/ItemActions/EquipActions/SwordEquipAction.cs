using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SwordEquip Action", menuName = "ItemActions/SwordEquipAction", order = 1)]
public class SwordEquipAction : EquipItemAction
{
    [SerializeField] private ItemCategory rightHandCategory;

    public override void ExecuteOnEquip(ItemWithAttributes item, GameObject parentGO, GameObject gameObjectToEquip = null)
    {
        var playerAnimator = parentGO.GetComponent<CharacterAnimator>();
        if (!gameObjectToEquip) return;
        var weaponCol = gameObjectToEquip.GetComponent<WeaponCollider>();
        /*var equipmentManager = gameObjectToEquip.GetComponentInParent<EquipmentManager>();
        var rightHandTransform = equipmentManager.equipmentLocations[rightHandCategory];*/

        if (weaponCol == null) return;
        weaponCol.Initialize(item);
        playerAnimator.OverrideDefaultAnimation(actionAnimationOverride);
        // playerAnimator.PlayAfterOneFrameCoroutine(CharacterAnimator.rightHandIdleDefault);
        playerAnimator.PlayAfterOneFrameCoroutine(CharacterAnimator.rightHandIdleDefault, playerAnimator.IsInteracting);

    }

    public override void ExecuteOnUnequip(ItemWithAttributes item, GameObject parentGO, GameObject gameObjectToEquip = null)
    {
        var playerAnimator = parentGO.GetComponent<CharacterAnimator>();
        playerAnimator.PlayAnimation(CharacterAnimator.rightHandEmptyDefault, playerAnimator.IsInteracting);
    }
}