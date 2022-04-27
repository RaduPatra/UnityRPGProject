using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // private bool isOn = true;
    [SerializeField] private List<GameObject> objectsToToggle;
    [SerializeField] private InputManager inputManager;
    private bool isInterfaceOn = false;
    public Dictionary<ItemCategory, InventorySlotUI> lastEquippedSlots = new Dictionary<ItemCategory, InventorySlotUI>();

    private void Awake()
    {
        AwakeInactive();
    }

    private void AwakeInactive()
    {
        Debug.Log("stats ui awake");

        foreach (var obj in objectsToToggle)
        {
            var statsUI = obj.GetComponent<StatsUI>();
            if (statsUI != null)
            {
                statsUI.Setup();
            }
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
}