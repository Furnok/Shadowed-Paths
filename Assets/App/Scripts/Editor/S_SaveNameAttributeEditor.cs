using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(S_SaveNameAttribute))]
public class S_SaveNameAttributeEditor : PropertyDrawer
{
    private readonly bool HaveSettings = true;
    private readonly bool HaveAchievements = true;
    private readonly bool HaveSaveAuto = true;
    private readonly bool HaveSaves = true;
    private readonly int SaveMax = 3;

    private List<string> SaveNames
    {
        get
        {
            List<string> saveNames = new();

            if (HaveSettings)
            {
                saveNames.Add($"Settings");
            }

            if (HaveAchievements)
            {
                saveNames.Add($"Achievements");
            }

            if (HaveSaveAuto)
            {
                saveNames.Add($"Save_Auto");
            }

            if (HaveSaves && SaveMax > 0)
            {
                for (int i = 0; i < SaveMax; i++)
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

        if (SaveNames.Count < 1)
        {
            EditorGUI.LabelField(position, label.text, "Saves Desactivated");
            EditorGUI.EndProperty();
            return;
        }

        // Find the Index of the Current Save
        var saveNamesArray = SaveNames.ToArray();
        int selectedIndex = Mathf.Max(0, Array.IndexOf(saveNamesArray, property.stringValue));
        property.stringValue = saveNamesArray[EditorGUI.Popup(position, label.text, selectedIndex, saveNamesArray)];

        EditorGUI.EndProperty();
    }
}