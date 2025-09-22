using UnityEngine;

public class S_Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float playerSpeed;

    [Header("References")]
    [SerializeField] private Rigidbody2D playerRb;

    [Header("Input")]
    [SerializeField] private RSE_Move rseMove;

    private void OnEnable()
    {
        rseMove.action += Move;
    }

    private void OnDisable()
    {
        rseMove.action -= Move;
    }

    private void Move(float value)
    {
        playerRb.linearVelocity = new Vector2(value * playerSpeed, 0);
    }
}