using UnityEngine;

[CreateAssetMenu(fileName = "New Gameplay Action Attribute", menuName = "Attributes/GameplayActionAttribute", order = 1)]
public class GameplayItemActionAttributeSO : AttributeSO<ItemGameplayActions>
{
    public override AttributeDataBase CreateData()
    {
        var val = new GameplayItemActionsData()
        {
            value = attributeData.value
        };
        return val;
    }
}
