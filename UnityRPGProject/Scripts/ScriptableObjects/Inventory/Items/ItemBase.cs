using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase: ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public GameObject dropItemPrefab;
    public bool isStackable;
    public int maxStack = 1;

    public virtual void UseTest(GameObject target)
    {
        
    }

    
}