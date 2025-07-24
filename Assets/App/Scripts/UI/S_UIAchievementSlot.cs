using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_UIAchievementSlot : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color32 colorUnLock;
    [SerializeField] private Color32 colorLock;

    [Header("References")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    public void Setup(S_ClassAchievements achievement)
    {
        image.sprite = achievement.image;
        title.text = achievement.title.GetLocalizedString();
        description.text = achievement.description.GetLocalizedString();

        if (achievement.unlocked)
        {
            image.color = colorUnLock;
            title.color = colorUnLock;
            description.color = colorUnLock;
        }
        else
        {
            image.color = colorLock;
            title.color = colorLock;
            description.color = colorLock;
        }
    }
}