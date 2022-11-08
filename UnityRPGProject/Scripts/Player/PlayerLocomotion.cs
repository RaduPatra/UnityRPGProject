using System;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;


public class PlayerLocomotion : MonoBehaviour
{
    //SPEED

    [Header("Speed Settings")] [SerializeField]
    private float walkMoveSpeed = 7f;

    [SerializeField] private float baseWalkSpeed = 7f;
    [SerializeField] private float baseJumpHeight = 2.5f;
    [SerializeField] private float sprintSpeedMultiplier = 2f;
    [SerializeField] private float inAirSpeedMultiplier = .5f;
    [SerializeField] private float blockingSpeedMultiplier = .5f;

    //ROTATION
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Gravity Settings")]
    //JUMP and FALL values
    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float jumpTimeout = 0.5f;
    [SerializeField] private float groundCheckRayDistance;
    [SerializeField] private float airTimeToBeginFall = .5f;

    [SerializeField] private float sprintStaminaCost = 1;


    [Header("References")]
    //REFERENCES
    [SerializeField]
    public CharacterController controller;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private InputManager inputManager;


    private Stamina playerStamina;
    private Vector3 moveDirection;
    private Vector3 moveVelocity;
    private Vector3 verticalVelocity;
    private float moveSpeed = 7f;
    private float inAirTime;
    private float currentJumpTimeout;


    private Vector2 movementInput;
    private bool sprintInput;
    private bool blockInput;
    private bool isRayGrounded;

    public float WalkMoveSpeed
    {
        get => walkMoveSpeed;
        set => walkMoveSpeed = value;
    }

    public float JumpHeight
    {
        get => jumpHeight;
        set => jumpHeight = value;
    }


    public float BaseWalkSpeed => baseWalkSpeed;
    public float BaseJumpHeight => baseJumpHeight;


    private CharacterAnimator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<CharacterAnimator>();
        playerStamina = GetComponent<Stamina>();
        walkMoveSpeed = baseWalkSpeed;
        jumpHeight = baseJumpHeight;
        currentJumpTimeout = jumpTimeout;
        //playerManager.isGrounded = false;
        // SetupInput();
    }

    private void OnEnable()
    {
        SetupInput();
    }

    private void SetupInput()
    {
        inputManager.jumpAction += HandleJump;
        inputManager.rollAction += HandleRoll;
        inputManager.moveAction += OnMove;
        inputManager.sprintStartAction += OnSprintStart;
        inputManager.sprintCancelledAction += OnSprintCancel;

        inputManager.blockActionStart += OnBlockStart;
        inputManager.blockCancelledAction += OnBlockCancel;
    }


    private void OnDisable()
    {
        inputManager.jumpAction -= HandleJump;
        inputManager.rollAction -= HandleRoll;
        inputManager.moveAction -= OnMove;
        inputManager.sprintStartAction -= OnSprintStart;
        inputManager.sprintCancelledAction -= OnSprintCancel;
        inputManager.blockActionStart -= OnBlockStart;
        inputManager.blockCancelledAction -= OnBlockCancel;
    }

    private void Update()
    {
        SetMoveDirection();
        HandleMovement();
        HandleRotation();
        HandleJumpTimeout();
    }

    public bool canRotate;

    private void HandleRotation()
    {
        if (playerAnimator.IsRolling) return;
        if (playerAnimator.IsAiming || playerAnimator.GetCanRotate())
        {
            RotateToCameraDirection();
        }
        else
        {
            if (playerAnimator.IsInteracting) return;
            RotateToMovementDirection();
        }
    }


    private void SetMoveDirection()
    {
        var horizontalInput = movementInput.x;
        var verticalInput = movementInput.y;
        var direction = new Vector2(horizontalInput, verticalInput);
        moveDirection = Vector3.zero;

        //don't calculate move if interacting
        if (playerAnimator.IsInteracting)
            return;

        if (!(direction.magnitude > 0))
            return;

        //target angle is movement direction angle + camera angle
        var targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
        //transform rotation to a direction
        moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    private void HandleMovement()
    {
        HandleLocomotionAnimation();
        CalculateMovementSpeed();
        HandleFalling();
        moveVelocity = moveDirection * (moveSpeed * Time.deltaTime);
        var finalVelocity = moveVelocity + verticalVelocity * Time.deltaTime;
        controller.Move(finalVelocity);
    }

    //here we use controller.isGrounded for resetting fall exactly when we touch the ground so we fall constantly at the right speed
    //and playerManager.isGrounded for checking things just before they touch the ground

    private bool isFalling = false;
    private void HandleFalling()
    {
        //Handle gravity. Reset if grounded.
        if (controller.isGrounded && verticalVelocity.y < 0)
            verticalVelocity.y = -2f;
        verticalVelocity.y += gravity * Time.deltaTime;

        inAirTime += Time.deltaTime;


        // var isFalling = false;
        //play fall animation if not grounded and in air for more than x seconds
        if (!isRayGrounded && inAirTime > airTimeToBeginFall && !isFalling)
        {
            playerAnimator.PlayAnimation(CharacterAnimator.Falling, false, .3f);
            isFalling = true;
        }

        RaycastHit hit;


        //check if player is grounded
        if (Physics.SphereCast(groundCheck.position, controller.radius, -Vector3.up, out hit, groundCheckRayDistance,
            layerMask))
        {
            //play land animation if player was previously not grounded and falling
            if (!isRayGrounded && isFalling)
            {
                isFalling = false;
                playerAnimator.PlayAnimation(CharacterAnimator.Landing, true);
            }

            isRayGrounded = true;
            //reset air time if grounded
            inAirTime = 0;
        }
        else
        {
            isRayGrounded = false;
        }
    }

    private void CalculateMovementSpeed()
    {
        var currentAirSpeedMultiplier = 1f;
        var currentSprintSpeedMultiplier = 1f;
        var currentAimSpeedMultiplier = 1f;

        moveSpeed = walkMoveSpeed;

        if (!controller.isGrounded)
            currentAirSpeedMultiplier = inAirSpeedMultiplier;

        if (sprintInput)
        {
            if (playerStamina.CurrentStamina <= 0) return;
            currentSprintSpeedMultiplier = sprintSpeedMultiplier;
            playerStamina.Damage(sprintStaminaCost * Time.deltaTime);
        }

        if (playerAnimator.IsAiming)
            currentAimSpeedMultiplier = blockingSpeedMultiplier;

        moveSpeed = walkMoveSpeed * currentSprintSpeedMultiplier * currentAirSpeedMultiplier *
                    currentAimSpeedMultiplier;
    }


    private void HandleJumpTimeout()
    {
        if (currentJumpTimeout >= 0.0f)
            currentJumpTimeout -= Time.deltaTime;
    }

    private void HandleJump()
    {
        if (controller.isGrounded)
            currentJumpTimeout = -1;

        if (!isRayGrounded || currentJumpTimeout >= 0.0f) return;
        currentJumpTimeout = jumpTimeout;
        verticalVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        playerAnimator.PlayAnimation(CharacterAnimator.Jump, false);
    }


    [SerializeField] private float rollingStaminaCost = 20f;

    private void HandleRoll()
    {
        if (playerStamina.CurrentStamina <= 0) return;
        if (moveDirection.magnitude < .1f) return;
        playerAnimator.PlayAnimation(CharacterAnimator.Roll, true);
        playerAnimator.IsRolling = true;

        var dir = moveDirection;
        dir.y = 0;
        var targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = targetRotation;
        playerStamina.Damage(rollingStaminaCost);
    }

    private void RotateToMovementDirection()
    {
        //rotate only if moving
        if (!(moveDirection.magnitude > .1f)) return;

        var targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void RotateToCameraDirection()
    {
        var targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleLocomotionAnimation()
    {
        var moveAmount = Mathf.Clamp01(Mathf.Abs(movementInput.x) + Mathf.Abs(movementInput.y));

        if (sprintInput && moveAmount != 0 && playerStamina.CurrentStamina > 0)
            moveAmount = 2f;

        if (playerAnimator.IsAiming)
        {
            playerAnimator.UpdateLocomotionAnimation(movementInput.y, movementInput.x);
        }
        else
        {
            playerAnimator.UpdateLocomotionAnimation(moveAmount, 0);
        }
    }

    private void OnMove(Vector2 input) => movementInput = input;
    private void OnSprintStart() => sprintInput = true;
    private void OnSprintCancel() => sprintInput = false;


    private void OnBlockStart() => blockInput = true;
    private void OnBlockCancel() => blockInput = false;
}