using UnityEngine;

[CreateAssetMenu(fileName = "New Float Attribute", menuName = "Attributes/FloatAttribute", order = 1)]
public class FloatAttributeSO : AttributeSO<float>
{
    public override AttributeDataBase CreateData()
    {
        var val = new FloatAttributeData
        {
            value = attributeData.value
        };
        return val;
    }
}