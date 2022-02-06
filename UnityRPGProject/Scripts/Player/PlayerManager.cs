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
    public PlayerAttack PlayerAttack { get; private set; }
    public InventoryHolder InventoryHolder { get; private set; }

    //FLAGS
    public bool isGrounded;
    public bool isInteracting;
    
    private void Awake()
    {
        // InputManager = GetComponent<InputManager>();
        PlayerAnimator = GetComponent<PlayerAnimator>();
        PlayerAttack = GetComponent<PlayerAttack>();
        InventoryHolder = GetComponent<InventoryHolder>();
        //PlayerLocomotion = GetComponent<PlayerLocomotion>();
    }

}