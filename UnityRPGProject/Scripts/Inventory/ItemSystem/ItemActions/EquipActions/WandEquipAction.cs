using UnityEngine;


[CreateAssetMenu(fileName = "New WandEquip Action", menuName = "ItemActions/WandEquipAction", order = 1)]
public class WandEquipAction : EquipItemAction
{
    public override void ExecuteOnEquip(ItemWithAttributes item, GameObject parentGO,
        GameObject gameObjectToEquip = null)
    {
        var playerAnimator = parentGO.GetComponent<PlayerAnimator>();
        playerAnimator.OverrideDefaultAnimation(actionAnimationOverride);
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

public class AnimationLayerData
{
    public string layerName;
    public float layerWeight;
}