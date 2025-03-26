using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Utils
{
    [CustomPropertyDrawer(typeof(S_SceneReference))]
    public class S_SceneNameAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get Property
            SerializedProperty sceneGUIDProp = property.FindPropertyRelative("sceneGUID");
            SerializedProperty sceneNameProp = property.FindPropertyRelative("sceneName");

            if (sceneGUIDProp == null || sceneNameProp == null)
            {
                EditorGUI.LabelField(position, label.text, "Invalid SceneReference.");
                EditorGUI.EndProperty();
                return;
            }

            var buildScenes = EditorBuildSettings.scenes;
            if (buildScenes.Length == 0)
            {
                EditorGUI.LabelField(position, label.text, "No Scenes in Build Settings.");
                EditorGUI.EndProperty();
                return;
            }

            // Get List of Scene Paths and Names
            var scenePaths = buildScenes.Select(scene => scene.path).ToArray();
            var sceneNames = scenePaths.Select(Path.GetFileNameWithoutExtension).ToArray();
            var sceneGUIDs = scenePaths.Select(AssetDatabase.AssetPathToGUID).ToArray();

            // Find Index based on GUID
            int selectedIndex = Array.IndexOf(sceneGUIDs, sceneGUIDProp.stringValue);
            if (selectedIndex < 0)
            {
                selectedIndex = 0;
                sceneGUIDProp.stringValue = sceneGUIDs[selectedIndex];
                sceneNameProp.stringValue = sceneNames[selectedIndex];
            }

            // Display Dropdown
            int newIndex = EditorGUI.Popup(position, label.text, selectedIndex, sceneNames);
            if (newIndex != selectedIndex)
            {
                sceneGUIDProp.stringValue = sceneGUIDs[newIndex];
                sceneNameProp.stringValue = sceneNames[newIndex];
            }

            EditorGUI.EndProperty();
        }
    }
}