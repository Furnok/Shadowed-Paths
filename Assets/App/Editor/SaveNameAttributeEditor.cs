using System;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Save
{
    [CustomPropertyDrawer(typeof(SaveNameAttribute))]
    public class SaveNameAttributeEditor : PropertyDrawer
    {
        private static readonly int saveMax = 5;
        private static readonly string[] saveNames = GenerateSaveNames();

        private static string[] GenerateSaveNames()
        {
            string[] names = new string[saveMax + 1];
            names[0] = "Settings";

            for (int i = 1; i <= saveMax; i++)
            {
                names[i] = $"Save {i}";
            }
                
            return names;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [SaveName] with a String.");
            }
            else
            {
                // Find the Index of the Current Save
                int selectedIndex = Mathf.Max(0, Array.IndexOf(saveNames, property.stringValue));
                property.stringValue = saveNames[EditorGUI.Popup(position, label.text, selectedIndex, saveNames)];
            }

            EditorGUI.EndProperty();
        }
    }
}