using System;
using UnityEngine;

[Serializable]
public class S_SceneReference
{
    [SerializeField] private string sceneName = "";
    [SerializeField] private string sceneGUID = "";

    public string Name => sceneName;

    public string GUID => sceneGUID;
}