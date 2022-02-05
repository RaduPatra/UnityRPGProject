/*using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine.EventSystems;

public class PlayerHotbarManager : MonoBehaviour
{
    private Inventory playerHotbar;
    private List<EffectBase> activeEffects = new List<EffectBase>();
    private List<EffectBase> effectsToRemove = new List<EffectBase>();
    private Action<EffectBase> removeEffectAction;

    

    private EquipmentManager equipmentManager;
    private void Awake()
    {
        //todo - make separate holder for hotbar. Hotbar inventory will have different inventory slot type
        playerHotbar = GetComponent<InventoryHolder>().HotbarInventory;
        equipmentManager = GetComponent<EquipmentManager>();
        removeEffectAction = AddEffectToRemove;
    }

    // private bool isEquippped = false;
    public EquipableItem equippedItem;

    private void Update()
    {
        for (int i = 0; i < playerHotbar.ItemList.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                var usedSlot = playerHotbar.ItemList[i];
                if (usedSlot.Item is ConsumableItem consumable)
                {
                    consumable.Use(gameObject, removeEffectAction);
                    AddEffect(consumable.useEffect);
                }
                /*else if (usedSlot.Item is EquipableItem equipable)
                {
                    equipmentManager.EquipItem(usedSlot);
                    // equipmentManager.EquipItem(equipable);
                    
                    
                    
                    
                    
                    /*if (!equippedItem)
                    {
                        equipable.Equip(equipLocation);
                        equippedItem = equipable;
                    }
                    else if (equippedItem == equipable)
                    {
                        equipable.UnEquip(equipLocation);
                        equippedItem = null;
                    }
                    else
                    {
                        equippedItem.UnEquip(equipLocation);
                        equipable.Equip(equipLocation);
                        equippedItem = equipable;
                    }#2#
                }#1#
            }
        }

        UpdateEffects();
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
        /*item.UseEffect(gameObject, removeEffectAction);
        item.UseItem(gameObject);
        item.SetupEffect(gameObject, removeAction);#1#
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
        }

        foreach (var effect in effectsToRemove)
        {
            activeEffects.Remove(effect);
        }

        effectsToRemove.Clear();
    }
}*/