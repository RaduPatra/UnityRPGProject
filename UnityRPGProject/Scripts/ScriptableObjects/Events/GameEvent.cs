using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameEvent<T> : ScriptableObject
{
    public event Action<T> Listeners = delegate { };
    public Func<T, bool> BoolListeners = delegate { return false; };//make separate?

    public void Raise(T value)
    {
        Listeners?.Invoke(value);
    }
    
    public bool RaiseBool(T value)
    {
        return BoolListeners != null && BoolListeners.Invoke(value);
    }
}

public abstract class VoidGameEvent : ScriptableObject
{
    public event Action Listeners = delegate { };
    public void Raise()
    {
        Listeners?.Invoke();
    }
}

/*public class ItemBoolFuncEventChannel : BoolFuncGameEvent<ItemWithAttributes>
{

}
public abstract class BoolFuncGameEvent<T> : FuncGameEvent<T,bool>
{
    public Func<T, bool> Listeners = delegate { return false; };//make separate?

    public bool Raise(T value)
    {
        return Listeners != null && Listeners.Invoke(value);
    }
}

public abstract class FuncGameEvent<T, U> : ScriptableObject
{
    public Func<T, U> Listeners = delegate { return default; };//make separate?

    public U RaiseBool(T value)
    {
        return Listeners.Invoke(value);
    }
}*/
