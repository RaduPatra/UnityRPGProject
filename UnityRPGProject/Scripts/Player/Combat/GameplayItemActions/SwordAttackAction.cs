using System.Runtime.Remoting.Messaging;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Actions/SwordAttackAction", order = 1)]
public class SwordAttackAction : ItemGameplayActions
{
    [SerializeField] private AttributeBaseSO staminaConsumptionAttribute;
    public override void StartAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<PlayerAnimator>();
        var playerAttacker = go.GetComponent<PlayerCombat>();
        var playerManager = go.GetComponent<PlayerManager>();
        var playerStamina = go.GetComponent<Stamina>();

        if (playerStamina.CurrentStamina <= 0)
        {
            return;
        }

        var canDoCombo = playerAnimator.GetCanDoComboBool();
        if (canDoCombo)
        {
            HandleCombo(playerAttacker, playerAnimator);
            playerStamina.DrainItemStamina(item, staminaConsumptionAttribute);
            // canDoCombo = false;
            return;
        }

        if (playerManager.IsInteracting) return;
        //play first attack
        playerAnimator.PlayAnimation(PlayerAnimator.lightAttack1, true);
        playerAttacker.LastAttack = PlayerAnimator.lightAttack1;
        
        playerStamina.DrainItemStamina(item, staminaConsumptionAttribute);
    }
    

    private static void HandleCombo(PlayerCombat playerAttacker, PlayerAnimator playerAnimator)
    {
        Debug.Log("Play Combo");
        switch (playerAttacker.LastAttack)
        {
            //todo - add support for custom combo chain list in the future
            case PlayerAnimator.lightAttack1:
                playerAnimator.PlayAnimation(PlayerAnimator.lightAttack2, true);
                playerAnimator.SetCanDoComboBool(false);
                playerAttacker.LastAttack = PlayerAnimator.lightAttack2;
                break;
            case PlayerAnimator.lightAttack2:
                playerAnimator.PlayAnimation(PlayerAnimator.lightAttack1, true);
                playerAnimator.SetCanDoComboBool(false);
                playerAttacker.LastAttack = PlayerAnimator.lightAttack1;
                break;
        }
    }
}