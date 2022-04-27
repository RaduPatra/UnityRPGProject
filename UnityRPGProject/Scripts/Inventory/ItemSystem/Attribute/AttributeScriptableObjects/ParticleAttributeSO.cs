using UnityEngine;

[CreateAssetMenu(fileName = "New Particle Attribute", menuName = "Attributes/ParticleAttribute", order = 1)]
public class ParticleAttributeSO : AttributeSO<ParticleSystem>
{
    public override AttributeDataBase CreateData()
    {
        var val = new ParticleAttributeData()
        {
            value = attributeData.value
        };
        return val;
    }
}