using UnityEngine;

/*[CreateAssetMenu(fileName = "New Quest Item Step", menuName = "QuestSteps/ItemStep", order = 6)]

public class GiveItemQuestStep : QuestStepSO//give item step
{
    public ItemWithAttributes requiredItem;
    public override bool CanComplete(GameObject go)
    {
        var inventory = go.GetComponent<InventoryHolder>();
        var hasRequiredItem = inventory.IsItemInInventory(requiredItem);
        return hasRequiredItem;
    }

    public override void CompleteAction(GameObject go)
    {
        var inventory = go.GetComponent<InventoryHolder>();
        inventory.RemoveItemFromInventory(requiredItem);
    }
}*/


public enum InteractionType
{
    StartInteraction,
    ContinueDialogue,
    TriggerInteraction,
    PickupItem
}