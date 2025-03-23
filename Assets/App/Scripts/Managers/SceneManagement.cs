using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SceneReference main;
    [SerializeField] private SceneReference menuName;
    [SerializeField] private SceneReference[] levelsName;

    [Header("Inputs")]
    [SerializeField] private RSE_LoadLevel rseLoadLevel;
    [SerializeField] private RSE_QuitGame rseQuitGame;

    [Header("Output")]
    [SerializeField] private RSO_CurrentLevel rsoCurrentLevel;

    private bool isLoading = false;

    private void OnEnable()
    {
        rseLoadLevel.action += LoadLevel;
        rseQuitGame.action += QuitGame;
    }

    private void OnDisable()
    {
        rseLoadLevel.action -= LoadLevel;
        rseQuitGame.action -= QuitGame;
    }

    private void Start()
    {
        if (SceneManager.sceneCount < 2)
        {
            rsoCurrentLevel.Value = null;
            LoadLevel(menuName.Name);
        }
    }

    private void LoadLevel(string sceneName)
    {
        if (isLoading) return;

        isLoading = true;

        Transition(sceneName);
    }

    private void Transition(string sceneName)
    {
        if (rsoCurrentLevel.Value != null)
        {
            StartCoroutine(Utils.UnloadSceneAsync(rsoCurrentLevel.Value));
        }

        StartCoroutine(Utils.LoadSceneAsync(sceneName, LoadSceneMode.Additive, () =>
        {
            isLoading = false;

            rsoCurrentLevel.Value = sceneName;
        }));
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}