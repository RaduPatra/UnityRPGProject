using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "Testing SO", menuName = "Testing SO", order = 1)]
public class TestingSO : SerializedScriptableObject
{
    public SerializableDictionary<ItemCategory, InventorySlot> equipmentArmorSlots =
        new SerializableDictionary<ItemCategory, InventorySlot>();

    public Dictionary<ItemCategory, InventorySlot> equipmentArmorSlots2 = new Dictionary<ItemCategory, InventorySlot>();

    [SerializeReference] private List<TestBase> testRef;


    public LazyValue<int> testLazy;
    public int lazyResult = -2;

    [ContextMenu("Test Lazy")]
    public void TestLazy()
    {
        lazyResult = testLazy.value;
    }

    [ContextMenu("Init Lazy")]
    public void InitLazy()
    {
        testLazy = new LazyValue<int>(Init2);
        var a = testLazy.value;
        Debug.Log(a);
        testLazy = new LazyValue<int>(Init);
    }

    public int test123 = 2;

    public int cnt = 0;

    public int Init()
    {
        var x = testLazy.ValueNoInit;
        Debug.Log(x);
        return 123;
    }

    public int Init2()
    {
        return 555;
    }
}

[Serializable]
public class TestBase
{
    public int testIntField1;
}

[Serializable]
public class TestA : TestBase
{
    public float testFloatField1;
    public float testFloatField2;
}

[Serializable]
public class TestB : TestBase
{
    public float testFloatField3;
    public string testStringField;
}