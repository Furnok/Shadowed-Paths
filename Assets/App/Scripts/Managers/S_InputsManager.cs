using UnityEngine;
using UnityEngine.InputSystem;

public class S_InputsManager : MonoBehaviour
{
    [Header("Output")]
    [SerializeField] private RSE_Escape rseEscape;
    [SerializeField] private RSE_Move rseMove;

    private bool isMoving = false;
    private float moveValue = 0;

    public void EscapeInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            rseEscape.Call();
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveValue = ctx.ReadValue<float>();

        if (ctx.started)
        {
            isMoving = true;
        } 
        else if (ctx.canceled)
        {
            isMoving = false;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            rseMove.Call(moveValue);
        }
    }
}