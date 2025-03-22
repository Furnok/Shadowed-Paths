using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] SceneReference main;
    [SerializeField] SceneReference mainMenuName;
    [SerializeField] SceneReference[] levelsName;

    //[Header("Input")]

    //[Header("Output")]

    private string currentLevel = "";
    private bool isLoading = false;

    private void Start()
    {
        LoadLevel(mainMenuName.Name);
    }

    private void LoadLevel(string sceneName)
    {
        if (isLoading) return;

        isLoading = true;

        Transition(sceneName);
    }

    private void Transition(string sceneName)
    {
        if (currentLevel != "")
        {
            StartCoroutine(Utils.UnloadSceneAsync(currentLevel));
        }

        StartCoroutine(Utils.LoadSceneAsync(sceneName, LoadSceneMode.Additive, () =>
        {
            isLoading = false;

            currentLevel = sceneName;
        }));
    }
}