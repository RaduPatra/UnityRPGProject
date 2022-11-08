using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class EquipableItem : ItemBase
{
    public GameObject equipItemPrefab;
    public ParticleSystem impactParticle;
    public ItemTypeOld itemTypeOld;
    public StatModifier[] statModifiers;

    /*public override void UseTest(GameObject user)
    {
    }*/
}
