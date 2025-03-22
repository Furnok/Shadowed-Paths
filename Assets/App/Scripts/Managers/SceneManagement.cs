using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SceneReference main;
    [SerializeField] SceneReference menuName;
    [SerializeField] SceneReference[] levelsName;

    private string currentLevel = "";
    private bool isLoading = false;

    private void Start()
    {
        LoadLevel(menuName.Name);
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