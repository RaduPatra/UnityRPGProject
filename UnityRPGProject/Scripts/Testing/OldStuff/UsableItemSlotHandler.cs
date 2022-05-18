using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;


public class UsableItemSlotHandler : MonoBehaviour
{
    private ItemWithAttributes leftHandSlot;
    private ItemWithAttributes rightHandSlot;

    [SerializeField] private ItemCategory leftCategory;
    [SerializeField] private ItemCategory rightCategory;

    private void Awake()
    {
    }

    public void LoadItem(ItemWithAttributes item)
    {
        if (item.HasCategory(leftCategory))
        {
            //
            leftHandSlot = item;
        }
        else if (item.HasCategory(rightCategory))
        {
            //
            rightHandSlot = item;
        }
    }
    
    public void UnloadItem(ItemWithAttributes item)
    {
        if (item.HasCategory(leftCategory))
        {
            //
            leftHandSlot = null;
        }
        else if (item.HasCategory(rightCategory))
        {
            //
            rightHandSlot = null;
        }
    }
}