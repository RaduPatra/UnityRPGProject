using System.Collections.Generic;
using System.Reflection;
using Polytope;
using UnityEngine;
using UnityEngine.UIElements;


public enum CategType
{
    Item,

    Equipable,

    Armor,
    Helm,
    Chest,

    Weapon,
    OneHandSword,
    TwoHandSword,
    Wand,


    Consumable
}


public class ItemStackTest
{
    public ItemWithAttributesTest item;
    public int quantity;

    public ItemStackTest()
    {
    }
}


public class ItemWithCategTest : ScriptableObject
{
    public CategoryTest itemCateg;
}

public class ItemWithAttributesTest : ScriptableObject
{
    public List<AttributeBaseSO> categAttr;

}


public abstract class CategoryTest : ScriptableObject
{
    // public string name;
    public List<CategoryTest> categParents;
    public List<AttributeBaseSO> categAttr; //has to be unique, cant be contained in parents

    public CategoryAction clickAction;
    public CategoryAction rightClickAction;
    public CategoryAction shiftClickAction;
}

public abstract class CategoryAction
{
    public abstract void Execute(GameObject go);
    
}

public class AttackAction : CategoryAction
{
    public override void Execute(GameObject go)
    {
    }
}

public class ShootAction : CategoryAction
{
    public override void Execute(GameObject go)
    {
    }
}

public class UseEffectsAction : CategoryAction
{
    //effects[]
    public override void Execute(GameObject go)
    {
        //foreach effect in effects -> effect.use(go)
    }
}

/*
[Serializable]
public class AttrTest<T> : AttributeBaseTest //so
{
    // public string name;
    public AttributeData<T> attribute;
    // public List<CategType> tags;
    
    public Type GetTypeTest()
    {
        return typeof(T);
    }
    
    public AttributeData<T> GetData()
    {
        return attribute;
    }

    public void GetAtrd()
    {
        throw new NotImplementedException();
    }

    public override void GetAtrd(out AttributeBaseTest d)
    {
        throw new NotImplementedException();
    }
}
*/

/*[Serializable]
public class FloatAttr : AttrTest<float>
{

    public FloatAttr()
    {
        var x = GetData();
        var a = x.GetType();
    }
}
[Serializable]
public class IntAttr : AttrTest<int>
{
}
/*
[CreateAssetMenu(fileName = "New Item", menuName = "ItemAttrTest", order = 1)]

public class TestAttrSo : ScriptableObject
{
    public AttrTest<float> test;
}
*/

