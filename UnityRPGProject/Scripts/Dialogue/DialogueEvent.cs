using Player;
using UnityEngine;

public abstract class DialogueEvent : ScriptableObject
{
    public abstract void Execute(DialogueSource dialogueGiver, Interactor dialogueInteractor);
}