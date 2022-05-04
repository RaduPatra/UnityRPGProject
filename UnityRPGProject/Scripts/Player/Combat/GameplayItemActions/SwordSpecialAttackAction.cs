using UnityEngine;


[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Actions/SwordSpecialAttackAction", order = 1)]
public class SwordSpecialAttackAction : ItemGameplayActions
{
    [SerializeField] private AttributeBaseSO specialAttackStaminaConsumptionAttribute;

    public override void StartAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<PlayerAnimator>();
        var playerAttacker = go.GetComponent<PlayerCombat>();
        var playerManager = go.GetComponent<PlayerManager>();
        
        var playerStamina = go.GetComponent<Stamina>();
        
        
        if (playerManager.IsInteracting) return;
        if (playerStamina.CurrentStamina <= 0) return;
        
        
        playerAnimator.PlayAnimation(PlayerAnimator.specialAttack, true);
        playerAttacker.LastAttack = PlayerAnimator.specialAttack;
        
        
        //todo - spawn particles
        
        playerStamina.DrainItemStamina(item, specialAttackStaminaConsumptionAttribute);

        
        
    }
}