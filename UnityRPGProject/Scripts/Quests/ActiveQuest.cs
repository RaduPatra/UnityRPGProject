using System;
using UnityEngine;

[Serializable]
public class ActiveQuest : ISerializationCallbackReceiver
{
    [field: SerializeField]

    // public QuestSO Quest { get; set; }
    public QuestSO Quest { get; set; }

    public string questId;

    [field: SerializeField] public int CurrentStepIndex { get; set; }

    public ActiveQuest(QuestSO quest)
    {
        Quest = quest;
        CurrentStepIndex = 0;
        questId = quest.Id;
    }

    public ActiveQuest(QuestSO quest, int currentStepIndex)
    {
        Quest = quest;
        CurrentStepIndex = currentStepIndex;
        questId = quest.Id;
    }

    public QuestStepSO GetCurrentStep()
    {
        return CurrentStepIndex == Quest.questSteps.Count ? null : Quest.questSteps[CurrentStepIndex];
    }

    public bool IsSameStep(QuestStepSO step)
    {
        return step == Quest.questSteps[CurrentStepIndex];
    }

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        if (questId == "") return;
        var saveManager = SaveSystemManager.Instance;
        if (saveManager == null) return;
        var db = saveManager.questDb;
        if (db == null) return;
        var foundQuest = db.GetById(questId);
        Quest = foundQuest;
    }
}

[Serializable]
public class QuestSaveData
{
    public string questId;
    public int currentQuestStep;
}

