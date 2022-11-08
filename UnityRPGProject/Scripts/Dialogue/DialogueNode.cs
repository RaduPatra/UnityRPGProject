using System;
using System.Collections.Generic;
using Player;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "New DialogueNode", menuName = "Dialogue/DialogueNode", order = 6)]
public class DialogueNode : ScriptableObject, IDatabaseItem
{
    public string dialogueText;
    public string characterName;
    public List<DialogueOption> dialogueOptions;
    public DialogueNode nextDialogue;
    public DialogueEvent dialogueNodeStartEvent;

    public DialogueEvent dialogueNodeEndEvent;
    [field: SerializeField, ReadOnly] public string Id { get; private set; }

    private void Awake()
    {
#if UNITY_EDITOR
        if (Id.IsNullOrWhitespace())
        {
            Id = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }

    [ContextMenu("getnew id")]
    public void GenerateNewId()
    {
#if UNITY_EDITOR
        Id = Guid.NewGuid().ToString();
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
#endif
        
    }
}

public class DialogueNodeRunner
{
    public DialogueNode CurrentNode { get; set; }

    private DialogueSource dialogueSource;
    private Interactor dialogueStarter;

    public DialogueNodeRunner(DialogueSource dialogueSource, Interactor dialogueStarter)
    {
        this.dialogueSource = dialogueSource;
        this.dialogueStarter = dialogueStarter;
    }

    public DialogueNode RunNextNode()
    {
        return RunNode(CurrentNode.nextDialogue);
    }

    public DialogueNode RunNode(DialogueNode node)
    {
        if (CurrentNode != null && CurrentNode.dialogueNodeEndEvent != null)
        {
            CurrentNode.dialogueNodeEndEvent.Execute(dialogueSource, dialogueStarter);
        }

        CurrentNode = node;

        if (CurrentNode != null && CurrentNode.dialogueNodeStartEvent != null)
        {
            CurrentNode.dialogueNodeStartEvent.Execute(dialogueSource, dialogueStarter);
        }

        return CurrentNode;
    }
}