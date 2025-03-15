using UnityEditor;
using UnityEngine;

namespace App.Scripts.Save
{
    [CustomPropertyDrawer(typeof(SaveNameAttribute))]
    public class SaveNameAttributeEditor : PropertyDrawer
    {
        private static readonly int saveMax = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [SaveName] with a String.");
                EditorGUI.EndProperty();
                return;
            }

            // Set All Saves
            string[] saveName = new string[saveMax + 1];
            saveName[0] = "Settings";

            for (int i = 1; i <= saveMax; i++)
            {
                saveName[i] = $"Save {i}";
            }

            // Find the Index of the Current Save
            int selectedIndex = Mathf.Max(0, System.Array.IndexOf(saveName, property.stringValue));

            property.stringValue = saveName[EditorGUI.Popup(position, label.text, selectedIndex, saveName)];
            EditorGUI.EndProperty();
        }
    }
}