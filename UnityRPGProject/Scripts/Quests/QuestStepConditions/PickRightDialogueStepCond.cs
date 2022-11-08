using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Condition", menuName = "QuestStepComponent/Conditions/PickRightDialogueStepCond", order = 6)]

public class PickRightDialogueStepCond : QuestStepCompletionCondition
{
    public DialogueNode rightDialogue;
    public override bool CanComplete(GameObject originGo, GameObject sourceGo)
    {
        var dialogueSource = sourceGo.GetComponent<DialogueSource>();
        var dialogueRunner = dialogueSource.dialogueRunner;
        return rightDialogue == dialogueRunner.CurrentNode;
    }
}