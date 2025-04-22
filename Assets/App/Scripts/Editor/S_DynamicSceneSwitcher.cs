using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.IO;
using System.Linq;

public class S_DynamicSceneSwitcher : EditorWindow
{
    private Vector2 scrollPosition;
    private string[] scenePaths;

    private GUIStyle buttonStyle;
    private GUIStyle buttonRefreshStyle;
	private float scale;

    [MenuItem("Tools/Dynamic Scenes Switcher")]
    public static void ShowWindow()
    {
        S_DynamicSceneSwitcher window = GetWindow<S_DynamicSceneSwitcher>();
        window.minSize = new Vector2(300, 300);
        window.maxSize = new Vector2(1000, 1000);
        window.position = new Rect(100, 100, 400, 400);
    }

    private void OnEnable()
    {
        RefreshSceneList();
    }

    private void RefreshSceneList()
    {
        scenePaths = AssetDatabase.FindAssets("t:Scene")
            .Select(AssetDatabase.GUIDToAssetPath)
            .Where(path => path.StartsWith("Assets/"))
            .ToArray();
    }

    private void SetupStyles()
    {
        float width = position.width;

        // Calcule dynamique
        scale = Mathf.Clamp(width / 400f, 0.75f, 2f);

        buttonStyle = new GUIStyle(GUI.skin.button)
        {
            margin = new RectOffset(20, 20, 5, 5),
            padding = new RectOffset(10, 10, 8, 8),
            fontStyle = FontStyle.Normal,
            fontSize = Mathf.RoundToInt(14 * scale),
            alignment = TextAnchor.MiddleCenter
        };

        buttonRefreshStyle = new GUIStyle(GUI.skin.button)
        {
            margin = new RectOffset(40, 40, 10, 10),
            padding = new RectOffset(15, 15, 10, 10),
            fontStyle = FontStyle.Bold,
            fontSize = Mathf.RoundToInt(16 * scale),
            normal = { textColor = Color.white },
            alignment = TextAnchor.MiddleCenter
        };
    }

    private void OnGUI()
    {
        SetupStyles();

        EditorGUILayout.Space(5);

        if (scenePaths == null || scenePaths.Length == 0)
        {
            EditorGUILayout.LabelField("No Scenes Found", EditorStyles.centeredGreyMiniLabel);
        }
        else
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true));

            foreach (string scenePath in scenePaths)
            {
                if (GUILayout.Button(Path.GetFileNameWithoutExtension(scenePath), buttonStyle) &&
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scenePath);
                }
            }

            EditorGUILayout.EndScrollView();
        }

        EditorGUILayout.Space(5);

        if (GUILayout.Button("Refresh Scenes", buttonRefreshStyle))
        {
            RefreshSceneList();
            Repaint();
        }

        EditorGUILayout.Space(5);
    }
}