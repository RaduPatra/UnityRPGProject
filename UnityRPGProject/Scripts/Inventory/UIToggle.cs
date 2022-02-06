using System;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    // private bool isOn = true;
    [SerializeField] private List<GameObject> objectsToToggle;
    [SerializeField] private InputManager inputManager;
    private bool isInterfaceOn = false;

    private void Start()
    {
        inputManager.toggleUIAction += Toggle;
    }

    private void Toggle()
    {
        isInterfaceOn = !isInterfaceOn;

        if (isInterfaceOn)
        {
            inputManager.EnableInterfaceActions();
        }
        else
        {
            inputManager.EnableGameplayActions();
        }

        foreach (var obj in objectsToToggle)
        {
            obj.SetActive(isInterfaceOn);
        }
    }
}