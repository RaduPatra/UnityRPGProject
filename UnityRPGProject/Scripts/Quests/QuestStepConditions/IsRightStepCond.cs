using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Condition", menuName = "QuestStepComponent/Conditions/IsRightStepCond",
    order = 6)]
public class IsRightStepCond : QuestStepCompletionCondition
{
    public QuestStepSO questStepSo;
    public override bool CanComplete(GameObject originGo, GameObject sourceGo)
    {
        return questStepSo.QuestOrigin.GetCurrentStep() == questStepSo;
    }
}