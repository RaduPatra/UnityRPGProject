using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Unity.Mathematics;
using UnityEngine.AI;

public abstract class StateAction : IStateComponent
{
    public abstract void OnUpdate();

    public abstract void OnStateEnter();

    public abstract void OnStateExit();
}