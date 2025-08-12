using UnityEngine;
using UnityEngine.InputSystem;

public class S_InputsManager : MonoBehaviour
{
    [Header("Output")]
    [SerializeField] private RSE_Escape rseEscape;

    public void EscapeInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            rseEscape.Call();
        }
    }
}