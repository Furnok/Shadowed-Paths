using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_UISelectableScroll : MonoBehaviour, IScrollHandler
{
    [Header("References")]
    [SerializeField] private ScrollRect parentScrollRect;

    public void OnScroll(PointerEventData eventData)
    {
        if (parentScrollRect != null)
        {
            parentScrollRect.OnScroll(eventData);
        }
    }
}