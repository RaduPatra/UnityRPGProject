using UnityEngine;

public abstract class InventoryItemAction : ScriptableObject
{
    public abstract void Execute(GameObject user, InventorySlot slot);
}