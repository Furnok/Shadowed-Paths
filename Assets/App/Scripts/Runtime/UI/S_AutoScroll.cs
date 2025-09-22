using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class S_AutoScroll : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float transition = 0.05f;

    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform content;

    private Tween moveTween = null;
    private S_SerializableDictionary<Selectable, int> selectables = new();

    private void OnEnable()
    {
        StartCoroutine(S_Utils.Delay(0.5f, () => Setup()));
    }

    private void Setup()
    {
        selectables.Clear();

        for (int i = 0; i < content.childCount; i++)
        {
            Transform item = content.GetChild(i).transform.GetChild(0);
            if (item.TryGetComponent(out Selectable selectable))
            {
                selectables[selectable] = i;
            }
        }
    }

    private void OnDisable()
    {
        moveTween?.Kill();
    }

    public void ScrollToIndex(Selectable item)
    {
        if (selectables.TryGetValue(item, out int index) && Gamepad.current != null)
        {
            float targetPos = 1f - ((float)index / (content.childCount - 1));
            moveTween = scrollRect.DOVerticalNormalizedPos(targetPos, transition).SetEase(Ease.Linear);
        }
    }
}