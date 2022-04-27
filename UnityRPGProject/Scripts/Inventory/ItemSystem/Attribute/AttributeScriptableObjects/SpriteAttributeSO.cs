using UnityEngine;

[CreateAssetMenu(fileName = "New Sprite Attribute", menuName = "Attributes/SpriteAttribute", order = 1)]
public class SpriteAttributeSO : AttributeSO<Sprite>
{
    public override AttributeDataBase CreateData()
    {
        var val = new SpriteAttributeData()
        {
            value = attributeData.value
        };
        return val;
    }
}