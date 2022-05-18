
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

public class ItemCategoryTest : ScriptableObject, IDatabaseItem
{

    [SerializeField]
    public string id= Guid.NewGuid().ToString(); 
    public string Id => id;
    
    [SerializeField]
    public string testString= "default"; 
    public string testString2= "default2"; 
    public string TestString => testString;


    
    [ReadOnly] public List<ItemCategoryTest> categoryParents = new List<ItemCategoryTest>();//private get?
    public List<AttributeBaseSO> attributes = new List<AttributeBaseSO>();

    private List<ItemCategoryTest> oldCategories = new List<ItemCategoryTest>();
    private List<AttributeBaseSO> oldAttributes = new List<AttributeBaseSO>();

    public HashSet<ItemCategoryTest> ancestorCategories = new HashSet<ItemCategoryTest>();
    public Action<ItemCategoryTest> categoryChanged;

    public InventoryItemAction categoryUseAction;

    #region Testing

    public AttributeBaseSO attrToGetTest; //

    #endregion


    private void OnValidate()
    {
        // Debug.Log("CATEGORY On Validate" + name); //
        var categoriesSet = new HashSet<ItemCategoryTest>(categoryParents);
        var categoryEquals = categoriesSet.SetEquals(oldCategories);

        var attributesSet = new HashSet<AttributeBaseSO>(attributes);
        var attributesEquals = attributesSet.SetEquals(oldAttributes);
        ancestorCategories = new HashSet<ItemCategoryTest>(GetAllCategories());

        if (categoryEquals && attributesEquals) return;
        oldCategories = new List<ItemCategoryTest>(categoryParents);
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

    public bool HasCategory(ItemCategoryTest category)
    {
        return ancestorCategories.Contains(category);
    }

    public List<ItemCategoryTest> GetAllCategoriesTest()
    {
        return ancestorCategories.ToList();
    }

    public List<ItemCategoryTest> GetAllCategories()
    {
        var containingCategories = new HashSet<ItemCategoryTest> { this };
        foreach (var category in categoryParents)
        {
            var subCategories = category.GetAllCategories();
            containingCategories.AddRange(subCategories);
        }

        return containingCategories.ToList();
    }
    public bool ContainsCategory(ItemCategoryTest categoryToCheck)
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