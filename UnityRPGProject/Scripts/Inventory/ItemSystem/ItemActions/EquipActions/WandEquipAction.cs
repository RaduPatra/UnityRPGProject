using UnityEngine;


[CreateAssetMenu(fileName = "New WandEquip Action", menuName = "ItemActions/WandEquipAction", order = 1)]
public class WandEquipAction : EquipItemAction
{
    public override void ExecuteOnEquip(ItemWithAttributes item, GameObject parentGO,
        GameObject gameObjectToEquip = null)
    {
        var playerAnimator = parentGO.GetComponent<CharacterAnimator>();
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

public class AnimationLayerData
{
    public string layerName;
    public float layerWeight;
}