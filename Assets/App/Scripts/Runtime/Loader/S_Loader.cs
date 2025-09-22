using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Loader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private S_SceneReference main;

    [Header("Output")]
    [SerializeField] private RSO_CurrentLevel rsoCurrentLevel;

    private void Awake()
    {
        if (!SceneManager.GetSceneByName(main.Name).isLoaded)
        {
            rsoCurrentLevel.Value = null;
            StartCoroutine(S_Utils.LoadSceneAsync(main.Name, LoadSceneMode.Additive));
            rsoCurrentLevel.Value = SceneManager.GetActiveScene().name;
        }
    }
}