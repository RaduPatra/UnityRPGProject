using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ExtensionMethods;

public class DialogueUI : MonoBehaviour, IInitializable
{
    public DialogueEventChannel onDialogueChangeEventChannel;
    public VoidEventChannel onDialogueExitEventChannel;
    public GameObjectEventChannel onDialogueGoInteractEventChannel;


    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI characterNameText;
    public Transform optionsListParent;
    public GameObject optionsPrefab;

    public void Initialize()
    {
        onDialogueChangeEventChannel.Listeners += ShowDialogue;
        onDialogueExitEventChannel.Listeners += ExitDialogue;
        onDialogueGoInteractEventChannel.Listeners += SetSelectionMethod;
    }

    public void Destroy()
    {
        onDialogueChangeEventChannel.Listeners -= ShowDialogue;
        onDialogueExitEventChannel.Listeners -= ExitDialogue;
        onDialogueGoInteractEventChannel.Listeners -= SetSelectionMethod;
    }

    private void ExitDialogue()
    {
        gameObject.SetActive(false);
    }

    private Action<DialogueNode> OnSelectDialogue;

    private void SetSelectionMethod(GameObject go)
    {
        OnSelectDialogue = go.GetComponent<DialogueSource>().SelectOption;
    }

    private void ShowDialogue(DialogueNode dialogue)
    {
        gameObject.SetActive(true);
        optionsListParent.gameObject.SetActive(false);

        dialogueText.text = dialogue.dialogueText;
        characterNameText.text = dialogue.characterName;

        optionsListParent.transform.Clear();

        if (dialogue.dialogueOptions.Count > 0)
            optionsListParent.gameObject.SetActive(true);

        foreach (var option in dialogue.dialogueOptions)
        {
            var optionPrefabGo = Instantiate(optionsPrefab, optionsListParent);
            var optionBtn = optionPrefabGo.GetComponent<Button>();
            var optionUI = optionPrefabGo.GetComponent<DialogueOptionUI>();
            optionUI.SetText(option.optionText);
            optionBtn.onClick.AddListener(() => OnSelectDialogue?.Invoke(option.optionNode));
        }
    }
}