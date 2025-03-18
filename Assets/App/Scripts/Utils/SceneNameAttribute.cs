using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SceneNameAttribute
{
    [SerializeField] private string sceneName;
    [SerializeField] private string sceneGUID;

    public string GUID => sceneGUID;
    public string Name => sceneName;

    public void SetScene(string guid)
    {
        sceneGUID = guid;
        sceneName = GetSceneNameFromGUID(guid);
    }

    private static string GetSceneNameFromGUID(string guid)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        return System.IO.Path.GetFileNameWithoutExtension(path);
    }
}