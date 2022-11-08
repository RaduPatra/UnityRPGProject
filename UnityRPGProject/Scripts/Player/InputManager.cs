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
    private Vector2 movementInput;
    public InputActionReference mouseLookReference;

    public Action jumpAction = delegate { };
    public Action rollAction = delegate { };
    public Action interactAction = delegate { };
    public Action<Vector2> moveAction = delegate { };
    public Action<int> hotbarUseAction = delegate { };
    public Action sprintStartAction = delegate { };
    public Action sprintCancelledAction = delegate { };
    public Action toggleUIAction = delegate { };
    public Action attackAction = delegate { };
    public Action specialAttackAction = delegate { };
    public Action blockActionStart = delegate { };
    public Action blockActionPerformed = delegate { };
    public Action blockCancelledAction = delegate { };
    public Action cancelAction = delegate { };
    public Action interactTestAction = delegate { };
    public Action pauseGameAction = delegate { };

    //todo - set callbacks instead of manually subscribing
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            SetupControls();
        }

        // playerControls.General.Enable();
        // EnableGameplayActions();
        ToggleAllActions(true);

        mouseLookReference.action.Enable();
        Debug.Log("input enable");
    }

    private void SetupControls()
    {
        Debug.Log("setup controls");
        playerControls.Gameplay.Movement.performed += OnMove;
        playerControls.Gameplay.Jump.performed += OnJump;
        playerControls.Gameplay.Roll.performed += OnRoll;

        playerControls.Gameplay.Sprint.performed += OnSprint;
        playerControls.Gameplay.Sprint.canceled += OnSprint;

        playerControls.Gameplay.Attack.performed += OnAttack;
        playerControls.Gameplay.SpecialAttack.performed += OnSpecialAttack;

        playerControls.Gameplay.Block.performed += OnBlock;
        playerControls.Gameplay.Block.canceled += OnBlock;
        playerControls.Gameplay.Block.started += OnBlock;

        playerControls.General.Hotbar.performed += OnHotbarInput;
        playerControls.General.ToggleUI.performed += OnToggleUI;

        playerControls.Interaction.Interact.performed += OnInteract;
        playerControls.Interaction.Cancel.performed += CancelAction;
        playerControls.Interaction.DialogueInteractTest.performed += InteractTest;

        playerControls.PauseMenu.Pause.performed += PauseGame;

        playerControls.Testing.TestButton.started += i => Debug.Log("composite btn test");
    }

    private void PauseGame(InputAction.CallbackContext ctx)
    {
        Debug.Log("pause pressed");
        if (ctx.performed)
            pauseGameAction?.Invoke();
    }

    private void InteractTest(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            interactTestAction?.Invoke();
    }

    private void CancelAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            cancelAction?.Invoke();
    }

    private void OnSpecialAttack(InputAction.CallbackContext obj)
    {
        specialAttackAction?.Invoke();
    }

    private void OnToggleUI(InputAction.CallbackContext ctx)
    {
        toggleUIAction?.Invoke();
    }


    private void OnHotbarInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("key number = " + ctx.ReadValue<float>());
        var hotbarInput = (int)ctx.ReadValue<float>();
        hotbarUseAction.Invoke(hotbarInput);
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        // Debug.Log("jump");
        jumpAction?.Invoke();
    }

    private void OnRoll(InputAction.CallbackContext ctx)
    {
        // Debug.Log("roll");
        rollAction?.Invoke();
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        attackAction?.Invoke();
    }

    private void OnBlock(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Debug.Log("block started");
            blockActionStart?.Invoke();
        }

        if (ctx.performed)
        {
            Debug.Log("block performed");
            blockActionPerformed?.Invoke();
        }

        if (ctx.canceled)
        {
            Debug.Log("block canceled");
            blockCancelledAction?.Invoke();
        }
    }

    private void OnMove(InputAction.CallbackContext ctx) //called the frame wasd keys were pressed or released
    {
        movementInput = ctx.ReadValue<Vector2>();
        moveAction.Invoke(movementInput);
    }

    private void OnSprint(InputAction.CallbackContext ctx)
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

    public void EnableGameplayActions()
    {
        playerControls.Gameplay.Enable();
        playerControls.Interaction.Enable();
        mouseLookReference.action.Enable();
    }

    public void EnableInterfaceActions()
    {
        moveAction.Invoke(Vector2.zero);
        playerControls.Gameplay.Disable();
        playerControls.Interaction.Disable();
        mouseLookReference.action.Disable();
    }

    public void ToggleChestActions(bool status)
    {
        if (status)
        {
            moveAction.Invoke(Vector2.zero);
            playerControls.Gameplay.Disable();
            playerControls.General.Disable();
        }
        else
        {
            playerControls.Gameplay.Enable();
            playerControls.General.Enable();
        }
    }

    public void ToggleDialogueActions(bool status)
    {
        if (status)
        {
            moveAction.Invoke(Vector2.zero);
            playerControls.Gameplay.Disable();
            playerControls.General.Disable();
            mouseLookReference.action.Disable();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            playerControls.Gameplay.Enable();
            playerControls.General.Enable();
            mouseLookReference.action.Enable();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void DisableAllActions()
    {
        playerControls.Gameplay.Disable();
        playerControls.General.Disable();
    }

    public void ToggleAllActions(bool status)
    {
        if (status)
        {
            playerControls.Gameplay.Enable();
            playerControls.General.Enable();
            playerControls.Interaction.Enable();
            playerControls.PauseMenu.Enable();
        }
        else
        {
            moveAction.Invoke(Vector2.zero);
            playerControls.Gameplay.Disable();
            playerControls.General.Disable();
            playerControls.Interaction.Disable();
        }
    }

    private void OnDisable()
    {
        Debug.Log("input disable");
        playerControls.Disable();
        // DisableAllActions();
        ToggleAllActions(false);
    }

    [ContextMenu("test toggle")]
    public void TestToggle()
    {
        ToggleAllActions(true);
    }

    [ContextMenu("reset controls")]
    public void ResetControls()
    {
        playerControls = null;
    }
}