using System;
using UnityEngine;

public interface IEffect
{
    public void SetupEffect(GameObject target, Action<IEffect> onFinished);
    public void UpdateItem(float deltaTime);
}