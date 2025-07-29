using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SaveConversion : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, S_SaveName] private string saveName;

    [Header("Output")]
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    [SerializeField] private RSE_ResetCursor rseResetCursor;
    [SerializeField] private RSE_SaveData rseSaveData;
    [SerializeField] private RSE_LoadData rseLoadData;
    [SerializeField] private RSE_DeleteData rseDeleteData;
    [SerializeField] private RSE_LoadLevel rseLoadLevel;

    public void Setup(string name)
    {
        saveName = name;
    }

    public void ButtonPressSaveData()
    {
        rsoContentSaved.Value.dateSaved = $"{DateTime.Now:yyyy-MM-dd}, {DateTime.Now:HH:mm:ss}";
        rsoContentSaved.Value.currentLevel = SceneManager.GetActiveScene().name;
        rseSaveData.Call(saveName, false, false);
    }

    public void ButtonPressLoadData()
    {
        rseLoadData.Call(saveName, false, false);

        StartCoroutine(S_Utils.DelayFrame(() => rseLoadLevel.Call(rsoContentSaved.Value.currentLevel)));
    }

    public void ButtonPressDeleteData()
    {
        rseDeleteData.Call(saveName);
        rseResetCursor.Call();
    }
}