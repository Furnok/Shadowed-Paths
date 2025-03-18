using System.Linq;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.Utils
{
    [CustomPropertyDrawer(typeof(SceneNameAttribute))]
    public class TagsNameAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty sceneGUIDProp = property.FindPropertyRelative("sceneGUID");
            SerializedProperty sceneNameProp = property.FindPropertyRelative("sceneName");

            var buildScenes = EditorBuildSettings.scenes;
            if (buildScenes.Length == 0)
            {
                EditorGUI.LabelField(position, label.text, "No scenes in Build Settings.");
                EditorGUI.EndProperty();
                return;
            }

            // Get List of Scene Paths and Names
            var scenePaths = buildScenes.Select(scene => scene.path).ToArray();
            var sceneNames = scenePaths.Select(System.IO.Path.GetFileNameWithoutExtension).ToArray();

            string storedScenePath = AssetDatabase.GUIDToAssetPath(sceneGUIDProp.stringValue);
            int selectedIndex = System.Array.IndexOf(scenePaths, storedScenePath);

            if (selectedIndex < 0)
            {
                selectedIndex = sceneNames.ToList().FindIndex(name => name == sceneNameProp.stringValue);
            }

            selectedIndex = Mathf.Max(0, selectedIndex);

            // Find Scene Name
            int newIndex = EditorGUI.Popup(position, label.text, selectedIndex, sceneNames);
            if (newIndex != selectedIndex)
            {
                string selectedPath = scenePaths[newIndex];
                sceneGUIDProp.stringValue = AssetDatabase.AssetPathToGUID(selectedPath);
                sceneNameProp.stringValue = sceneNames[newIndex];
            }

            // First Scene if Not Set
            if (string.IsNullOrEmpty(sceneGUIDProp.stringValue) && string.IsNullOrEmpty(sceneNameProp.stringValue))
            {
                string defaultPath = buildScenes[0].path;
                sceneGUIDProp.stringValue = AssetDatabase.AssetPathToGUID(defaultPath);
                sceneNameProp.stringValue = System.IO.Path.GetFileNameWithoutExtension(defaultPath);
            }

            EditorGUI.EndProperty();
        }
    }
}