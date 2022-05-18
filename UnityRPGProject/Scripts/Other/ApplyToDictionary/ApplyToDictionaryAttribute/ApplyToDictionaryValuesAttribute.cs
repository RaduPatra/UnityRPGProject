using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

[Conditional("UNITY_EDITOR")]
[AttributeUsage(AttributeTargets.Class)]
public class ApplyToDictionaryValuesAttribute : Attribute { }


[ApplyToDictionaryValues]
[HideReferenceObjectPicker]
public class DictionaryHideReferenceObjectPickerAttribute : Attribute { }


