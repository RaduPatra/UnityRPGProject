using System;
using Player;
using UnityEngine;


public interface IWorldEntity
{
    [field: SerializeField] public WorldEntitySO WorldEntity { get; }
}

public class DialogueSource : MonoBehaviour, IInteractable, IWorldEntity
{
    public DialogueNode defaultDialogue; //first one
    public DialogueNode firstDialogue; //first one
    public DialogueEventChannel onDialogueChangeEventChannel;
    public InputManager inputManager;

    public DialogueEventChannel onDialogueSelectEventChannel;
    public VoidEventChannel onDialogueExitEventChannel;
    public GameObjectEventChannel onDialogueGoInteractEventChannel;
    public string characterName = "Character";


    public DialogueNodeRunner dialogueRunner;

    private QuestManager interactorQuestMan;

    [field: SerializeField] public WorldEntitySO WorldEntity { get; set; }

    [Header("Debug")] public bool isInteracting;

    private void Awake()
    {
        // onDialogueSelectEventChannel.Listeners += SelectOption;
        SaveSystem.OnLoad += LoadDefaultDialogue;
        SaveSystem.OnInitSaveData += InitSaveData;
    }

    private void InitSaveData()
    {
        SaveData.Current.sceneData.defaultDialogueNodesData[GetComponent<GameObjectId>().Id] = defaultDialogue.Id;
    }

    private void OnDestroy()
    {
        onDialogueSelectEventChannel.Listeners -= SelectOption;
        inputManager.interactAction -= ContinueDialogue;
        SaveSystem.OnLoad -= LoadDefaultDialogue;
        SaveSystem.OnInitSaveData -= InitSaveData;
    }

    public void LoadDefaultDialogue(SaveData saveData)
    {
        var db = SaveSystemManager.Instance.dialogueDb;
        if (!saveData.sceneData.defaultDialogueNodesData.TryGetValue(GetComponent<GameObjectId>().Id,
            out var defaultNodeId)) return;
        var defaultNodeSave = db.GetById(defaultNodeId);
        if (defaultNodeSave == null) return;
        defaultDialogue = defaultNodeSave;
    }

    public void SetDefaultDialogue(DialogueNode newDefault)
    {
        SaveData.Current.sceneData.defaultDialogueNodesData[GetComponent<GameObjectId>().Id] = newDefault.Id;
        defaultDialogue = newDefault;
    }

    public void Interact(Interactor user)
    {
        if (isInteracting) return;
        isInteracting = true;
        interactorQuestMan = user.GetComponent<QuestManager>();
        dialogueRunner = new DialogueNodeRunner(this, user);
        inputManager.ToggleDialogueActions(true);
        inputManager.interactAction += ContinueDialogue;
        StartDialogue();
    }

    private void StartDialogue()
    {
        firstDialogue = defaultDialogue;
        interactorQuestMan.TryCompleteCurrentStep(InteractionType.StartInteraction, WorldEntity, gameObject);

        if (onDialogueGoInteractEventChannel != null)
            onDialogueGoInteractEventChannel.Raise(gameObject);

        RunDialogue(dialogueRunner.RunNode(firstDialogue));
    }

    private void ContinueDialogue()
    {
        if (dialogueRunner.CurrentNode.dialogueOptions.Count != 0) return;
        interactorQuestMan.TryCompleteCurrentStep(InteractionType.ContinueDialogue, WorldEntity, gameObject);
        RunDialogue(dialogueRunner.RunNextNode());
    }

    public void SelectOption(DialogueNode selectedNode) //called from ui
    {
        RunDialogue(dialogueRunner.RunNode(selectedNode));
    }

    private void RunDialogue(DialogueNode node)
    {
        if (!node)
        {
            ExitConversation();
            return;
        }

        node.characterName = characterName;
        onDialogueChangeEventChannel.Raise(node);
    }

    public void ExitConversation()
    {
        onDialogueExitEventChannel.Raise();
        inputManager.interactAction -= ContinueDialogue;
        isInteracting = false;
        inputManager.ToggleDialogueActions(false);
    }

    public void InteractPreview(Interactor user)
    {
    }

    public void InteractExit(Interactor user)
    {
    }
}