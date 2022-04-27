

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EffectList Attribute", menuName = "Attributes/EffectListAttribute", order = 1)]
public class EffectListSO : AttributeSO<List<EffectBase>>
{
    public override AttributeDataBase CreateData()
    {
        var val = new EffectListAttributeData
        {
            value =  new List<EffectBase>(attributeData.value)
        };
        return val;
    }
}