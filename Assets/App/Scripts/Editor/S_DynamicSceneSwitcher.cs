using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.IO;
using System.Linq;

public class S_DynamicSceneSwitcher : EditorWindow
{
    private Vector2 scrollPosition;
    private string[] scenePaths;

    private GUIStyle labelStyle;
    private GUIStyle labelError;
    private GUIStyle buttonStyle;
    private GUIStyle buttonRefreshStyle;

    [MenuItem("Tools/Dynamic Scenes Switcher")]
    public static void ShowWindow()
    {
        // Create the UI
        S_DynamicSceneSwitcher window = GetWindow<S_DynamicSceneSwitcher>("Scenes Switcher");
        window.minSize = new Vector2(300, 300);
        window.maxSize = new Vector2(600, 600);

        window.position = new Rect(100, 100, 300, 300);
    }

    private void OnEnable()
    {
        RefreshSceneList();
    }

    private void RefreshSceneList()
    {
        // Get All Scenes in the Folder Assets in the Project
		scenePaths = AssetDatabase.FindAssets("t:Scene").Select(AssetDatabase.GUIDToAssetPath).Where(path => path.StartsWith("Assets/")).ToArray();
    }

    private void SetupStyles()
    {
        labelStyle = new GUIStyle(EditorStyles.label)
        {
            fontStyle = FontStyle.Bold,
            fontSize = 18,
            alignment = TextAnchor.MiddleCenter,
            margin = new RectOffset(10, 10, 10, 5),
        };

        labelError = new GUIStyle(EditorStyles.label)
        {
            fontSize = 14,
            alignment = TextAnchor.MiddleCenter,
            margin = new RectOffset(10, 10, 10, 5),
        };

        buttonStyle = new GUIStyle(GUI.skin.button)
        {
            margin = new RectOffset(30, 30, 10, 5),
            padding = new RectOffset(10, 10, 5, 5),
            fontStyle = FontStyle.Bold,
            fontSize = 16,
        };

        buttonRefreshStyle = new GUIStyle(GUI.skin.button)
        {
            margin = new RectOffset(30, 30, 10, 5),
            padding = new RectOffset(10, 10, 15, 15),
            fontStyle = FontStyle.Bold,
            normal = { textColor = Color.white },
            fontSize = 20,
        };
    }

    private void OnGUI()
    {
        SetupStyles();

        GUILayout.Label("All Scenes in the Project Assets:", labelStyle);

        EditorGUILayout.Space(5);

        if (scenePaths == null || scenePaths.Length == 0)
        {
            GUILayout.Label("No Scenes Found", labelError);
		}
        else
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));

            foreach (string scenePath in scenePaths)
            {
                if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePath), buttonStyle) && EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scenePath);
                }
            }

            EditorGUILayout.EndScrollView();
        }

        EditorGUILayout.Space(5);

        if (GUILayout.Button("REFRESH SCENES", buttonRefreshStyle))
        {
            RefreshSceneList();
            Repaint();
        }

        EditorGUILayout.Space(5);
    }
}