using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;



[CustomPropertyDrawer(typeof(AnimVariable))]
public class AnimVariableEditor : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(rect, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        rect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);
        rect.width /= 3;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("nameVariable"), GUIContent.none);
        rect.x += rect.width;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("type"), GUIContent.none);
        rect.x += rect.width;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("audio"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}


[CustomPropertyDrawer(typeof(Interect))]
public class InterectEditor : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        var target = base.fieldInfo.GetValue(property.serializedObject.targetObject) as Interect;

        // EditorGUI.BeginProperty(rect, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        //rect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), label);

        if (GUI.Button(rect, label))
        {
            target.action.Invoke();
        }


        //EditorGUI.PropertyField(rect, property.FindPropertyRelative("button"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        // EditorGUI.EndProperty();
    }
}



[CustomPropertyDrawer(typeof(SelectOnList))]
public class SelectOnListDrower : PropertyDrawer
{
    SelectOnList target;
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        if (target == null)
            target = fieldInfo.GetValue(property.serializedObject.targetObject) as SelectOnList;

        target.index = EditorGUI.Popup(rect, label.text, target.index, target.list.Select(c => c.name + "." + c.GetType().Name).ToArray());

        property.serializedObject.ApplyModifiedProperties();

    }
}


[CustomPropertyDrawer(typeof(Block))]
public class BlockEditor : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(rect, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        var w = rect.width - 20;
        rect.width = 50;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("type"), GUIContent.none);
        rect.x = rect.width + 20;
        rect.width = w;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("value"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}

[CustomPropertyDrawer(typeof(Block2))]
public class Block2Editor : PropertyDrawer
{
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(rect, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        var w = rect.width - 20;
        rect.width = 10;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("value"), GUIContent.none);
        rect.x = rect.width + 20;
        rect.width += 40;
        EditorGUI.LabelField(rect, "Count");
        rect.x = rect.width + 20;
        rect.width = w;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("count"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}

[CustomEditor(typeof(GenParamTypeVar))]
public class GenParamTypeVarDrower : PropertyDrawer
{
    GenParamTypeVar target;
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        if (target == null)
            target = fieldInfo.GetValue(property.serializedObject.targetObject) as GenParamTypeVar;

        EditorGUI.BeginProperty(rect, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        var w = rect.width - 20;
        rect.width = 10;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("value"), GUIContent.none);
        rect.x = rect.width + 20;
        rect.width += 40;
        EditorGUI.LabelField(rect, "Count");
        rect.x = rect.width + 20;
        rect.width = w;
        EditorGUI.PropertyField(rect, property.FindPropertyRelative("count"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();

        property.serializedObject.ApplyModifiedProperties();
    }
}

