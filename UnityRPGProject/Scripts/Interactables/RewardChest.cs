using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class RewardChest : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemWithAttributes item;
    [SerializeField] private bool isInteracting;
    [SerializeField] private float lootOffset;
    [SerializeField] private GameObject chestLootBeamParticles;


    [SerializeField] private ItemEventChannel showChestLootEventChannel;
    [SerializeField] private ItemEventChannel hideChestLootEventChannel;
    [SerializeField] private BoolEventChannel toggleInventoryFullEventChannel;

    [SerializeField] private StringEventChannel interactPreviewEventChannel;
    [SerializeField] private VoidEventChannel interactExitPreviewEventChannel;

    [SerializeField] private InputManager inputManager;

    private const string ChestOpenAnimation = "ChestOpen";
    private const string ChestCloseAnimation = "ChestClose";


    private bool rewardTaken;
    private Animator animator;
    private Interactor chestUser;

    [SerializeField] private Transform debugPlayerTransform;
    [SerializeField] private bool debugDot;

    private void Awake()
    {

        animator = GetComponent<Animator>();
        SaveSystem.OnLoad += LoadChest;
        if (!canOpen) chestCanOpenParticlesTransform.gameObject.SetActive(true);


        if (chestLocation != null)
        {
            var locationManager = chestLocation.GetComponent<EnemyLocationManager>();
            if (locationManager != null)
                locationManager.OnLocationCleared += LocationCleared;
        }
        
    }


    private void OnDestroy()
    {
        SaveSystem.OnLoad -= LoadChest;
        inputManager.interactAction -= TryTakeReward;
        inputManager.cancelAction -= CloseChest;
    }

    private void Update()
    {
        if (!debugDot) return;
        var directionFromChestToPlayerTest = (debugPlayerTransform.position - transform.position).normalized;
        var dotTest = Vector3.Dot(debugPlayerTransform.forward, directionFromChestToPlayerTest);
        Debug.Log("Dot: " + dotTest);
    }

    private void LoadChest(SaveData saveData)
    {
        if (saveData.lootedChests.Contains(GetComponent<GameObjectId>().Id))
        {
            rewardTaken = true;
            animator.Play(ChestOpenAnimation);
            GetComponent<BoxCollider>().enabled = false;
            chestLootBeamParticles.SetActive(false);
            chestCanOpenParticlesTransform.gameObject.SetActive(false);
        }
        else
        {
            rewardTaken = false;
            animator.Play(ChestCloseAnimation);
            chestLootBeamParticles.SetActive(true);
            if (!canOpen) chestCanOpenParticlesTransform.gameObject.SetActive(true);

        }

        ExitChest();
        isInteracting = false;
        toggleInventoryFullEventChannel.Raise(false);
    }

    public Transform chestLocation;
    public Transform chestCanOpenParticlesTransform;
    private void LocationCleared()
    {
        canOpen = true;
        chestCanOpenParticlesTransform.gameObject.SetActive(false);
    }

    [SerializeField]
    private bool canOpen = true;

    public void Interact(Interactor user)
    {
        if (isInteracting) return;
        if (rewardTaken) return;
        if (!canOpen) return;

        //only interact if facing towards chest
        var directionFromChestToPlayer = (user.transform.position - transform.position).normalized;
        var dot = Vector3.Dot(user.transform.forward, directionFromChestToPlayer);
        if (dot > -1 + lootOffset) return;
        
        inputManager.ToggleChestActions(true);
        var playerAnimator = user.GetComponent<CharacterAnimator>();
        chestLootBeamParticles.GetComponent<ParticleSystem>().Play();
        isInteracting = true;
        chestUser = user;
        InteractExit(user);

        playerAnimator.PlayAnimation(CharacterAnimator.chestOpenAnimation, true);
        StartCoroutine(OpenChestAnimation(ChestOpenAnimation));
    }

    private IEnumerator OpenChestAnimation(string stateName)
    {
        yield return WaitForAnimation(stateName);
        OnChestOpenAnimEnd();
    }

    private IEnumerator CloseChestAnimation(string stateName)
    {
        yield return WaitForAnimation(stateName);
        OnChestCloseAnimEnd();
    }

    private IEnumerator WaitForAnimation(string stateName)
    {
        animator.Play(stateName);
        yield return new WaitForEndOfFrame(); //we need to wait for next animator update so it updates the length
        var anim = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log(anim.length + "  " + anim.normalizedTime);
        yield return new WaitForSeconds(anim.length);
    }

    private void OnChestOpenAnimEnd()
    {
        showChestLootEventChannel.Raise(item);
        inputManager.interactAction += TryTakeReward;
        inputManager.cancelAction += CloseChest;
    }

    private void OnChestCloseAnimEnd()
    {
        isInteracting = false;
        InteractPreview(chestUser);
    }

    private void TryTakeReward()
    {
        inputManager.interactAction -= TryTakeReward;
        inputManager.cancelAction -= CloseChest;

        var inventoryHolder = chestUser.GetComponent<InventoryHolder>();
        if (inventoryHolder == null) return;
        var pickupSuccess = inventoryHolder.PickUp(item);
        if (pickupSuccess)
        {
            rewardTaken = true;
            SaveData.Current.lootedChests.Add(GetComponent<GameObjectId>().Id);
            hideChestLootEventChannel.Raise(item);
            inputManager.ToggleChestActions(false);
            chestLootBeamParticles.SetActive(false); //set active for now, add cool fade effect on disable later
            var playerAnimator = chestUser.GetComponent<CharacterAnimator>();
            playerAnimator.PlayAnimation(CharacterAnimator.takeItem, true);
            GetComponent<BoxCollider>().enabled = false;
            return;
        }

        hideChestLootEventChannel.Raise(item);
        toggleInventoryFullEventChannel.Raise(true);
        inputManager.interactAction += ConfirmInventoryFull;
    }

    private void ConfirmInventoryFull()
    {
        inputManager.interactAction -= ConfirmInventoryFull;
        toggleInventoryFullEventChannel.Raise(false);
        StartCoroutine(CloseChestAnimation(ChestCloseAnimation));
        inputManager.ToggleChestActions(false);
    }

    private void CloseChest()
    {
        Debug.Log("Close Chest");
        ExitChest();
        StartCoroutine(CloseChestAnimation(ChestCloseAnimation));
    }

    private void ExitChest()
    {
        hideChestLootEventChannel.Raise(item);
        inputManager.interactAction -= TryTakeReward;
        inputManager.cancelAction -= CloseChest;
        if (isInteracting) inputManager.ToggleChestActions(false);
    }

    public void InteractPreview(Interactor user)
    {
        var previewText = "Press E to Loot Chest";
        interactPreviewEventChannel.Raise(previewText);
    }

    public void InteractExit(Interactor user)
    {
        interactExitPreviewEventChannel.Raise();
    }
}