using UnityEngine;

[CreateAssetMenu(fileName = "New GameObject Attribute", menuName = "Attributes/GameObjectAttribute", order = 1)]
public class GameObjectAttributeSO : AttributeSO<GameObject>
{
    public override AttributeDataBase CreateData()
    {
        var val = new GameObjectData()
        {
            value = attributeData.value
        };
        return val;
    }
}