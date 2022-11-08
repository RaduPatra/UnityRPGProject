using System.Runtime.Remoting.Messaging;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Actions/SwordAttackAction", order = 1)]
public class SwordAttackAction : ItemGameplayActions
{
    [SerializeField] private AttributeBaseSO staminaConsumptionAttribute;
    public override void StartAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<CharacterAnimator>();
        var playerAttacker = go.GetComponent<PlayerCombat>();
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

        if (playerAnimator.IsInteracting) return;
        //play first attack
        playerAnimator.PlayAnimation(CharacterAnimator.lightAttack1, true);
        playerAttacker.LastAttack = CharacterAnimator.lightAttack1;
        
        playerStamina.DrainItemStamina(item, staminaConsumptionAttribute);
    }
    

    private static void HandleCombo(PlayerCombat playerAttacker, CharacterAnimator playerAnimator)
    {
        Debug.Log("Play Combo");
        switch (playerAttacker.LastAttack)
        {
            //todo - add support for custom combo chain list in the future
            case CharacterAnimator.lightAttack1:
                playerAnimator.PlayAnimation(CharacterAnimator.lightAttack2, true);
                playerAnimator.SetCanDoComboBool(false);
                playerAttacker.LastAttack = CharacterAnimator.lightAttack2;
                break;
            case CharacterAnimator.lightAttack2:
                playerAnimator.PlayAnimation(CharacterAnimator.lightAttack1, true);
                playerAnimator.SetCanDoComboBool(false);
                playerAttacker.LastAttack = CharacterAnimator.lightAttack1;
                break;
        }
    }
}