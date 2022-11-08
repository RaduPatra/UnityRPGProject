using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CollisionIgnorer : MonoBehaviour
{
    public Collider characterCollider;
    public Collider characterCollisionBlocker;
    public Collider[] allColliders;

    private void Awake()
    {
        BlockColl();
        allColliders = gameObject.GetComponentsInChildren<Collider>();
    }

    private void BlockColl()
    {
        if (characterCollider != null && characterCollisionBlocker != null)
            Physics.IgnoreCollision(characterCollider, characterCollisionBlocker, true);
    }

    [ContextMenu("Block collision")]
    public void BlockCollisionTest()
    {
        BlockColl();
    }

    public void IgnoreCollisionWithCollider(Collider colliderToIgnore)
    {
        foreach (var coll in allColliders)
        {
            Physics.IgnoreCollision(coll, colliderToIgnore, true);
        }
    }

    public void ToggleAllColliders(bool status)
    {
        foreach (var coll in allColliders)
        {
            coll.enabled = status;
        }
    }
    
    public void ToggleAllCollidersExcept(bool status, Collider[] excludedColliders)
    {
        foreach (var coll in allColliders)
        {
            if (!excludedColliders.Contains(coll))
            {
                coll.enabled = status;
            }
        }
    }
}