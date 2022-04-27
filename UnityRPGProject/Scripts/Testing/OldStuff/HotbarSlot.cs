/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HotbarSlot
{
    public delegate void UpdateSlotEvent(HotbarSlot slot);

    public UpdateSlotEvent OnSlotAdded;
    public Action UseAction;
    
    [field: SerializeField] public Item Item { get; private set; }

    [field: SerializeField] public int Quantity { get; private set; }


    public void ResetSlot()
    {
        Item = null;
        Quantity = 0;
        OnSlotAdded?.Invoke(this);
    }

    public void AssignItem(Item item, int quantity, Action useStrategy)
    {
        Item = item;
        Quantity = quantity;
        UseAction = useStrategy;
        OnSlotAdded?.Invoke(this);
    }

    public void Use()
    {
        UseAction?.Invoke();
    }
}*/