using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Condition", menuName = "QuestStepComponent/Conditions/HasRequiredItemStepConditon", order = 6)]

public class HasRequiredItemStepConditon : QuestStepCompletionCondition
{
    public ItemWithAttributes requiredItem;
    public override bool CanComplete(GameObject originGo, GameObject sourceGo)
    {
        var dialogueSource = sourceGo.GetComponent<DialogueSource>();
        var inventory = originGo.GetComponent<InventoryHolder>();

        var hasRequiredItem = inventory.IsItemInInventory(requiredItem);
        return hasRequiredItem;
    }
}