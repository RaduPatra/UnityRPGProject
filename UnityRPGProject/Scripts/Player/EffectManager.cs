using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private Inventory playerHotbar;
    private List<EffectBase> activeEffects = new List<EffectBase>();
    private List<EffectBase> effectsToRemove = new List<EffectBase>();
    private Action<EffectBase> removeEffectAction;
    private InputManager inputManager;

    private void Awake()
    {
        //todo - make separate holder for hotbar. Hotbar inventory will have different inventory slot type
        inputManager = GetComponent<InputManager>();
        playerHotbar = GetComponent<InventoryHolder>().HotbarInventory;
        removeEffectAction = AddEffectToRemove;
    }

    private void Update()
    {
        /*if (inputManager.hotbarInput >= 0 && inputManager.hotbarInput < playerHotbar.ItemList.Count)
        {
            var slot = playerHotbar.ItemList[inputManager.hotbarInput];
            ConsumeItem(slot);
        }
        */

        UpdateEffects();
    }

    public void ConsumeItem(InventorySlot slot)
    {
        if (!(slot.itemStack.item is ConsumableItem consumable)) return;
        consumable.Use(gameObject);
        slot.DecreaseQuantity();
        // AddEffect(consumable.useEffect);
        AddEffects(consumable.effects);
    }

    private void AddEffects(EffectBase[] effects)
    {
        foreach (var effect in effects)
        {
            AddEffect(effect);
        }
    }

    private void AddEffect(EffectBase item)
    {
        //only apply the last effect of the same type that was used
        var sameType = GetEffectOfSameType(item);
        if (sameType != null)
        {
            effectsToRemove.Add(sameType);
        }

        activeEffects.Add(item);
    }

    private EffectBase GetEffectOfSameType(EffectBase item)
    {
        foreach (var effect in activeEffects)
        {
            if (item.GetType() == effect.GetType())
            {
                Debug.Log("same type " + item.GetType());
                return effect;
            }
        }

        return null;
    }

    private void AddEffectToRemove(EffectBase item)
    {
        effectsToRemove.Add(item);
        Debug.Log("effect removed------------");
    }

    private void UpdateEffects()
    {
        foreach (var item in activeEffects)
        {
            item.UpdateEffect(Time.deltaTime);

            if (item.IsDone())
            {
                AddEffectToRemove(item);
            }
        }

        foreach (var effect in effectsToRemove)
        {
            activeEffects.Remove(effect);
        }

        effectsToRemove.Clear();
    }
}