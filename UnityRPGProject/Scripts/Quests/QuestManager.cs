using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using Player;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<ActiveQuest> activeQuests = new List<ActiveQuest>();
    public List<ActiveQuest> completedQuests = new List<ActiveQuest>();

    private InventoryHolder inventoryHolder;


    [SerializeField] private QuestEventChannel onStartQuestEventChannel;
    [SerializeField] private QuestEventChannel onUpdateQuestStepEventChannel;
    [SerializeField] private QuestEventChannel onCompleteQuestEventChannel;
    [SerializeField] private QuestEventChannel loadQuestEventChannel;


    private void Awake()
    {
        inventoryHolder = GetComponent<InventoryHolder>();
        inventoryHolder.RegisterOnPickup(HandlePickupQuestItem);
        SaveSystem.OnLoad += LoadQuests;
        SaveSystem.OnInitSaveData += InitSaveData;
    }

    private void InitSaveData()
    {
        SaveData.Current.activeQuestsData = activeQuests;
        SaveData.Current.completedQuestsData = completedQuests;
    }

    private void LoadQuests(SaveData saveData)
    {

        activeQuests = saveData.activeQuestsData;
        completedQuests = saveData.completedQuestsData;
        InitQuestList(activeQuests);
        InitQuestList(completedQuests);

        void InitQuestList(List<ActiveQuest> questList)
        {
            foreach (var activeQuest in questList)
            {
                foreach (var step in activeQuest.Quest.questSteps)
                {
                    step.QuestOrigin = activeQuest;
                }
                if (loadQuestEventChannel != null)
                    loadQuestEventChannel.Raise(activeQuest);
            }
        }
    }

    private void OnDestroy()
    {
        inventoryHolder.UnRegisterOnPickup(HandlePickupQuestItem);
        SaveSystem.OnLoad -= LoadQuests;
    }

    private void HandlePickupQuestItem(ItemWithAttributes item)
    {
        TryCompleteCurrentStep(InteractionType.PickupItem, item, null);
    }

    public void AddQuest(ActiveQuest quest)
    {
        activeQuests.Add(quest);
        foreach (var step in quest.Quest.questSteps)
        {
            step.QuestOrigin = quest;
        }

        if (onStartQuestEventChannel != null)
            onStartQuestEventChannel.Raise(quest);

        if (onUpdateQuestStepEventChannel != null)
            onUpdateQuestStepEventChannel.Raise(quest);
    }

    public bool ContainsQuest(QuestSO quest)
    {
        return activeQuests.Any(activeQuest => activeQuest.Quest == quest);
    }

    public void TryCompleteCurrentStep(InteractionType interactionType, IDatabaseItem identifiableItem,
        GameObject sourceGo)
    {
        foreach (var activeQuest in activeQuests)
        {
            var currentStep = activeQuest.GetCurrentStep();
            if (currentStep == null) continue;
            if (currentStep.questEntityToInteractWith.Id != identifiableItem.Id) continue;

            //first try execute completion step actions
            if (currentStep.stepCompletionInteractions.ContainsKey(interactionType))
            {
                var canCompleteStepCondition =
                    RunStepComponent(interactionType, sourceGo, currentStep.stepCompletionInteractions);
                if (canCompleteStepCondition)
                {
                    //complete step
                    activeQuest.CurrentStepIndex++;
                    onUpdateQuestStepEventChannel.Raise(activeQuest);

                    if (activeQuest.CurrentStepIndex >= activeQuest.Quest.questSteps.Count)
                    {
                        if (onCompleteQuestEventChannel != null)
                            onCompleteQuestEventChannel.Raise(activeQuest);

                        activeQuests.Remove(activeQuest);

                        //todo - inv full case
                        foreach (var item in activeQuest.Quest.rewardItems)
                        {
                            inventoryHolder.PickUp(item);
                        }
                        completedQuests.Add(activeQuest);
                    }

                    break;
                }
            }

            //if no completion condition is met, execute normal actions
            //(such as the one that changes dialogue depending on the step state)
            if (currentStep.stepInteractions.ContainsKey(interactionType))
            {
                RunStepComponent(interactionType, sourceGo, currentStep.stepInteractions);
                break;
            }

            bool RunStepComponent(InteractionType interactionType, GameObject sourceGo,
                IReadOnlyDictionary<InteractionType, QuestStepComponent> interactionDict)
            {
                var currentStepCompleteCondition = interactionDict[interactionType].condition;
                var currentStepCompleteAction = interactionDict[interactionType].action;

                var canCompleteStepCondition = currentStepCompleteCondition.CanComplete(gameObject, sourceGo);
                currentStepCompleteAction.CompleteAction(gameObject, sourceGo, canCompleteStepCondition);
                return canCompleteStepCondition;
            }
        }
    }
}

public class QuestUIPair
{
    public GameObject questItemUI;
    public GameObject questStepListUI;
}


 