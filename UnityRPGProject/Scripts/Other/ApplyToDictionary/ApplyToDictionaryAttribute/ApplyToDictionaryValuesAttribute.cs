using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class)]
public class ApplyToDictionaryValuesAttribute : Attribute { }


[ApplyToDictionaryValues]
[HideReferenceObjectPicker]
public class DictionaryHideReferenceObjectPickerAttribute : Attribute { }