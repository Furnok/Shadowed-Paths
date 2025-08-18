using DG.Tweening;
using UnityEngine;

public class S_Camera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float duration;

    [Header("References")]
    [SerializeField] private Transform targetTransform;

    //[Header("Input")]

    //[Header("Output")]

    private void Update()
    {
        Vector3 targetPos = new Vector3(targetTransform.position.x, 0, -10);

        transform.DOKill();

        transform.DOMove(targetPos, duration);
    }
}