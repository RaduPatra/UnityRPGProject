using UnityEngine;

[CreateAssetMenu(fileName = "New StatModifier Attribute", menuName = "Attributes/StatModifierAttribute", order = 1)]
public class StatModifierSO : AttributeSO<StatModifier>
{
    public override AttributeDataBase CreateData()
    {
        var val = new StatModifierAttributeData()
        {
            value = attributeData.value
        };
        return val;
    }
}