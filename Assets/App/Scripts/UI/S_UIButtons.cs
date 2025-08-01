using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class S_UIButtons : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float transition = 0.2f;
    [SerializeField] private Color32 colorMouseEnter = new(200, 200, 200, 255);
    [SerializeField] private Color32 colorMouseDown = new(150, 150, 150, 255);

    [Header("References")]
    [SerializeField] private Image image;

    private Color32 colorBase = new();
    private bool mouseOver = false;
    private bool isPressed = false;
    private Tween colorTween = null;

    private void Start()
    {
        colorBase = image.color;
    }

    public void MouseEnter(Selectable uiElement)
    {
        if (uiElement.interactable)
        {
            if (!isPressed)
            {
                PlayColorTransition(colorMouseEnter);
            }

            mouseOver = true;
        }
    }

    public void MouseExit(Selectable uiElement)
    {
        if (uiElement.interactable)
        {
            if (!isPressed)
            {
                PlayColorTransition(colorBase);
            }

            mouseOver = false;
        }
    }


    public void MouseDown(Selectable uiElement)
    {
        if (uiElement.interactable)
        {
            PlayColorTransition(colorMouseDown);

            isPressed = true;
        }
    }

    public void MouseUp(Selectable uiElement)
    {
        if (uiElement.interactable)
        {
            if (mouseOver)
            {
                PlayColorTransition(colorMouseEnter);
            }  
            else
            {
                PlayColorTransition(colorBase);
            }

            isPressed = false;
        }
    }

    private void PlayColorTransition(Color32 targetColor)
    {
        colorTween?.Kill();

        colorTween = image.DOColor(targetColor, transition);
    }
}