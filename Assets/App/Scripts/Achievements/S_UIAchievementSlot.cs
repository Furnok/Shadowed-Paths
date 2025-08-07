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

    private S_UIAchievements currentUIAchievements = null;
    private S_ClassAchievements currentAchievement = null;

    public void Setup(S_UIAchievements UIAchievements, S_ClassAchievements achievement)
    {
        currentUIAchievements = UIAchievements;

        currentAchievement = achievement;

        image.sprite = currentAchievement.image;
        title.text = $"{currentAchievement.title.GetLocalizedString()}";
        description.text = $"{currentAchievement.description.GetLocalizedString()}";

        Unlock();
    }

    public void Unlock()
    {
        if (currentAchievement.unlocked)
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

    public void ScrollAuto(Selectable item)
    {
        currentUIAchievements.ScrollAuto(item);
    }
}