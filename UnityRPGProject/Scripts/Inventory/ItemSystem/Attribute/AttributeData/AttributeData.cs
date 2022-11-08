using System;
using Sirenix.OdinInspector;

[Serializable]
public class AttributeData<T> : AttributeDataBase
{
    // [HideReferenceObjectPicker]
    public T value;

    
}