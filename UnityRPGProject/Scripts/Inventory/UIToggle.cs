using System;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    // private bool isOn = true;
    [SerializeField] private List<GameObject> objectsToToggle;
    private bool isOn = true;

    private void Update()
    {
        Toggle();
    }

    private void Toggle()
    {
        if (!Input.GetKeyDown(KeyCode.K)) return;
        isOn = !isOn;
        Cursor.visible = isOn;
        Cursor.lockState = isOn ? CursorLockMode.None : CursorLockMode.Locked;

        foreach (var obj in objectsToToggle)
        {
            obj.SetActive(isOn);
        }
    }
}