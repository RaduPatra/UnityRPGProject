using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueEndEvent", menuName = "Dialogue/DialogueEndEvent", order = 6)]

public class ExitDialogueEvent : DialogueEvent
{
    public override void Execute(DialogueSource dialogueSource, Interactor dialogueInteractor)
    {
        var dialogueCharacter = dialogueSource.GetComponent<DialogueSource>();
        dialogueCharacter.ExitConversation();
    }
}