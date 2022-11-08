using UnityEngine;

[CreateAssetMenu(fileName = "New ShieldEquip Action", menuName = "ItemActions/ShieldEquipAction", order = 1)]
public class ShieldEquipAction : EquipItemAction
{
    public override void ExecuteOnEquip(ItemWithAttributes item, GameObject parentGO,
        GameObject gameObjectToEquip = null)
    {
        var colliderHolder = parentGO.GetComponent<ItemColliderHolder>();
        var playerAnimator = parentGO.GetComponent<CharacterAnimator>();
        var shieldColl = colliderHolder.shieldCollider.GetComponent<ShieldCollider>();

        shieldColl.Initialize(item);
        playerAnimator.OverrideDefaultAnimation(actionAnimationOverride);
        var layerData = new AnimationLayerData
        {
            layerName = "LeftHand",
            layerWeight = animationWeightTest
        };
        // playerAnimator.PlayAfterOneFrameCoroutine(CharacterAnimator.leftHandIdleDefault, layerData);
        playerAnimator.PlayAfterOneFrameCoroutine(CharacterAnimator.leftHandIdleDefault, playerAnimator.IsInteracting, layerData);
    }

    public override void ExecuteOnUnequip(ItemWithAttributes item, GameObject parentGO,
        GameObject gameObjectToEquip = null)
    {
        var playerAnimator = parentGO.GetComponent<CharacterAnimator>();
        playerAnimator.PlayAnimation(CharacterAnimator.leftHandEmptyDefault, playerAnimator.IsInteracting);
        playerAnimator.IsAiming = false;
    }
}