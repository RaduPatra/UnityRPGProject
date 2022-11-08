using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    // private bool isOn = true;
    [SerializeField] private List<GameObject> objectsToToggle;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject inventoryFullUI;
    [SerializeField] private BoolEventChannel toggleInventoryFullEventChannel;
    private bool isInterfaceOn = false;

    [SerializeField] private QuestEventChannel onStartQuestEventChannel;
    [SerializeField] private QuestEventChannel onUpdateQuestStepEventChannel;

    public ItemInfoUI ItemInfoUI { get; set; }

    private void Awake()
    {
        AwakeInactive();
        ItemInfoUI = GetComponentInChildren<ItemInfoUI>(true);
        
    }

    private void OnEnable()
    {
        inputManager.toggleUIAction += Toggle;
        toggleInventoryFullEventChannel.Listeners += ToggleInventoryFull;
        
        onStartQuestEventChannel.Listeners += ShowStartQuest;
        onUpdateQuestStepEventChannel.Listeners += ShowUpdateQuest;
    }

    private void Start()
    {
        inputManager.EnableGameplayActions();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void AwakeInactive()
    {
        Debug.Log("stats ui awake");
        var objectsToInit = GetComponentsInChildren<IInitializable>(true);
        foreach (var item in objectsToInit)
        {
            item.Initialize();
        }
    }

    private void OnDisable()
    {
        toggleInventoryFullEventChannel.Listeners -= ToggleInventoryFull;
        inputManager.toggleUIAction -= Toggle;
        
        onStartQuestEventChannel.Listeners -= ShowStartQuest;
        onUpdateQuestStepEventChannel.Listeners -= ShowUpdateQuest;
    }

    private void OnDestroy()
    {
        var objectsToInit = GetComponentsInChildren<IInitializable>(true);
        foreach (var item in objectsToInit)
        {
            item.Destroy();
        }
    }

    private void Toggle()
    {
        isInterfaceOn = !isInterfaceOn;

        if (isInterfaceOn)
        {
            inputManager.EnableInterfaceActions();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            inputManager.EnableGameplayActions();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        foreach (var obj in objectsToToggle)
        {
            obj.SetActive(isInterfaceOn);
        }
    }

    private void ToggleInventoryFull(bool status)
    {
        inventoryFullUI.SetActive(status);
    }
    
    //todo - move this to quest manager ui
    [SerializeField] private Transform questStatusParent;
    [SerializeField] private TextMeshProUGUI questNameTmp;
    
    [SerializeField] private Transform updatedQuestParent;
    [SerializeField] private TextMeshProUGUI updatedQuestName;

    

    private void ShowStartQuest(ActiveQuest quest)
    {
        StartCoroutine(ShowQuestStatusCo(quest));
    }

    private IEnumerator ShowQuestStatusCo(ActiveQuest quest)
    {
        yield return new WaitForSeconds(1f);
        questStatusParent.gameObject.SetActive(true);
        questNameTmp.text = quest.Quest.questName;
        yield return new WaitForSeconds(3f);
        questStatusParent.gameObject.SetActive(false);
    }
    
    private void ShowUpdateQuest(ActiveQuest quest)
    {
        StartCoroutine(ShowUpdateQuestCo(quest));
    }

    private IEnumerator ShowUpdateQuestCo(ActiveQuest quest)
    {
        yield return new WaitForSeconds(1f);
        updatedQuestName.text = quest.Quest.questName;
        updatedQuestParent.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        updatedQuestParent.gameObject.SetActive(false);
    }
}