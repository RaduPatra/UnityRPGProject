/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

public class TestA
{
}

public class TestB : TestA
{
}

public class TestGeneric<T>
{
}

[CustomPropertyDrawer(typeof(AttributeInfo), true)]
public class ItemPropertyDrawer : PropertyDrawer
{
    /*public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        // EditorGUI.ObjectField(position, property.FindPropertyRelative("test"), GUIContent.none);
        EditorGUI.PropertyField(position, property.FindPropertyRelative("test"));
        EditorGUI.EndProperty();

    }#1#


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect btnPos = new Rect(position.x, position.y + 100f, position.width, 16f);
        EditorGUI.BeginProperty(position, label, property);

        ;
        EditorGUI.PropertyField(position, property, label, true);
        /*if (GUI.Button(btnPos, "Refresh"))
        {#1#
        Type parentType = property.serializedObject.targetObject.GetType();
        // property.serializedObject.targetObject
        FieldInfo fi = parentType.GetField(property.propertyPath);
        Debug.Log(fi);
        var attributeMemberInfo =
            fi.GetValue(property.serializedObject.targetObject).GetType().GetMember(nameof(AttributeInfo.attribute))
                .First();

        if (attributeMemberInfo.MemberType == MemberTypes.Field)
        {
            var atrProp = property.FindPropertyRelative(nameof(AttributeInfo.attribute)).objectReferenceValue;
            var valueMemberInfo = atrProp.GetType().GetMember(nameof(AttributeData<Type>.value)).First();
            var valueFieldInfo = (FieldInfo)valueMemberInfo;
            var valueType = valueFieldInfo.FieldType;

            SerializedProperty attributeDataProperty = property.FindPropertyRelative(nameof(AttributeInfo.data));
            Debug.Log("--3-- " + attributeDataProperty);
            var instance = Activator.CreateInstance(valueType);
            attributeDataProperty.managedReferenceValue = instance;
            EditorUtility.SetDirty(property.serializedObject.targetObject);
            // }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }
}


/*public static class TypeFactory
{
    public static AttrInfoBase2 CreateType(Type type)
    {
        if (type == typeof(int))
            return new IntAttributeData();
        if (type == typeof(float))
            return new FloatAttributeData();
        return default;
    }
}#1#*/