using UnityEditor;
using UnityEngine;

public abstract class ItemGameplayActions : ScriptableObject
{
    public abstract void StartAction(ItemWithAttributes item, GameObject go);

    public virtual void PerformedAction(ItemWithAttributes item, GameObject go)
    {
    }

    public virtual void CancelledAction(ItemWithAttributes item, GameObject go)
    {
    }

    public virtual void FinalizeAction(ItemWithAttributes item, GameObject go)
    {
    }
}