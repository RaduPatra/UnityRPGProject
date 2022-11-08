using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Action", menuName = "QuestStepComponent/Actions/DoNothingStepAction",
    order = 6)]
public class DoNothingStepAction : QuestStepCompletionAction
{
    public override void CompleteAction(GameObject originGo, GameObject sourceGo, bool canCompleteResult)
    {
    }
}