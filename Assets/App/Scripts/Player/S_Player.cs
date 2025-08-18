using UnityEngine;

public class S_Player : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float playerSpeed;

    [Header("References")]
    [SerializeField] private Rigidbody2D playerRb;

    //[Header("Input")]

    //[Header("Output")]

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerRb.linearVelocity = new Vector2(-1 * playerSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerRb.linearVelocity = new Vector2(1 * playerSpeed, 0);
        }
    }
}