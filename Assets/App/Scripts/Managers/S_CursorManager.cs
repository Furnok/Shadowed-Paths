using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_CursorManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Texture2D handCursor;

    [Header("Input")]
    [SerializeField] private RSE_ShowMouseCursor rseShowMouseCursor;
    [SerializeField] private RSE_HideMouseCursor rseHideMouseCursor;
    [SerializeField] private RSE_DeleteData rseDeleteData;
    [SerializeField] private RSE_MouseEnterUI rseMouseEnterUI;
    [SerializeField] private RSE_MouseLeaveUI rseMouseLeaveUI;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        rseShowMouseCursor.action += ShowMouseCursor;
        rseHideMouseCursor.action += HideMouseCursor;
        rseDeleteData.action += DefaultCursor;
        rseMouseEnterUI.action += MouseEnter;
        rseMouseLeaveUI.action += MouseLeave;
    }

    private void OnDisable() 
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
        rseShowMouseCursor.action -= ShowMouseCursor;
        rseHideMouseCursor.action -= HideMouseCursor;
        rseDeleteData.action -= DefaultCursor;
        rseMouseEnterUI.action -= MouseEnter;
        rseMouseLeaveUI.action -= MouseLeave;
    }

    private void Start()
    {
        ShowMouseCursor();
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad)
        {
            if (change == InputDeviceChange.Added)
            {
                HideMouseCursor();
            }
            else if (change == InputDeviceChange.Removed)
            {
                EventSystem.current.SetSelectedGameObject(null);

                ShowMouseCursor();
            }
        }
    }

    private void ShowMouseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void HideMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void MouseEnter(Button button)
    {
        if (button.interactable)
        {
            Vector2 cursorOffset = new Vector2(handCursor.width / 3, handCursor.height / 40);

            Cursor.SetCursor(handCursor, cursorOffset, CursorMode.Auto);
        }
    }

    private void MouseLeave(Button button)
    {
        if (button.interactable)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void DefaultCursor(string name)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}