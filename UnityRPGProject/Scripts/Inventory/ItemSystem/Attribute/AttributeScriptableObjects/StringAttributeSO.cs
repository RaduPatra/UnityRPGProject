
using UnityEngine;

[CreateAssetMenu(fileName = "New String Attribute", menuName = "Attributes/StringAttribute", order = 1)]
public class StringAttributeSO : AttributeSO<string>
{
    public override AttributeDataBase CreateData()
    {
        var val = new StringAttributeData()
        {
            value = attributeData.value
        };
        return val;
    }
}