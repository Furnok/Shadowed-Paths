using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(S_SaveNameAttribute))]
public class S_SaveNameAttributeEditor : PropertyDrawer
{
    private static List<string> SaveNames
    {
        get
        {
            if (!SaveConfig.saveActived)
            {
                return new();
            }

            List<string> saveNames = new();

            if (SaveConfig.HaveSettings)
            {
                saveNames.Add($"Settings");
            }

            if (SaveConfig.HaveAchievements)
            {
                saveNames.Add($"Achievements");
            }

            if (SaveConfig.SaveMax > 0)
            {
                for (int i = 0; i < SaveConfig.SaveMax; i++)
                {
                    saveNames.Add($"Save_{i + 1}");
                }
            }

            return saveNames;
        }
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

        if ((SaveConfig.SaveMax <= 0 && !SaveConfig.HaveSettings) || !SaveConfig.saveActived)
        {
            EditorGUI.LabelField(position, label.text, "Saves Desactivated");
            EditorGUI.EndProperty();
            return;
        }

        if (SaveConfig.SaveMax > 0)
        {
            // Find the Index of the Current Save
            var saveNamesArray = SaveNames.ToArray();
            int selectedIndex = Mathf.Max(0, Array.IndexOf(saveNamesArray, property.stringValue));
            property.stringValue = saveNamesArray[EditorGUI.Popup(position, label.text, selectedIndex, saveNamesArray)];
        }

        EditorGUI.EndProperty();
    }
}