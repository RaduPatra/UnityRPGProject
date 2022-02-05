using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameEvent<T> : GameEvent
{
    public event Action<T> Listeners = delegate { };
    public Func<T, bool> BoolListeners = delegate { return false; };

    public void Raise(T value)
    {
        Listeners?.Invoke(value);
    }
    
    public bool RaiseBool(T value)
    {
        return BoolListeners != null && BoolListeners.Invoke(value);
    }
}

public abstract class GameEvent : ScriptableObject
{
    public event Action VoidListeners = delegate { };

    public void RaiseVoid()
    {
        VoidListeners?.Invoke();
    }
}