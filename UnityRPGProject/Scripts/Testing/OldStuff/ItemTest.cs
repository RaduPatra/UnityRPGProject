using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public enum ItemType
{
    //eq
    Sword,
    Helm,
    Chest,
    Pants,
    Boots,
    Shield,
    Consumable,
    Any
}
[CreateAssetMenu(fileName = "New Item", menuName = "Items", order = 1)]

public class ItemTest : ScriptableObject
{
    [Header("Common")]
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public GameObject dropItemPrefab;
    public bool isStackable;
    public int maxStack = 1;
    
    [Header("Consumable")]
    public ItemEffectApplierTest itemEffectApplier;

    [Header("Equipable")]
    public GameObject equipItemPrefab;
    public ParticleSystem impactParticle;
    // public ItemTypeOld itemTypeOld; //eq type?
    public StatModifier[] statModifiers;

    //actions?

    public bool IsEquipment()
    {
        return IsArmor() || IsUsableEquipment();
    }

    public bool IsArmor()
    {
        return itemType == ItemType.Helm || itemType == ItemType.Chest || itemType == ItemType.Pants ||
               itemType == ItemType.Boots;
    }

    public bool IsUsableEquipment()
    {
        return itemType == ItemType.Sword || itemType == ItemType.Shield;
    }

    public bool IsWeapon()
    {
        return itemType == ItemType.Sword;//left, right, twohand weapon?
    }
}

[Serializable]
public class ItemEffectApplierTest
{
    public EffectBase[] effects;
    
    public void Use(GameObject target)
    {
        foreach (var effect in effects)
        {
            effect.UseEffect(target);
        }
    }
}




