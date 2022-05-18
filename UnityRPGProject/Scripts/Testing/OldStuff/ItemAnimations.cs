using System;
using UnityEngine;

[Serializable]
public class ItemAnimations
{
    public AnimationClip leftIdle;
    public AnimationClip rightIdle;
    public AnimationClip attack;

    public ItemAnimations(ItemAnimations animations)
    {
        leftIdle = animations.leftIdle;
        rightIdle = animations.rightIdle;
        attack = animations.attack;
    }

    public ItemAnimations()
    {
        
    }
}