using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "InputReader", order = 1)]
public class InputManager : ScriptableObject
{
    //todo implement the interface for setcallbacks so you dont have to manually subscribe to actions multiple times
    private PlayerControls playerControls;
    private PlayerManager playerManager;
    private Vector2 movementInput;
    [SerializeField] private InputActionReference mouseLookReference;

    public Action jumpAction = delegate { };
    public Action interactAction = delegate { };
    public Action<Vector2> moveAction = delegate { };
    public Action<int> hotbarUseAction = delegate { };
    public Action sprintStartAction = delegate { };
    public Action sprintCancelledAction = delegate { };
    public Action toggleUIAction = delegate { };

    //todo - set callbacks instead of manually subscribing
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            SetupControls();
        }

        playerControls.General.Enable();
        EnableGameplayActions();
    }

    void SetupControls()
    {
        playerControls.Gameplay.Movement.performed += OnMove;
        playerControls.Gameplay.Jump.performed += OnJump;
        playerControls.Gameplay.Interact.performed += OnInteract;
        playerControls.Gameplay.Sprint.performed += OnSprint;
        playerControls.Gameplay.Sprint.canceled += OnSprint;
        playerControls.Gameplay.TestButton.started += i => Debug.Log("composite btn test");
        //playerControls.Gameplay.Attack.performed += OnAttack;
        playerControls.General.Hotbar.performed += OnHotbarInput;
        playerControls.General.ToggleUI.performed += OnToggleUI;
    }

    // private bool isOn = true;
    private void OnToggleUI(InputAction.CallbackContext ctx)
    {
        toggleUIAction?.Invoke();
    }

    public void EnableGameplayActions()
    {
        playerControls.Gameplay.Enable();
        mouseLookReference.action.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnableInterfaceActions()
    {
        playerControls.Gameplay.Disable();
        mouseLookReference.action.Disable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void DisableAllActions()
    {
        playerControls.Gameplay.Disable();
        playerControls.General.Disable();
    }

    private void OnHotbarInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("key number = " + ctx.ReadValue<float>());
        var hotbarInput = (int)ctx.ReadValue<float>();
        hotbarUseAction.Invoke(hotbarInput);
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        Debug.Log("jump");
        jumpAction?.Invoke();
    }

    private void OnAttack(InputAction.CallbackContext ctx) //this is just for testing atm
    {
        // attackInput = true;
        if (playerManager.isInteracting) return;
        playerManager.PlayerAttack.AttackAction();
        Debug.Log("attack");
    }

    private void OnMove(InputAction.CallbackContext ctx) //called the frame wasd keys were pressed or released
    {
        movementInput = ctx.ReadValue<Vector2>();
        moveAction.Invoke(movementInput);
    }

    private void OnSprint(InputAction.CallbackContext ctx) //called the frame wasd keys were pressed or released
    {
        if (ctx.performed)
            sprintStartAction?.Invoke();

        if (ctx.canceled)
            sprintCancelledAction?.Invoke();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        interactAction?.Invoke();
        Debug.Log("interact");
    }

    private void OnDisable()
    {
        playerControls.Disable();
        DisableAllActions();
    }
}