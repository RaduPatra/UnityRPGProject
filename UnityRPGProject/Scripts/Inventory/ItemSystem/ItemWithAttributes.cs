using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Test Item", menuName = "Item", order = 1)]
public class ItemWithAttributes : SerializedScriptableObject, IDatabaseItem
{
    [SerializeField]
    private string id = Guid.NewGuid().ToString();

    public string Id => id;
    public string testString = "default";


    [SerializeField] private ItemCategory categoryParent;
    private ItemCategory lastCategory;

    [DictionaryDrawerSettings(IsReadOnly = true), DictionaryHideReferenceObjectPicker, SerializeField]
    private Dictionary<AttributeBaseSO, AttributeDataBase> attributes =
        new Dictionary<AttributeBaseSO, AttributeDataBase>();

    public bool isStackable;
    public int maxStack = 1;

    #region Testing

    public AttributeBaseSO attrToGetTest;
    public ItemCategory categToGetTest;

    #endregion


#if UNITY_EDITOR
    private void OnValidate()
    {
        // Debug.Log("ITEM On Validate" + name);
        var allCategories = new List<ItemCategory>();
        if (categoryParent != null)
            allCategories = categoryParent.GetAllCategories();

        RefreshEvents(allCategories);
        if (lastCategory == categoryParent) return;
        CategoryChanged(categoryParent);
    }

    //called in editor when a category changes to update this item attributes
    private void CategoryChanged(ItemCategory changedCategory)
    {
        // if (changedCategory == null) return;
        // Debug.Log("CategoryChanged----!!!");

        var allCategories = new List<ItemCategory>();
        if (changedCategory != null)
            allCategories = categoryParent.GetAllCategories();
        RefreshEvents(allCategories);

        var allAttributes = new List<AttributeBaseSO>();
        //get attributes from categories
        foreach (var category in allCategories)
        {
            allAttributes.AddRange(category.attributes);
        }

        //add new attributes
        foreach (var attr in allAttributes)
        {
            if (attr == null) continue;

            if (attributes.Any(x => x.Key == attr) == false)
            {
                var data = attr.CreateData();
                attributes[attr] = data;
            }
        }

        var attributesToRemove = new List<AttributeBaseSO>();
        foreach (var attr in attributes)
        {
            if (allAttributes.Contains(attr.Key) == false)
            {
                attributesToRemove.Add(attr.Key);
            }
        }

        foreach (var attr in attributesToRemove)
        {
            attributes.Remove(attr);
        }

        lastCategory = categoryParent;
    }

    private void RefreshEvents(List<ItemCategory> allCategories)
    {
        if (lastCategory != null)
        {
            foreach (var category in lastCategory.GetAllCategories())
            {
                category.categoryChanged -= CategoryChanged;
            }
        }

        foreach (var category in allCategories)
        {
            category.categoryChanged -= CategoryChanged;
            category.categoryChanged += CategoryChanged;
        }
    }
#endif
    public T GetAttribute<T>(AttributeBaseSO atr) where T : AttributeDataBase
    {
        attributes.TryGetValue(atr, out var data);
        return data as T;
    }


    public bool HasCategory(ItemCategory category)
    {
        return categoryParent.HasCategory(category);
    }

    #region Testing

    [ContextMenu("Reset test")]
    public void ResetTest()
    {
        categoryParent = default;
        lastCategory = default;
        attributes = default;
        // attrToAddTest = default;
    }

    [ContextMenu("Get values test")]
    public void GetValTest()
    {
        Debug.Log("Get vals---");
        Debug.Log(categoryParent);
        Debug.Log(lastCategory);
        Debug.Log(attributes);
        // Debug.Log(attrToAddTest);
        Debug.Log("---");
    }

    [ContextMenu("get events")]
    public void GetEventsTest()
    {
        var allCat = new List<ItemCategory>();
        if (categoryParent != null)
            allCat = categoryParent.GetAllCategories();
        foreach (var cat in allCat)
        {
            if (cat.categoryChanged == null) continue;
            var invList = cat.categoryChanged.GetInvocationList();
            foreach (var item in invList)
            {
                Debug.Log(item.Method);
            }
        }
    }

    [ContextMenu("Get int atr test")]
    public void TestAddInt()
    {
        var attr = GetAttribute<IntAttributeData>(attrToGetTest);
        Debug.Log(attr);
        if (attr != null)
        {
            Debug.Log(attr.value);
        }
    }

    [ContextMenu("Get float test")]
    public void TestAddFloat()
    {
        var attr = GetAttribute<FloatAttributeData>(attrToGetTest);
        Debug.Log(attr);
        if (attr != null)
        {
            Debug.Log(attr.value);
        }
    }

    [ContextMenu("has categ test")]
    public void TestHasCateg()
    {
        Debug.Log(HasCategory(categToGetTest));
    }

    public IEnumerable<ItemCategory> GetCategoryAncestors()
    {
        return categoryParent.ancestorCategories;
    }
    [ContextMenu("Debug Test")]
    public void DebugTest()
    {
        
    }

    #endregion

}