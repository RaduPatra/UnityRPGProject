using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New StartQuestDialogueEvent", menuName = "Dialogue/StartQuestDialogueEvent", order = 6)]

public class StartQuestDialogueEvent : DialogueEvent
{
    public QuestSO questToStart;
    public DialogueNode defaultDialogueAfterStart;
    public override void Execute(DialogueSource dialogueGiver, Interactor interactor)
    {
        var questManager = interactor.GetComponent<QuestManager>();
        if (questManager.ContainsQuest(questToStart)) return;
        questManager.AddQuest(new ActiveQuest(questToStart));
        if (defaultDialogueAfterStart != null)
            dialogueGiver.SetDefaultDialogue(defaultDialogueAfterStart);
    }
}