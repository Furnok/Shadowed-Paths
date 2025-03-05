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
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [SceneName] with a string.");
                return;
            }

            var buildScenes = EditorBuildSettings.scenes;

            if (buildScenes.Length <= 0)
            {
                EditorGUI.LabelField(position, label.text, "No scenes in Build Settings.");
                return;
            }

            // Get List of Scene Paths and Names
            string[] scenePaths = buildScenes.Select(scene => scene.path).ToArray();
            string[] sceneNames = scenePaths.Select(path => System.IO.Path.GetFileNameWithoutExtension(path)).ToArray();

            // Get the Stored Scene GUI
            string storedSceneGUID = property.stringValue;

            // Convert GUID to scene path
            string storedScenePath = AssetDatabase.GUIDToAssetPath(storedSceneGUID);
            string storedSceneName = string.IsNullOrEmpty(storedScenePath) ? "" : System.IO.Path.GetFileNameWithoutExtension(storedScenePath);

            // Try to find the scene by path
            int selectedIndex = System.Array.IndexOf(scenePaths, storedScenePath);

            // If path is not found, fallback to name search
            if (selectedIndex == -1 && !string.IsNullOrEmpty(storedSceneName))
            {
                for (int i = 0; i < sceneNames.Length; i++)
                {
                    if (sceneNames[i] == storedSceneName)
                    {
                        selectedIndex = i;
                        break;
                    }
                }
            }

            // Default to first scene if not found
            if (selectedIndex == -1)
            {
                selectedIndex = 0;
            }

            // Display dropdown
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, sceneNames);

            if (selectedIndex >= 0 
                && selectedIndex < scenePaths.Length)
            {
                // Convert scene path to GUID and store it
                string newSceneGUID = AssetDatabase.AssetPathToGUID(scenePaths[selectedIndex]);
                property.stringValue = newSceneGUID;
            }
        }
    }
}