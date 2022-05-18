using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;


// [CustomPropertyDrawer(typeof(ItemCategory), true)]

[CustomEditor(typeof(ItemCategory))]
public class CategoryEditor : Editor
{
    private CategorySearchProvider searchProvider;
    private ReorderableList reorderableList;
    private SerializedProperty categoriesProp;
    private ItemCategory myTarget;


    private void OnEnable()
    {
        Debug.Log("----on enable called");
        myTarget = (ItemCategory)target;
        categoriesProp = serializedObject.FindProperty(nameof(ItemCategory.categoryParents));
        reorderableList = new ReorderableList(serializedObject, categoriesProp, true, true, true, true)
        {
            drawElementCallback = DrawElement,
            drawHeaderCallback = DrawHeader,
            onAddCallback = OnAdd,
            onRemoveCallback = OnRemove
        };
    }

    private void OnRemove(ReorderableList list)
    {
        // myTarget.categories.RemoveAt(list.index);
        serializedObject.Update();
        var prop = reorderableList.serializedProperty;
        prop.GetArrayElementAtIndex(list.index).objectReferenceValue = null;
        prop.DeleteArrayElementAtIndex(list.index);
        serializedObject.ApplyModifiedProperties();
    }

    private void OnAdd(ReorderableList list)
    {
        Debug.Log("----on add test called");
        var categoryAssets = GetCategoryAssets();
        var categoriesToShow = new List<ItemCategory>();

        /*filter assets: dont show - categories that are already contained in this category
                                   - categories that contain this category, in order to prevent cycles*/
        foreach (var categoryAsset in categoryAssets)
        {
            if (!myTarget.ContainsCategory(categoryAsset) && !categoryAsset.ContainsCategory(myTarget))
            {
                categoriesToShow.Add(categoryAsset);
            }
        }

        searchProvider.Init(categoriesToShow, AddCategory);
        SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)),
            searchProvider);
    }

    private void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "Categories");
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        rect.y += 2;

        var element = categoriesProp.GetArrayElementAtIndex(index);

        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Element " + index);
        EditorGUI.PropertyField(
            new Rect(rect.x + 100, rect.y, EditorGUIUtility.currentViewWidth - 150, EditorGUIUtility.singleLineHeight),
            element, GUIContent.none);

        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var prop = serializedObject.GetIterator();
        if (prop.NextVisible(true))
        {
            do
            {
                // Draw my list manually.
                if (prop.name == nameof(ItemCategory.categoryParents))
                {
                    serializedObject.Update();
                    reorderableList.DoLayoutList();
                    serializedObject.ApplyModifiedProperties();
                }
                // Draw default property field.
                else
                {
                    serializedObject.Update();
                    EditorGUILayout.PropertyField(prop, true);
                    serializedObject.ApplyModifiedProperties();
                }
            } while (prop.NextVisible(false));
        }

        serializedObject.ApplyModifiedProperties();
    }

    private List<ItemCategory> GetCategoryAssets()
    {
        var categoryAssets = new List<ItemCategory>();
        var guids = AssetDatabase.FindAssets("t:ItemCategory");
        searchProvider = CreateInstance<CategorySearchProvider>();
        foreach (var guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<ItemCategory>(assetPath);
            if (asset != null)
            {
                categoryAssets.Add(asset);
            }
        }

        return categoryAssets;
    }

    private void AddCategory(ItemCategory categoryToAdd)
    {
        var prop = reorderableList.serializedProperty;
        prop.arraySize++;
        var newElement = prop.GetArrayElementAtIndex(prop.arraySize - 1);
        newElement.objectReferenceValue = categoryToAdd;
        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
    }
}


// myTarget.categories.Add(categoryToAdd);