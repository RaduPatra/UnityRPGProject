using System;
using System.Collections;
using Sirenix.Utilities;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    private CharacterAnimator characterAnimator;
    private CharacterController characterController;
    private CollisionIgnorer collisionIgnorer;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Vector3 lastCheckPointPos;
    [SerializeField] private VoidEventChannel reloadSceneEventChannel;
    [SerializeField] private VoidEventChannel deathEventChannel;
    // [SerializeField] private StringEventChannel beforeSceneLoadEventChannel;

    private void Awake()
    {
        GetComponent<Health>().OnDeath += HandleDeath;
        // beforeSceneLoadEventChannel.Listeners += BeforeNewSceneLoad;
        characterAnimator = GetComponent<CharacterAnimator>();
        characterController = GetComponent<CharacterController>();
        collisionIgnorer = GetComponent<CollisionIgnorer>();
        SaveSystem.OnLoad += Load;
        SaveSystem.OnInitSaveData += InitSave;
    }

    private void BeforeNewSceneLoad(string sceneName)
    {
    }

    private void InitSave()
    {
        SaveData.Current.lastCheckPointPositionData = transform.position;
    }

    private void OnDestroy()
    {
        SaveSystem.OnLoad -= Load;
        SaveSystem.OnInitSaveData -= InitSave;
        // beforeSceneLoadEventChannel.Listeners -= BeforeNewSceneLoad;

    }

    private void Load(SaveData saveData)
    {
        lastCheckPointPos = saveData.lastCheckPointPositionData;
        StartCoroutine(RespawnToLastCheckpointTest());
    }

    private void HandleDeath()
    {
        characterAnimator.PlayAnimation(CharacterAnimator.death, true);
        inputManager.ToggleAllActions(false);

        Collider[] excludedColliders = { GetComponent<CharacterController>() };
        collisionIgnorer.ToggleAllCollidersExcept(false, excludedColliders);
        deathEventChannel.Raise();

        Debug.Log("handle death");
        StartCoroutine(LoadLastSave());
        // StartCoroutine(RespawnToLastCheckpoint());
    }

    private IEnumerator LoadLastSave()
    {
        yield return new WaitForSeconds(3f);
        reloadSceneEventChannel.Raise();
        Debug.Log("loaded");
    }

    private IEnumerator RespawnToLastCheckpointTest()
    {
        yield return new WaitForSeconds(.1f);

        transform.position = new Vector3(0,1000,0);

        characterController.enabled = false;
        transform.position = lastCheckPointPos;
        characterController.enabled = true;
        // characterAnimator.PlayAnimation(CharacterAnimator.respawn, true);
        characterAnimator.PlayAnimation(CharacterAnimator.getUpGround, true);
        collisionIgnorer.ToggleAllColliders(true);

        Debug.Log("respawned");
    }
}