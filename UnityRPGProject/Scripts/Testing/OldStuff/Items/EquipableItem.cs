using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Equipable Item", menuName = "Equipables/Equipable", order = 1)]
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