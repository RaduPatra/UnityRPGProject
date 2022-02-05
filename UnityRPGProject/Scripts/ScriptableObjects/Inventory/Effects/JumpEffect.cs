using System;
using Sirenix.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "New Effect", menuName = "Effects/Jump Effect", order = 1)]
public class JumpEffect : EffectBase
{
    public float jumpModifier = 10f;
    private PlayerLocomotion player;

    //public override void UseEffect(GameObject target, Action<EffectBase> onFinished)
    public override void UseEffect(GameObject target)
    {
        player = target.GetComponent<PlayerLocomotion>();
        player.JumpHeight = jumpModifier;
        timeRemaining = effectExpireTime;
    }

    public override void UpdateEffect(float deltaTime)
    {
        timeRemaining -= deltaTime;
        if (IsDone())
        {
            player.JumpHeight = player.BaseJumpHeight;
        }
    }
    
}