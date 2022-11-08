using System;
using UnityEngine;

public class CompleteStepQuestTrigger : MonoBehaviour, IWorldEntity
{
    [field : SerializeField]
    public WorldEntitySO WorldEntity { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        var questManager = other.GetComponentInParent<QuestManager>();
        if (!questManager) return;
        // questManager.CompleteCurrentStepTest(InteractionType.TriggerInteraction, this);
        questManager.TryCompleteCurrentStep(InteractionType.TriggerInteraction, WorldEntity, gameObject);
    }
}