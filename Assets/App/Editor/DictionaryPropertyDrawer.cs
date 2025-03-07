using UnityEditor;
using UnityEngine;

namespace App.Scripts.Utils
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>), true)]
    public class DictionaryPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Find the Serialized Lists inside SerializableDictionary
            SerializedProperty keysProperty = property.FindPropertyRelative("keys");
            SerializedProperty valuesProperty = property.FindPropertyRelative("values");

            if (keysProperty == null 
                || valuesProperty == null)
            {
                EditorGUI.LabelField(position, "Dictionary serialization error.");
                EditorGUI.EndProperty();
                return;
            }

            // Foldout for Collapsing/Expanding
            Rect foldoutRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

            if (!property.isExpanded)
            {
                EditorGUI.EndProperty();
                return;
            }

            float yOffset = foldoutRect.y + EditorGUIUtility.singleLineHeight;
            EditorGUI.indentLevel++;
            float customSpacing = 2f;

            // Iterate through Dictionary Items
            for (int i = 0; i < keysProperty.arraySize; i++)
            {
                Rect itemRect = new(position.x, yOffset, position.width, EditorGUIUtility.singleLineHeight);
                float halfWidth = itemRect.width / 2;

                EditorGUI.PropertyField(new Rect(itemRect.x, itemRect.y, halfWidth, itemRect.height), keysProperty.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUI.PropertyField(new Rect(itemRect.x + halfWidth, itemRect.y, halfWidth, itemRect.height), valuesProperty.GetArrayElementAtIndex(i), GUIContent.none);

                yOffset += EditorGUIUtility.singleLineHeight + customSpacing;
            }

            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float customSpacing = 2f;
            SerializedProperty keysProperty = property.FindPropertyRelative("keys");
            return (keysProperty.arraySize + 1) * EditorGUIUtility.singleLineHeight + customSpacing * keysProperty.arraySize;
        }
    }
}
