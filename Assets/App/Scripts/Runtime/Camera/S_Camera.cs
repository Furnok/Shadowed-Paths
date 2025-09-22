using System.Collections.Generic;
using UnityEngine;

public class S_Camera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float smoothTime;
    [SerializeField] private List<Vector2> listPointBound;

    [Header("References")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Camera cameraMain;

    private Vector3 velocity = Vector3.zero;
    private float halfHeight = 0;
    private float halfWidth = 0;
    private Vector2 minBounds = Vector2.zero;
    private Vector2 maxBounds = Vector2.zero;

    private void Awake()
    {
        halfHeight = cameraMain.orthographicSize;
        halfWidth = halfHeight * cameraMain.aspect;

        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (var p in listPointBound)
        {
            if (p.x < minX) minX = p.x;
            if (p.x > maxX) maxX = p.x;
            if (p.y < minY) minY = p.y;
            if (p.y > maxY) maxY = p.y;
        }

        minBounds = new Vector2(minX, minY);
        maxBounds = new Vector2(maxX, maxY);
    }

    private Vector3 CameraBound()
    {
        float clampedX = Mathf.Clamp(targetTransform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(targetTransform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        return new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void LateUpdate()
    {
        if (!targetTransform) return;

        if (Mathf.Abs(transform.position.x - targetTransform.position.x) < 0.01f) return;

        transform.position = Vector3.SmoothDamp(transform.position, CameraBound(), ref velocity, smoothTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (listPointBound == null || listPointBound.Count < 2) return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < listPointBound.Count; i++)
        {
            Vector3 current = transform.TransformPoint(listPointBound[i]);
            Vector3 next = transform.TransformPoint(listPointBound[(i + 1) % listPointBound.Count]);

            Gizmos.DrawSphere(current, 0.1f);

            Gizmos.DrawLine(current, next);
        }
    }
}