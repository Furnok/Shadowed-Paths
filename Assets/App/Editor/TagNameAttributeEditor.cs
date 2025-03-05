using UnityEditor;
using UnityEngine;

namespace App.Scripts.Utils
{
    [CustomPropertyDrawer(typeof(TagNameAttribute))]
    public class TagNameAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [TagName] with a string.");
                return;
            }

            // Get All Unity Tags
            string[] allTags = UnityEditorInternal.InternalEditorUtility.tags;

            // Get Currently Stored Tag
            string currentTag = property.stringValue;

            // Find the Index of the Current Tag
            int selectedIndex = System.Array.IndexOf(allTags, currentTag);

            if (selectedIndex == -1)
            {
                selectedIndex = 0; // Default to First Tag if not found
            }

            // Show Tag Dropdown
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, allTags);

            // Update Property if changed
            if (selectedIndex >= 0 
                && selectedIndex < allTags.Length)
            {
                property.stringValue = allTags[selectedIndex];
            }
        }
    }
}