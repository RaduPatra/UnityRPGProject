using UnityEngine;

[CreateAssetMenu(fileName = "New ShieldEquip Action", menuName = "ItemActions/ShieldEquipAction", order = 1)]
public class ShieldEquipAction : EquipItemAction
{
    public override void ExecuteOnEquip(ItemWithAttributes item, GameObject parentGO,
        GameObject gameObjectToEquip = null)
    {
        var colliderHolder = parentGO.GetComponent<ItemColliderHolder>();
        var playerAnimator = parentGO.GetComponent<PlayerAnimator>();
        var shieldColl = colliderHolder.shieldCollider.GetComponent<ShieldCollider>();

        shieldColl.Initialize(item);
        playerAnimator.OverrideDefaultAnimationTest(actionAnimationOverride);
        var layerData = new AnimationLayerData
        {
            layerName = "LeftHand",
            layerWeight = animationWeightTest
        };
        playerAnimator.PlayAfterOneFrameCoroutine(PlayerAnimator.leftHandIdleDefault, layerData);
    }

    public override void ExecuteOnUnequip(ItemWithAttributes item, GameObject parentGO,
        GameObject gameObjectToEquip = null)
    {
        var playerAnimator = parentGO.GetComponent<PlayerAnimator>();
        var playerManager = parentGO.GetComponent<PlayerManager>();
        playerAnimator.PlayAnimation(PlayerAnimator.leftHandEmptyDefault, false);
        playerManager.IsAiming = false;
    }
}