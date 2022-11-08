using UnityEngine;

public class CompleteLocationQuestTrigger : MonoBehaviour, IWorldEntity
{
    [field : SerializeField]
    public WorldEntitySO WorldEntity { get; set; }

    public Transform locationToClear;

    [SerializeField]
    private QuestManager questManager;

    private void Awake()
    {
        locationToClear.GetComponent<EnemyLocationManager>().OnLocationCleared += LocationCleared;
    }

    private void LocationCleared()
    {
        // questManager.CompleteCurrentStepTest(InteractionType.TriggerInteraction, this);
        questManager.TryCompleteCurrentStep(InteractionType.StartInteraction, WorldEntity, gameObject);
    }

}