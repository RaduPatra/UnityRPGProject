using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueAnimationEvent", menuName = "Dialogue/DialogueAnimationEvent", order = 6)]

public class PlayAnimationDialogueEvent : DialogueEvent
{
    public string animationToPlay;
    public override void Execute(DialogueSource dialogueGiver, Interactor interactor)
    {
        var characterAnimator = dialogueGiver.GetComponent<CharacterAnimator>();
        characterAnimator.PlayAnimation(animationToPlay, true);
    }
}

public class StepDialogueDialogueEvent : DialogueEvent
{
    public QuestStepSO stepToComplete;
    public override void Execute(DialogueSource dialogueGiver, Interactor interactor)
    {
        var questManager = dialogueGiver.GetComponent<QuestManager>();
    }
}
