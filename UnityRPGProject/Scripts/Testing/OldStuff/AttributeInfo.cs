using System;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public class AttributeInfo
{
    [Sirenix.OdinInspector.ReadOnly]
    public AttributeBaseSO attribute;

    [HideReferenceObjectPicker] [SerializeReference]
    public AttributeDataBase data;

    public AttributeInfo(AttributeBaseSO attribute)
    {
        this.attribute = attribute;
        data = attribute.CreateData();
    }
}