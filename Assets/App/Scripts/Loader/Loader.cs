using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SceneReference main;

    [Header("Output")]
    [SerializeField] private RSO_CurrentLevel rsoCurrentLevel;

    private void Start()
    {
        if (!SceneManager.GetSceneByName(main.Name).isLoaded)
        {
            rsoCurrentLevel.Value = null;
            StartCoroutine(Utils.LoadSceneAsync(main.Name, LoadSceneMode.Additive));
            rsoCurrentLevel.Value = SceneManager.GetActiveScene().name;
        }
    }
}