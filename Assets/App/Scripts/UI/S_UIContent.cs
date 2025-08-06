using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_UIContent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Selectable defaultButton;

    private void OnEnable()
    {
        if (Gamepad.current != null)
        {
            defaultButton?.Select();
            defaultButton?.GetComponent<S_UISelectable>()?.Selected(defaultButton);
        }
    }

    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}