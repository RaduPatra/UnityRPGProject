using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Action", menuName = "QuestStepComponent/Actions/ExchangeItemStepAction", order = 6)]

public class  ExchangeItemStepAction : QuestStepCompletionAction
{
    public ItemWithAttributes itemToRemove;
    public ItemWithAttributes itemToGive;
    //add show ui event channel

    public override void CompleteAction(GameObject originGo, GameObject sourceGo, bool canCompleteResult)
    {
        if (canCompleteResult)
        {
            var inventory = originGo.GetComponent<InventoryHolder>();
            if (itemToRemove != null)
                inventory.RemoveItemFromInventory(itemToRemove);//show remove message ui
            if (itemToGive != null)//need to check if inv full
                inventory.PickUp(itemToGive);//show received message ui
        }
    }
}