using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New TestClass Attribute", menuName = "Attributes/TestClass", order = 1)]
public class TestCustomClassSO : AttributeSO<MyTestClass>
{
    public override AttributeDataBase CreateData()
    {
        var val = new MyTestClassData
        {
            value = attributeData.value
        };
        return val;
    }
}

public class MyTestClassData : AttributeData<MyTestClass>
{
}

[Serializable]
public class MyTestClass
{
    public int intVal;
    public float floatVal;
    public GameObject goVal;
    public MyTestClass2 test2;
}

[Serializable]
public class MyTestClass2
{
    public int intVal2;
}