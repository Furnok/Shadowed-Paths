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

            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use [SceneName] with a string.");
                EditorGUI.EndProperty();
                return;
            }

            var buildScenes = EditorBuildSettings.scenes;

            if (buildScenes.Length <= 0)
            {
                EditorGUI.LabelField(position, label.text, "No scenes in Build Settings.");
                EditorGUI.EndProperty();
                return;
            }

            // Get List of Scene Paths and Names
            var scenePaths = buildScenes.Select(scene => scene.path).ToArray();
            var sceneNames = scenePaths.Select(System.IO.Path.GetFileNameWithoutExtension).ToArray();

            string storedScenePath = AssetDatabase.GUIDToAssetPath(property.stringValue);
            int selectedIndex = Mathf.Max(0, System.Array.IndexOf(scenePaths, storedScenePath));

            // Find Scene Name if Path is Missing
            if (selectedIndex == 0 && string.IsNullOrEmpty(storedScenePath))
            {
                string storedSceneName = System.IO.Path.GetFileNameWithoutExtension(storedScenePath);
                selectedIndex = Mathf.Max(0, sceneNames.ToList().FindIndex(name => name == storedSceneName));
            }

            property.stringValue = AssetDatabase.AssetPathToGUID(scenePaths[EditorGUI.Popup(position, label.text, selectedIndex, sceneNames)]);
            EditorGUI.EndProperty();
        }
    }
}