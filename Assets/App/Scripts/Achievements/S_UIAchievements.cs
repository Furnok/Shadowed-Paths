using System.Collections.Generic;
using UnityEngine;

public class S_UIAchievements : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject achievementSlotPrefab;

    [Header("Input")]
    [SerializeField] private RSE_UpdateUIAchievement rseUpdateUIAchievement;

    [Header("Output")]
    [SerializeField] private RSO_Achievements rsoAchievements;

    private List<S_UIAchievementSlot> listUIAchievementSlot = new();

    private void OnEnable()
    {
        PopulateAchievementSlots();

        rseUpdateUIAchievement.action += UpdateUIAchivementSlot;
    }

    private void OnDisable()
    {
        rseUpdateUIAchievement.action -= UpdateUIAchivementSlot;
    }

    private void PopulateAchievementSlots()
    {
        listUIAchievementSlot.Clear();

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

            listUIAchievementSlot.Add(slotScript);
        }
    }

    private void UpdateUIAchivementSlot(int id)
    {
        listUIAchievementSlot[id].Unlock();
    }
}