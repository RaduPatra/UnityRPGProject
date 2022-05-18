using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // private bool isOn = true;
    [SerializeField] private List<GameObject> objectsToToggle;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject inventoryFullUI;
    [SerializeField] private BoolEventChannel toggleInventoryFullEventChannel;
    private bool isInterfaceOn = false;

    public ItemInfoUI ItemInfoUI { get; set; }

    private void Awake()
    {
        AwakeInactive();
        ItemInfoUI = GetComponentInChildren<ItemInfoUI>(true);
        toggleInventoryFullEventChannel.Listeners += ToggleInventoryFull;
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

    private void Start()
    {
        inputManager.toggleUIAction += Toggle;
        inputManager.EnableGameplayActions();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
}