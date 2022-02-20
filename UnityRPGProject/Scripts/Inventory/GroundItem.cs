using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    public ItemBase item;

    public void Interact(Interactor interactor)
    {
        //add to inventory
        // var user = interactor.gameObject.GetComponent<PlayerManager>();
        var user = interactor.PlayerManager;
        if (user == null) return;
        user.InventoryHolder.PickUp(item);
        Destroy(gameObject);
        Debug.Log(user.name);
        Debug.Log(item.name);
    }
}