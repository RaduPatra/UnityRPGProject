using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Category", menuName = "ItemCategory", order = 1)]
public class ItemCategory : ScriptableObject
{
    [ReadOnly] public List<ItemCategory> categoryParents = new List<ItemCategory>();//private get?
    public List<AttributeBaseSO> attributes = new List<AttributeBaseSO>();

    private List<ItemCategory> oldCategories = new List<ItemCategory>();
    private List<AttributeBaseSO> oldAttributes = new List<AttributeBaseSO>();

    public HashSet<ItemCategory> ancestorCategories = new HashSet<ItemCategory>();
    public Action<ItemCategory> categoryChanged;

    public InventoryItemAction categoryUseAction;

    #region Testing

    public AttributeBaseSO attrToGetTest; //

    #endregion


    private void OnValidate()
    {
        // Debug.Log("CATEGORY On Validate" + name); //
        var categoriesSet = new HashSet<ItemCategory>(categoryParents);
        var categoryEquals = categoriesSet.SetEquals(oldCategories);

        var attributesSet = new HashSet<AttributeBaseSO>(attributes);
        var attributesEquals = attributesSet.SetEquals(oldAttributes);
        ancestorCategories = new HashSet<ItemCategory>(GetAllCategories());

        if (categoryEquals && attributesEquals) return;
        oldCategories = new List<ItemCategory>(categoryParents);
        oldAttributes = new List<AttributeBaseSO>(attributes);
        categoryChanged?.Invoke(this);
    }

    public T GetAttribute<T>(AttributeBaseSO atr) where T : AttributeBaseSO
    {
        foreach (var attr in attributes)
        {
            if (attr != atr) continue;
            var foundAttr = atr as T;
            return foundAttr;
        }

        foreach (var categ in categoryParents)
        {
            var foundAtr = categ.GetAttribute<T>(atr);
            //if we didn't find the attribute in this category, check the next one
            if (foundAtr != null) return foundAtr;
        }

        return default;
    }

    public bool HasCategory(ItemCategory category)
    {
        return ancestorCategories.Contains(category);
    }

    public List<ItemCategory> GetAllCategoriesTest()
    {
        return ancestorCategories.ToList();
    }

    public List<ItemCategory> GetAllCategories()
    {
        var containingCategories = new HashSet<ItemCategory> { this };
        foreach (var category in categoryParents)
        {
            var subCategories = category.GetAllCategories();
            containingCategories.AddRange(subCategories);
        }

        return containingCategories.ToList();
    }
    public bool ContainsCategory(ItemCategory categoryToCheck)
    {
        if (this == categoryToCheck) return true;
        foreach (var category in categoryParents)
        {
            if (category == categoryToCheck) return true;
            var result = category.ContainsCategory(categoryToCheck);
            if (result) return true;
        }

        return false;
    }

    #region Testing

    [ContextMenu("get categories test")]
    public void GetCategoriesTest()
    {
        var cat = GetAllCategories();
        foreach (var c in cat)
        {
            Debug.Log(c);
        }
    }

    [ContextMenu("get int atr test")]
    public void GetIntAttrTest()
    {
        var x = GetAttribute<IntAttributeSO>(attrToGetTest);
        Debug.Log(x);
    }

    [ContextMenu("get float atr test")]
    public void GetFloatAttrTest()
    {
        var x = GetAttribute<FloatAttributeSO>(attrToGetTest);
        Debug.Log(x);
    }

    [ContextMenu("reset events")]
    public void ResetEventsTest()
    {
        categoryChanged = null;
    }

    [ContextMenu("get events")]
    public void GetEventsTest()
    {
        var invList = categoryChanged?.GetInvocationList();
        if (invList == null) return;
        Debug.Log("Get events start");
        foreach (var item in invList)
        {
            Debug.Log(item.Method + " --- " + item.Target);
        }

        Debug.Log("Get events end");
    }

    [ContextMenu("get ancestors")]
    public void GetAncestors()
    {
        foreach (var cat in ancestorCategories)
        {
            Debug.Log(cat.name);
        }
    }

    #endregion
}