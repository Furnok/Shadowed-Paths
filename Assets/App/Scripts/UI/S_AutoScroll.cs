using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class S_AutoScroll : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float transition = 0.05f;

    [Header("References")]
    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform content;

    private int number;
    private Tween moveTween = null;
    private S_SerializableDictionary<Selectable, int> selectables = new();

    private void OnEnable()
    {
        number = dropDown.options.Count - 1;

        StartCoroutine(S_Utils.Delay(0.5f, () => Setup()));
    }

    private void Setup()
    {
        selectables.Clear();

        for (int i = 0; i <= number; i++)
        {
            Transform item = content.GetChild(i + 1);
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
        if (selectables.TryGetValue(item, out int index))
        {
            float targetPos = 1f - ((float)index / number);
            moveTween = scrollRect.DOVerticalNormalizedPos(targetPos, transition).SetEase(Ease.Linear);
        }
    }
}