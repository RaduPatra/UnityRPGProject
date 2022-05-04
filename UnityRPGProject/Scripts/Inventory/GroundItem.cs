using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemWithAttributes item;
    [SerializeField] private ItemEventChannel itemPickupPreviewEventChannel;
    [SerializeField] private ItemEventChannel itemPickupExitPreviewEventChannel;

    public void Interact(Interactor user)
    {
        var inventoryHolder = user.GetComponent<InventoryHolder>();
        if (inventoryHolder == null) return;
        inventoryHolder.PickUp(item);
        Destroy(gameObject);
        Debug.Log(item.name);
        itemPickupExitPreviewEventChannel.Raise(item);
    }

    public void InteractPreview(Interactor user)
    {   
        Debug.Log("Press E to Pickup - " + item);
        itemPickupPreviewEventChannel.Raise(item);
    }
    
    public void InteractExit(Interactor user)
    {   
        itemPickupExitPreviewEventChannel.Raise(item);
    }
}