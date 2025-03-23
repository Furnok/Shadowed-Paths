using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<GameObject> windows = new();

    [Header("Input")]
    [SerializeField] private RSE_OpenWindow rseOpenWindow;
    [SerializeField] private RSE_CloseWindow rseCloseWindow;

    private void OnEnable()
    {
        rseOpenWindow.action += OpenWindow;
        rseCloseWindow.action += CloseWindow;
    }

    private void OnDisable()
    {
        rseOpenWindow.action -= OpenWindow;
        rseCloseWindow.action -= CloseWindow;
    }

    private void OpenWindow(string name)
    {
        GameObject window = windows.Find(x => x.name == name);

        if (window != null && !window.activeInHierarchy)
        {
            window.SetActive(true);
        }
    }

    private void CloseWindow(string name)
    {
        GameObject window = windows.Find(x => x.name == name);

        if (window != null && window.activeInHierarchy)
        {
            window.SetActive(false);
        }
    }
}