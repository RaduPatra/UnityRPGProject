using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CategorySearchProvider : ScriptableObject, ISearchWindowProvider
{
    private List<ItemCategory> categories;
    private Action<ItemCategory> onSelectCallback;

    public void Init(List<ItemCategory> categories, Action<ItemCategory> onSelectCallback)
    {
        this.categories = categories;
        this.onSelectCallback = onSelectCallback;
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchlist = new List<SearchTreeEntry>();
        searchlist.Add(new SearchTreeGroupEntry(new GUIContent("List"), 0));

        foreach (var category in categories)
        {
            var entry = new SearchTreeEntry(new GUIContent(category.name,
                EditorGUIUtility.ObjectContent(category, category.GetType()).image))
            {
                level = 1,
                userData = category
            };

            searchlist.Add(entry);
        }

        return searchlist;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        onSelectCallback?.Invoke((ItemCategory)SearchTreeEntry.userData);
        return true;
    }
}