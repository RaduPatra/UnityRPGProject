using System;
using UnityEngine;

[Serializable]
public abstract class AttributeBaseSO : ScriptableObject
{
    public string name;
    
    public abstract AttributeDataBase CreateData();

    
    private void OnValidate()
    {
        // Debug.Log("ATTRIBUTE On Validate" + name);
    }

}