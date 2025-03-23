using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RSE_OpenWindow rseOpenWindow;
    [SerializeField] private RSE_CloseWindow rseCloseWindow;
    [SerializeField] private RSE_CloseAllWindows rseCloseAllWindows;

    private List<GameObject> currentWindows = new();

    private void OnEnable()
    {
        rseOpenWindow.action += AlreadyOpen;
        rseCloseWindow.action += CloseWindow;
        rseCloseAllWindows.action += CloseAllWindows;
    }

    private void OnDisable()
    {
        rseOpenWindow.action -= AlreadyOpen;
        rseCloseWindow.action -= CloseWindow;
        rseCloseAllWindows.action -= CloseAllWindows;
    }

    private void AlreadyOpen(GameObject window)
    {
        if (window != null && !window.activeInHierarchy)
        {
            OpenWindow(window);
        }
        else
        {
            CloseWindow(window);
        }
    }

    private void OpenWindow(GameObject window)
    {
        window.SetActive(true);

        currentWindows.Add(window);
    }

    private void CloseWindow(GameObject window)
    {
        if (window != null && window.activeInHierarchy)
        {
            window.SetActive(false);

            currentWindows.Remove(window);
        }
    }

    private void CloseAllWindows()
    {
        foreach (var window in currentWindows)
        {
            window.SetActive(false);
        }

        currentWindows.Clear();
    }
}