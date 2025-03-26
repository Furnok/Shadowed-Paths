using UnityEngine;

public class S_SceneConversion : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private S_SceneReference saveName;

    [Header("Output")]
    [SerializeField] private RSE_LoadLevel rseLoadLevel;

    public void ButtonPressScene()
    {
        rseLoadLevel.Call(saveName.Name);
    }
}