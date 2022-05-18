
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StatModifierList Attribute", menuName = "Attributes/StatModifierListAttribute", order = 1)]
public class StatModifierListSO : AttributeSO<List<StatModifier>>
{
    public override AttributeDataBase CreateData()
    {
        var val = new StatModifierListData
        {
            value =  new List<StatModifier>(/*attributeData.value*/)
        };
        return val;
    }
}