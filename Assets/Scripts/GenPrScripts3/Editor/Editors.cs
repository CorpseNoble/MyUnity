using UnityEngine;
using UnityEditor;
using System;
namespace GenPr3
{
    [CustomPropertyDrawer(typeof(PropertyButton))]
    public class EditorButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            var target = base.fieldInfo.GetValue(property.serializedObject.targetObject) as PropertyButton;
            if (GUI.Button(position, label.text))
            {
                target.Button_Cklick();
            }

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}