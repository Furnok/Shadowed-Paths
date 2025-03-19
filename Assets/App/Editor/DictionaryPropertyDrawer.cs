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

            if (keysProperty == null || valuesProperty == null)
            {
                EditorGUI.LabelField(position, "Dictionary Serialization Error.");
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

            EditorGUI.indentLevel++;
            float yOffset = position.y + EditorGUIUtility.singleLineHeight + 2f;
            float halfWidth = position.width * 0.5f;

            for (int i = 0; i < keysProperty.arraySize; i++)
            {
                Rect keyRect = new(position.x, yOffset, halfWidth - 10, EditorGUIUtility.singleLineHeight);
                Rect valueRect = new(position.x + halfWidth, yOffset, halfWidth - 10, EditorGUIUtility.singleLineHeight);
                Rect removeButtonRect = new(position.x + position.width - 20, yOffset, 20, EditorGUIUtility.singleLineHeight);

                EditorGUI.PropertyField(keyRect, keysProperty.GetArrayElementAtIndex(i), GUIContent.none);
                EditorGUI.PropertyField(valueRect, valuesProperty.GetArrayElementAtIndex(i), GUIContent.none);

                yOffset += EditorGUIUtility.singleLineHeight + 2f;
            }

            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return EditorGUIUtility.singleLineHeight;

            SerializedProperty keysProperty = property.FindPropertyRelative("keys");
            return (keysProperty.arraySize + 1) * (EditorGUIUtility.singleLineHeight + 1);
        }
    }
}
