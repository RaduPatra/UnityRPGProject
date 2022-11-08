using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Condition", menuName = "QuestStepComponent/Conditions/BoolStepCond", order = 6)]
public class BoolStepCond : QuestStepCompletionCondition
{
    public bool result;

    public override bool CanComplete(GameObject originGo, GameObject sourceGo)
    {
        return result;
    }
}