using UnityEngine;

[CreateAssetMenu(fileName = "New Int Attribute", menuName = "Attributes/IntAttribute", order = 1)]
public class IntAttributeSO : AttributeSO<int>
{
    public override AttributeDataBase CreateData()
    {
        var val = new IntAttributeData()
        {
            value = attributeData.value
        };
        return val;
    }
}