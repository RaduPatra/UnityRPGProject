using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    //REFERENCES
    public PlayerAnimator PlayerAnimator { get; private set; }
    public InventoryHolder InventoryHolder { get; private set; }

    //FLAGS
    public bool isGrounded;
    public bool isInteracting;
    
    [SerializeField]
    private bool isAiming;
    
    public bool IsAiming
    {
        get => isAiming;
        set
        {
            isAiming = value;
            PlayerAnimator.animator.SetBool(PlayerAnimator.IsAiming, value);
        }
    }


    private void Awake()
    {
        // InputManager = GetComponent<InputManager>();
        PlayerAnimator = GetComponent<PlayerAnimator>();
        // PlayerAttack = GetComponent<PlayerAttack>();
        InventoryHolder = GetComponent<InventoryHolder>();
        //PlayerLocomotion = GetComponent<PlayerLocomotion>();
    }
}