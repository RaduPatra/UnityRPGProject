using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private PlayerManager playerManager;
    private Vector2 movementInput;
    [SerializeField] private InputActionReference mouseLookReference;
    [SerializeField] private InputActionAsset assetTest;



    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public bool sprintInput;
    public bool attackInput;
    public bool interactInput;
    public int hotbarInput = -1;
    public bool leftClickInputTest;
    
    


    //public PlayerLocomotion playerLocomotion;
    //todo implement the interface for setcallbacks so you dont have to manually subscribe to actions multiple times

    private void Awake()
    {
        // mouseLookReference
        mouseLookReference.action.Enable();
        playerManager = GetComponent<PlayerManager>();
    }


//todo move bindings to their corresponding action maps (eg. gameplay, ui)
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            SetupControls();
        }

        playerControls.Enable();
    }


    void SetupControls()
    {
        playerControls.PlayerMovement.Movement.performed += OnMove;
        playerControls.PlayerMovement.Jump.performed += OnJump;
        playerControls.PlayerMovement.Interact.performed += OnInteract;
        playerControls.PlayerMovement.Sprint.performed += i => sprintInput = true;
        playerControls.PlayerMovement.Sprint.canceled += i => sprintInput = false;
        playerControls.PlayerMovement.TestButton.started += i => Debug.Log("btntest");
        playerControls.PlayerMovement.MouseTest.performed += MouseTest;
        playerControls.PlayerAttack.Hotbar.performed += OnHotbarInput;
        //playerControls.PlayerAttack.Attack.performed += OnAttack;
    }

    private bool isOn = true;

    private void MouseTest(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx);
        leftClickInputTest = true;

        /*isOn = !isOn;
        if (isOn)
            playerControls.PlayerMovement.MouseLook.Disable();
        else playerControls.PlayerMovement.MouseLook.Enable();*/
    }

    private void OnHotbarInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("key number = " + ctx.ReadValue<float>());
        hotbarInput = (int)ctx.ReadValue<float>();
    }

    private void Update()
    {
        HandleLocomotionAnimation();

        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            
            Debug.Log("disable");
            Debug.Log(mouseLookReference.action);
            mouseLookReference.action.Disable();
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            Debug.Log("enable");
            mouseLookReference.action.Enable();


            // mouseLookReference.action.Enable();
        }
    }

    private void HandleLocomotionAnimation() // todo - ??? .... move this shit to another class
    {
        var moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (sprintInput && moveAmount != 0)
            moveAmount = 2f;

        playerManager.PlayerAnimator.UpdateLocomotionAnimation(moveAmount);
    }

    private void LateUpdate()
    {
        //reset inputs next frame
        jumpInput = false;
        interactInput = false;
        attackInput = false;
        interactInput = false;
        leftClickInputTest = false;
        hotbarInput = -1;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        jumpInput = true;
        //playerLocomotion.HandleJump();
        // Debug.Log("jumptgest23eeeeedeee");
    }

    private void OnAttack(InputAction.CallbackContext obj) //this is just for testing atm
    {
        attackInput = true;
        if (playerManager.isInteracting) return;
        playerManager.PlayerAttack.AttackAction();
        Debug.Log("attack");
    }

    private void OnMove(InputAction.CallbackContext ctx) //called the frame wasd keys were pressed or released
    {
        movementInput = ctx.ReadValue<Vector2>();
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        interactInput = true;
        //layerManager.i
        Debug.Log("interact");
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}