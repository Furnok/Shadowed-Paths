using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SceneManagement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private S_SceneReference main;
    [SerializeField] private S_SceneReference menuName;
    [SerializeField] private S_SceneReference[] levelsName;

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

        rsoCurrentLevel.Value = null;
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
            StartCoroutine(S_Utils.UnloadSceneAsync(rsoCurrentLevel.Value));
        }

        StartCoroutine(S_Utils.LoadSceneAsync(sceneName, LoadSceneMode.Additive, () =>
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