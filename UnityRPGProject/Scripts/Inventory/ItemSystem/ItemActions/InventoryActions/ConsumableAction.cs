using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Consumable Action", menuName = "ItemActions/ConsumableAction", order = 1)]
public class ConsumableAction : InventoryItemAction
{
    [SerializeField] private AttributeBaseSO effectsAttribute;

    public override void Execute(GameObject user, InventorySlot slot)
    {
        var effectManager = user.GetComponent<EffectManager>();

        var item = slot.GetItem();
        var attr = item.GetAttribute<EffectListAttributeData>(effectsAttribute);

        if (attr == null) return;
        var effects = attr.value;
        foreach (var effect in effects)
        {
            effect.UseEffect(user);
        }

        slot.DecreaseQuantity();
        effectManager.AddEffects(effects);
    }
}