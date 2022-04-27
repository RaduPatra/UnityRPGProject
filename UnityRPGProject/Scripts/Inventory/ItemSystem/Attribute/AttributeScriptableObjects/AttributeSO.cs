using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class AttributeSO<T> : AttributeBaseSO //so
{
    public AttributeData<T> attributeData;

    /*public AttributeDataBase CreateData()
    {
        return new AttributeData<T>();
    }*/

    /*public AttributeData2<T> GetData()
    {
        return attributeData;
    }*/
}