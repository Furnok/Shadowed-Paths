using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_UIReturn : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject returnContent;
    [SerializeField] private GameObject currentContent;
    [SerializeField] private List<GameObject> block;

    [Header("Input")]
    [SerializeField] private RSE_Escape rseEscape;

    [Header("Output")]
    [SerializeField] private RSE_OpenWindow rseOpenWindow;
    [SerializeField] private RSE_CloseWindow rseCloseWindow;
    [SerializeField] private RSO_DefaultSelectable rsoDefaultSelectable;

    private bool blocked = false;

    private void OnEnable()
    {
        rseEscape.action += Return;
    }

    private void OnDisable()
    {
        rseEscape.action -= Return;
    }

    private bool Blocker()
    {
        foreach (GameObject obj in block)
        {
            if (obj == null) continue;

            TMP_Dropdown dropdown = obj.GetComponent<TMP_Dropdown>();

            if (dropdown != null && dropdown.IsExpanded)
            {
                return true;
            }

            TMP_InputField inputField = obj.GetComponent<TMP_InputField>();

            if (inputField != null && inputField.isFocused)
            {
                return true;
            }
        }

        return false;
    }

    public void SetBlocked()
    {
        blocked = true;
    }

    public void Return()
    {
        if (blocked && Gamepad.current != null)
        {
            blocked = false;

            rsoDefaultSelectable?.Value.Select();
            rsoDefaultSelectable?.Value.GetComponent<S_UISelectable>()?.Selected(rsoDefaultSelectable.Value);
        }
        else if (!Blocker())
        {
            rseCloseWindow.Call(currentContent);

            rseOpenWindow.Call(returnContent);
        }
    }
}