using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace App.Scripts.Utils
{
    [CustomPropertyDrawer(typeof(TagNameAttribute))]
    public class TagNameAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [TagName] with a string.");
                EditorGUI.EndProperty();
                return;
            }

            // Get All Unity Tags
            string[] allTags = InternalEditorUtility.tags;

            // Find the Index of the Current Tag
            int selectedIndex = Mathf.Max(0, System.Array.IndexOf(allTags, property.stringValue));

            property.stringValue = allTags[EditorGUI.Popup(position, label.text, selectedIndex, allTags)];
            EditorGUI.EndProperty();
        }
    }
}