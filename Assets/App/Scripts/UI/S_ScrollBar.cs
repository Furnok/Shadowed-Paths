using UnityEngine;
using UnityEngine.UI;

public class S_ScrollBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;

    private void OnEnable()
    {
        StartCoroutine(S_Utils.DelayFrame(() => scrollRect.verticalNormalizedPosition = 1));
    }
}