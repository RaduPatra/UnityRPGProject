using UnityEngine;

[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Actions/BlockItemAction", order = 1)]
public class BlockItemAction : ItemGameplayActions
{
    public override void StartAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<CharacterAnimator>();
        if (playerAnimator.IsInteracting) return;
        
        var colliderHolder = go.GetComponent<ItemColliderHolder>();
        colliderHolder.OpenShieldCollider();

        playerAnimator.PlayAnimation(CharacterAnimator.shieldBlockLoop, false);
        playerAnimator.IsAiming = true;
        //enable collider
    }

    public override void CancelledAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<CharacterAnimator>();
        var colliderHolder = go.GetComponent<ItemColliderHolder>();
        playerAnimator.IsAiming = false;
        colliderHolder.CloseShieldCollider();
    }
}