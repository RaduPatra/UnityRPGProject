using System;
using System.Collections.Generic;
using ExtensionMethods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public Transform questListParent;
    public Transform stepListParent;

    [SerializeField] private QuestEventChannel onStartQuestEventChannel;
    [SerializeField] private QuestEventChannel onCompleteQuestEventChannel;
    [SerializeField] private QuestEventChannel onUpdateQuestStepEventChannel;
    [SerializeField] private QuestEventChannel loadQuestEventChannel;

    [SerializeField] private GameObject questStepListPrefab;
    [SerializeField] private GameObject questStepItemPrefab;
    [SerializeField] private GameObject questItemPrefab;
    [SerializeField] private ToggleGroup toggleGroup;

    private Dictionary<QuestSO, QuestUIPair> questUIs = new Dictionary<QuestSO, QuestUIPair>();

    private void Awake()
    {
        onStartQuestEventChannel.Listeners += AddQuest;
        onCompleteQuestEventChannel.Listeners += CompleteQuest;
        onUpdateQuestStepEventChannel.Listeners += UpdateStepList; //
        loadQuestEventChannel.Listeners += LoadQuest; //
    }

    private void OnDestroy()
    {
        onStartQuestEventChannel.Listeners -= AddQuest;
        onUpdateQuestStepEventChannel.Listeners -= UpdateStepList;
        onCompleteQuestEventChannel.Listeners -= CompleteQuest;
        loadQuestEventChannel.Listeners -= LoadQuest; //
    }

    private void AddQuest(ActiveQuest activeQuest)
    {
        var questItemGo = Instantiate(questItemPrefab, questListParent);
        var questStepListGo = Instantiate(questStepListPrefab, stepListParent);

        
        // toggle.onValueChanged.AddListener((x) => ShowCurrentQuestSteps(questStepListGo));
        var toggle = questItemGo.GetComponent<Toggle>();
        if (toggle)
        {
            toggle.onValueChanged.AddListener(delegate { ShowCurrentQuestSteps(questStepListGo); });
            toggle.group = toggleGroup;
            toggle.isOn = false;
            toggle.isOn = true;
            // toggle.Select();
        }

        // var btn = questItemGo.GetComponent<Button>();
        // if (btn)
        //     btn.onClick.AddListener(() => ShowCurrentQuestSteps(questStepListGo));
        
        var questItemText = questItemGo.GetComponentInChildren<TextMeshProUGUI>();
        questItemText.text = activeQuest.Quest.questName;

        questUIs[activeQuest.Quest] = new QuestUIPair
        {
            questItemUI = questItemGo,
            questStepListUI = questStepListGo
        };
        // ShowCurrentQuestSteps(questStepListGo); 
    }

    private void ShowCurrentQuestSteps(GameObject questStepListGo)
    {
        stepListParent.SetAllChildrenActive(false);
        questStepListGo.SetActive(true);
    }

    private void LoadQuest(ActiveQuest quest)
    {
        AddQuest(quest);
        for (int i = 0; i <= quest.CurrentStepIndex; i++)
        {
            UpdateStepList(new ActiveQuest(quest.Quest, i));
        }

        if (quest.CurrentStepIndex >= quest.Quest.questSteps.Count)
        {
            CompleteQuest(quest);
        }
    }


    private void UpdateStepList(ActiveQuest activeQuest)
    {
        //mark last step at completed
        if (activeQuest.CurrentStepIndex > 0)
        {
            var thisQuestList = questUIs[activeQuest.Quest].questStepListUI;
            var completedStep = thisQuestList.transform.GetChild(activeQuest.CurrentStepIndex - 1);
            completedStep.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            completedStep.GetComponentsInChildren<Image>()[1].enabled = true;

        }

        //add next step to list
        if (activeQuest.CurrentStepIndex < activeQuest.Quest.questSteps.Count)
        {
            var currStep = activeQuest.GetCurrentStep();
            var thisQuestList = questUIs[activeQuest.Quest].questStepListUI;
            var newStep = Instantiate(questStepItemPrefab, thisQuestList.transform);
            newStep.GetComponentInChildren<TextMeshProUGUI>().text = currStep.stepInstructions;
        }
    }


    private void CompleteQuest(ActiveQuest activeQuest)
    {
        questUIs[activeQuest.Quest].questItemUI.GetComponent<Image>().color = Color.green;
    }
}

