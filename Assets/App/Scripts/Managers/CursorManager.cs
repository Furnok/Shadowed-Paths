using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Texture2D handCursor;

    [Header("Input")]
    [SerializeField] private RSE_ShowMouseCursor rseShowMouseCursor;
    [SerializeField] private RSE_HideMouseCursor rseHideMouseCursor;
    [SerializeField] private RSE_MouseEnterUI rseMouseEnterUI;
    [SerializeField] private RSE_MouseLeaveUI rseMouseLeaveUI;

    private void OnEnable()
    {
        rseShowMouseCursor.action += ShowMouseCursor;
        rseHideMouseCursor.action += HideMouseCursor;
        rseMouseEnterUI.action += MouseEnter;
        rseMouseLeaveUI.action += MouseLeave;
    }

    private void OnDisable() 
    {
        rseShowMouseCursor.action -= ShowMouseCursor;
        rseHideMouseCursor.action -= HideMouseCursor;
        rseMouseEnterUI.action -= MouseEnter;
        rseMouseLeaveUI.action -= MouseLeave;
    }

    private void Start()
    {
        ShowMouseCursor();
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

    private void MouseEnter()
    {
        Vector2 cursorOffset = new Vector2(handCursor.width / 3, handCursor.height / 40);

        Cursor.SetCursor(handCursor, cursorOffset, CursorMode.Auto);
    }

    private void MouseLeave()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}