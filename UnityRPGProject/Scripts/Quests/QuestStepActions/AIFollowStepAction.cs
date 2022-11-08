using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Action", menuName = "QuestStepComponent/Actions/AIFollowStepAction", order = 6)]

public class AIFollowStepAction : QuestStepCompletionAction
{
    public override void CompleteAction(GameObject originGo, GameObject sourceGo, bool canCompleteResult)
    {
        if (canCompleteResult)
        {
            var follower = sourceGo.GetComponent<AIFollower>();
            if (!follower) return;
            originGo.GetComponent<PlayerFollower>().SetFollower(follower);
        }
    }
}