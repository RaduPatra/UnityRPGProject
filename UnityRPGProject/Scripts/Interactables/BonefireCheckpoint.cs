using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonefireCheckpoint : MonoBehaviour, IInteractable
{
    public Transform spawnTransform;
    public bool isFirstInteract = true;
    [SerializeField] private VoidEventChannel reloadSceneEventChannel;

    private void Awake()
    {
        SaveSystem.OnLoad += LoadCheckPoint;
        SaveSystem.OnInitSaveData += InitData;
    }

    private void OnDestroy()
    {
        SaveSystem.OnLoad -= LoadCheckPoint;
        SaveSystem.OnInitSaveData -= InitData;
    }

    private CheckPointSaveData checkPointSaveData;

    private void InitData()
    {
        checkPointSaveData = new CheckPointSaveData { isFirstInteraction = isFirstInteract };
        SaveData.Current.sceneData.checkpointData[GetComponent<GameObjectId>().Id] = checkPointSaveData;
    }

    private void Start()
    {
    }


    private void LoadCheckPoint(SaveData saveData)
    {
        isFirstInteract = saveData.sceneData.checkpointData[GetComponent<GameObjectId>().Id].isFirstInteraction;
        if (!isFirstInteract) fireParticleTransform.gameObject.SetActive(true);
    }

    [SerializeField] private Transform fireParticleTransform;
    private CharacterAnimator currentCharAnimator;

    public void Interact(Interactor user)
    {
        currentCharAnimator = user.GetComponentInParent<CharacterAnimator>();
        if (isFirstInteract)
        {
            isFirstInteract = false;
            currentCharAnimator.PlayAnimation(CharacterAnimator.takeItem, true);
            fireParticleTransform.gameObject.SetActive(true);
            checkPointSaveData.isFirstInteraction = isFirstInteract;
            SaveData.Current.sceneData.checkpointData[GetComponent<GameObjectId>().Id] = checkPointSaveData;
        }
        else
        {
            StartCoroutine(RestAtBonefireCo());
        }

        user.GetComponent<Health>().HealToMax();
        SaveData.Current.lastCheckPointPositionData = spawnTransform.position;
        SaveData.Current.lastLoadedScene = SceneManager.GetActiveScene().name;
        SaveSystemManager.Instance.Save();
    }

    public float secondsToSit = 2.25f;

    public IEnumerator RestAtBonefireCo()
    {
        currentCharAnimator.PlayAnimation(CharacterAnimator.sitGround, true);
        yield return new WaitForSeconds(secondsToSit);
        reloadSceneEventChannel.Raise();
    }

    public void InteractPreview(Interactor user)
    {
    }

    public void InteractExit(Interactor user)
    {
    }
}