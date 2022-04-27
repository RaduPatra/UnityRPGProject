using UnityEngine;

[CreateAssetMenu(fileName = "New EquipAction Attribute", menuName = "Attributes/EquipActionAttribute", order = 1)]
public class EquipActionAttributeSO : AttributeSO<EquipItemAction>
{
    public override AttributeDataBase CreateData()
    {
        var val = new EquipActionData()
        {
            value = attributeData.value
        };
        return val;
    }
}