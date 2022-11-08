using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Action", menuName = "QuestStepComponent/Actions/ModifyStartDialogueStepAction",
    order = 6)]
public class ModifyStartDialogueStepAction : QuestStepCompletionAction
{
    public DialogueNode stepLoseDialogue;
    public DialogueNode stepWinDialogue;
    public DialogueNode afterWinDefaultDialogue;

    public override void CompleteAction(GameObject originGo, GameObject sourceGo, bool canCompleteResult)
    {
        var dialogueSource = sourceGo.GetComponent<DialogueSource>();
        dialogueSource.firstDialogue = canCompleteResult ? stepWinDialogue : stepLoseDialogue;

        if (afterWinDefaultDialogue != null)
        {
            dialogueSource.SetDefaultDialogue(afterWinDefaultDialogue);
        }
    }
}