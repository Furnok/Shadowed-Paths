using UnityEngine;

public class S_UIAchievements : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject achievementSlotPrefab;

    //[Header("Input")]

    [Header("Output")]
    [SerializeField] private RSO_Achievements rsoAchievements;

    private void OnEnable()
    {
        PopulateAchievementSlots();
    }

    private void PopulateAchievementSlots()
    {
        foreach (Transform child in scrollContent)
        {
            Destroy(child.gameObject);
        }

        foreach (var achievement in rsoAchievements.Value)
        {
            GameObject slot = Instantiate(achievementSlotPrefab, scrollContent);
            slot.transform.SetParent(scrollContent);

            S_UIAchievementSlot slotScript = slot.GetComponent<S_UIAchievementSlot>();

            if (slotScript != null)
            {
                slotScript.Setup(achievement);
            }
        }
    }
}