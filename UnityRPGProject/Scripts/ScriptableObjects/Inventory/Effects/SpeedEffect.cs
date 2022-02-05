using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Effect", menuName = "Effects/Speed Effect", order = 1)]
public class SpeedEffect : EffectBase
{
    public float speedModifier = 10f;

    private PlayerLocomotion player;

    //public override void UseEffect(GameObject target, Action<EffectBase> onFinished)
    public override void UseEffect(GameObject target)
    {
        player = target.GetComponent<PlayerLocomotion>();
        player.WalkMoveSpeed = speedModifier;
        timeRemaining = effectExpireTime;
    }

    public override void UpdateEffect(float deltaTime)
    {
        timeRemaining -= deltaTime;
        if (IsDone())
        {
            player.WalkMoveSpeed = player.BaseWalkSpeed;
        }
    }
}