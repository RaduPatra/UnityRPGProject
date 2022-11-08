using UnityEngine;


[CreateAssetMenu(fileName = "New Gameplay Action", menuName = "Gameplay Actions/SwordSpecialAttackAction", order = 1)]
public class SwordSpecialAttackAction : ItemGameplayActions
{
    [SerializeField] private AttributeBaseSO specialAttackStaminaConsumptionAttribute;

    public override void StartAction(ItemWithAttributes item, GameObject go)
    {
        var playerAnimator = go.GetComponent<CharacterAnimator>();
        var playerAttacker = go.GetComponent<PlayerCombat>();
        
        var playerStamina = go.GetComponent<Stamina>();
        
        
        if (playerAnimator.IsInteracting) return;
        if (playerStamina.CurrentStamina <= 0) return;
        
        
        playerAnimator.PlayAnimation(CharacterAnimator.specialAttack, true);
        playerAttacker.LastAttack = CharacterAnimator.specialAttack;
        
        
        //todo - spawn particles
        
        playerStamina.DrainItemStamina(item, specialAttackStaminaConsumptionAttribute);

        
        
    }
}