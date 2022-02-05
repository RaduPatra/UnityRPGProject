using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    public void AttackAction()
    {
        //attack and enable root motion. Root motion gets disabled again on attack state exit.
        playerManager.PlayerAnimator.animator.SetBool(PlayerAnimator.RootMotionOn, true);
        playerManager.PlayerAnimator.PlayAnimation( "Attack", true);
    }
}