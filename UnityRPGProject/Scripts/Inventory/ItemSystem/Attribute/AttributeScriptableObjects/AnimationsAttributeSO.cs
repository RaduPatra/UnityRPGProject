
using UnityEngine;

[CreateAssetMenu(fileName = "New Animations Attribute", menuName = "Attributes/AnimationsAttribute", order = 1)]
public class AnimationsAttributeSO : AttributeSO<ItemAnimations>
{
    public override AttributeDataBase CreateData()
    {
        var val = new AnimationsData()
        {
            value = new ItemAnimations()
        };
        return val;
    }
}