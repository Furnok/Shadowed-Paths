using System;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Save
{
    [CustomPropertyDrawer(typeof(S_SaveNameAttribute))]
    public class S_SaveNameAttributeEditor : PropertyDrawer
    {
        private static readonly string[] saveNames = GenerateSaveNames();

        private static string[] GenerateSaveNames()
        {
            int offset = SaveConfig.HaveSettings ? 1 : 0;
            string[] names = new string[SaveConfig.SaveMax + offset];

            if (SaveConfig.saveActived)
            {
                if (SaveConfig.HaveSettings)
                {
                    names[0] = "Settings";
                }

                for (int i = 0; i < SaveConfig.SaveMax; i++)
                {
                    names[i + offset] = $"Save_{i + 1}";
                }
            }

            return names;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [SaveName] with a String.");
                EditorGUI.EndProperty();
                return;
            }

            if ((SaveConfig.SaveMax == 0 && !SaveConfig.HaveSettings) || !SaveConfig.saveActived)
            {
                EditorGUI.LabelField(position, label.text, "Saves Desactivated");
                EditorGUI.EndProperty();
                return;
            }

            // Find the Index of the Current Save
            int selectedIndex = Mathf.Max(0, Array.IndexOf(saveNames, property.stringValue));
            property.stringValue = saveNames[EditorGUI.Popup(position, label.text, selectedIndex, saveNames)];

            EditorGUI.EndProperty();
        }
    }
}