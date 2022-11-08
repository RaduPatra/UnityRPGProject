using System;
using UnityEngine;

// [Serializable]
public class IntAttributeData : AttributeData<int>
{
}


public class TestRefAttributeData : AttributeData<TestRef>
{
}


[Serializable]
public class TestRef
{
    [SerializeReference]
    public TestBase testBase;
}