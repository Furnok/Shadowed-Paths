using UnityEngine;

public class S_Camera : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] private Transform targetTransform;

    //[Header("Input")]

    //[Header("Output")]

    private void Update()
    {
        transform.position = new Vector3(targetTransform.position.x, 0, -10);
    }
}