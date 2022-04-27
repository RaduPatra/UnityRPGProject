using UnityEngine;


[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Actions/SwordSpecialAttackAction", order = 1)]
public class SwordSpecialAttackAction : ItemGameplayActions
{
    public override void StartAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<PlayerAnimator>();
        var playerAttacker = go.GetComponent<PlayerAttack>();
        var playerManager = go.GetComponent<PlayerManager>();
//particles
        if (playerManager.isInteracting) return;
        playerAnimator.PlayAnimation(PlayerAnimator.specialAttack, true);
        playerAttacker.LastAttack = PlayerAnimator.specialAttack;

        //drain special attack stamina
    }
}