using UnityEngine;

[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Actions/BlockItemAction", order = 1)]

public class BlockItemAction : ItemGameplayActions
{
    public override void StartAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<PlayerAnimator>();
        var playerManager = go.GetComponent<PlayerManager>();
        var colliderHolder = go.GetComponent<ItemColliderHolder>();
        colliderHolder.OpenShieldCollider();

        playerAnimator.PlayAnimation(PlayerAnimator.shieldBlockLoop, false);
        playerManager.IsAiming = true;
        //enable collider
    }

    public override void CancelledAction(ItemWithAttributes item, GameObject go)
    {
        var playerManager = go.GetComponent<PlayerManager>();
        var colliderHolder = go.GetComponent<ItemColliderHolder>();
        playerManager.IsAiming = false;
        colliderHolder.CloseShieldCollider();
    }
}