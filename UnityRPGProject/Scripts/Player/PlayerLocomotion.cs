using System;
using UnityEditor.Experimental;
using UnityEngine;
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
    [SerializeField] private float baseJumpHeight =2.5f;
    [SerializeField] private float sprintSpeedMultiplier = 2f;
    [SerializeField] private float inAirSpeedMultiplier = .5f;

    //ROTATION
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Gravity Settings")]
    //JUMP and FALL values
    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField] private float jumpHeight = 2.5f;
    [SerializeField] private float groundCheckRayDistance;
    [SerializeField] private float airTimeToBeginFall = .5f;

    [Header("References")]
    //REFERENCES
    [SerializeField]
    private CharacterController controller;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask layerMask;


    private PlayerManager playerManager;
    private Vector3 moveDirection;
    private Vector3 moveVelocity;
    private Vector3 verticalVelocity;
    private float moveSpeed = 7f;
    private float inAirTime;

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

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        walkMoveSpeed = baseWalkSpeed;
        jumpHeight = baseJumpHeight;
        //playerManager.isGrounded = false;
    }

    private void Update()
    {
        SetMoveDirection();
        HandleMovement();
        HandleRotation();
    }

    private void SetMoveDirection()
    {
        var horizontalInput = playerManager.InputManager.horizontalInput;
        var verticalInput = playerManager.InputManager.verticalInput;
        Vector2 direction = new Vector2(horizontalInput, verticalInput);
        moveDirection = Vector3.zero;

        //don't calculate move if interacting
        if (playerManager.isInteracting)
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
        CalculateMovementSpeed();
        HandleFalling();
        HandleJump();
        moveVelocity = moveDirection * (moveSpeed * Time.deltaTime);
        var finalVelocity = moveVelocity + verticalVelocity * Time.deltaTime;
        controller.Move(finalVelocity);
    }

    private void HandleFalling()
    {
        //Handle gravity. Reset if grounded.
        if (controller.isGrounded && verticalVelocity.y < 0)
            verticalVelocity.y = -2f;
        verticalVelocity.y += gravity * Time.deltaTime;

        inAirTime += Time.deltaTime;


        var isFalling = false;
        //play fall animation if not grounded and in air for more than x seconds
        if (!playerManager.isGrounded && inAirTime > airTimeToBeginFall)
        {
            playerManager.PlayerAnimator.PlayAnimation("Falling", false);
            isFalling = true;
        }

        RaycastHit hit;

        //check if player is grounded
        if (Physics.SphereCast(groundCheck.position, controller.radius, -Vector3.up, out hit, groundCheckRayDistance,
            layerMask))
        {
            //play land animation if player was previously not grounded and falling
            if (!playerManager.isGrounded && isFalling)
            {
                playerManager.PlayerAnimator.PlayAnimation("Landing", true);
            }

            playerManager.isGrounded = true;
            //reset air time if grounded
            inAirTime = 0;
        }
        else
        {
            playerManager.isGrounded = false;
        }
    }

    private void CalculateMovementSpeed()
    {
        var currentAirSpeedMultiplier = 1f;
        var currentSprintSpeedMultiplier = 1f;

        moveSpeed = walkMoveSpeed;

        if (!controller.isGrounded)
            currentAirSpeedMultiplier = inAirSpeedMultiplier;

        if (playerManager.InputManager.sprintInput)
            currentSprintSpeedMultiplier = sprintSpeedMultiplier;


        moveSpeed = walkMoveSpeed * currentSprintSpeedMultiplier * currentAirSpeedMultiplier;
    }

    private void HandleJump()
    {
        //jump if pressing space and grounded
        if (!playerManager.InputManager.jumpInput || !playerManager.isGrounded) return;

        verticalVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        playerManager.PlayerAnimator.PlayAnimation("Jump", false);
    }

    private void HandleRotation()
    {
        //rotate only if moving
        if (!(moveDirection.magnitude > .1f)) return;

        var targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}