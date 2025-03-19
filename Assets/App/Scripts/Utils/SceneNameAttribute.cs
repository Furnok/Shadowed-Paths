using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class SceneReference
{
    [SerializeField] private string sceneName;
    [SerializeField] private string sceneGUID;

    public string GUID => sceneGUID;
    public string Name => sceneName;
}