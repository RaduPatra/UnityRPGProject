using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/*public abstract class QuestStepSO : ScriptableObject
{
    public string stepInstructions;
    public QuestEntitySO questEntityToInteractWith;
    public DialogueNode stepLoseDialogue;
    public DialogueNode stepWinDialogue;
    public DialogueNode rightDialogue;

    public abstract bool CanComplete(GameObject go);
    public abstract void CompleteAction(GameObject go);
}*/


[CreateAssetMenu(fileName = "New Quest Step", menuName = "QuestStep", order = 6)]

public class QuestStepSO : SerializedScriptableObject
{
    public string stepInstructions;

    public IDatabaseItem questEntityToInteractWith;

    public ActiveQuest QuestOrigin { get; set; }

    /*public DialogueNode stepLoseDialogue;
    public DialogueNode stepWinDialogue;
    public DialogueNode rightDialogue;*/

    /*public Dictionary<InteractionType, QuestStepCompletionCondition> stepInteractionCondition =
        new Dictionary<InteractionType, QuestStepCompletionCondition>();

    public Dictionary<InteractionType, QuestStepCompletionAction> stepInteractionAction =
        new Dictionary<InteractionType, QuestStepCompletionAction>();*/

    public Dictionary<InteractionType, QuestStepComponent> stepInteractions =
        new Dictionary<InteractionType, QuestStepComponent>();

    public Dictionary<InteractionType, QuestStepComponent> stepCompletionInteractions =
        new Dictionary<InteractionType, QuestStepComponent>();
    
    
    
    //need to add QuestStepCompletionAction/connd to pair under interaction type
    //so value = pair of action and cond
    
    /*public Dictionary<InteractionType, QuestStepCompletionCondition> completeStepCondition =
        new Dictionary<InteractionType, QuestStepCompletionCondition>();

    public Dictionary<InteractionType, QuestStepCompletionAction> completeStepActions =
        new Dictionary<InteractionType, QuestStepCompletionAction>();*/
}

public class QuestStepComponent
{
    public QuestStepCompletionCondition condition;
    public QuestStepCompletionAction action;
}

public abstract class QuestStepCompletionAction : ScriptableObject
{
    public abstract void CompleteAction(GameObject go, GameObject sourceGo, bool canCompleteResult);
}

public abstract class QuestStepCompletionCondition : ScriptableObject
{
    public abstract bool CanComplete(GameObject originGo, GameObject sourceGo);
}