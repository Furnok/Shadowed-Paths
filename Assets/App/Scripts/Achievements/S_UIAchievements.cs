using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UIAchievements : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private S_AutoScroll autoScroll;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject achievementSlotPrefab;
    [SerializeField] private Selectable buttonReturn;
    [SerializeField] private S_UIReturn UIReturn;

    [Header("Input")]
    [SerializeField] private RSE_UpdateUIAchievement rseUpdateUIAchievement;

    [Header("Output")]
    [SerializeField] private RSO_Achievements rsoAchievements;

    private List<S_UIAchievementSlot> listUIAchievementSlot = new();

    private void OnEnable()
    {
        PopulateAchievementSlots();

        rseUpdateUIAchievement.action += UpdateUIAchievementSlot;
    }

    private void OnDisable()
    {
        rseUpdateUIAchievement.action -= UpdateUIAchievementSlot;
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
                slotScript.Setup(this, achievement);
            }

            slot.transform.GetChild(0).GetComponent<S_UISelectableScroll>().Setup(scrollRect);

            listUIAchievementSlot.Add(slotScript);
        }

        SetupNavigation();
    }

    private void SetupNavigation()
    {
        Navigation nav = buttonReturn.navigation;
        nav.mode = Navigation.Mode.Explicit;

        nav.selectOnUp = listUIAchievementSlot[0].transform.GetChild(0).GetComponent<Selectable>();
        nav.selectOnDown = listUIAchievementSlot[0].transform.GetChild(0).GetComponent<Selectable>();

        buttonReturn.navigation = nav;

        for (int i = 0; i < listUIAchievementSlot.Count; i++)
        {
            var current = listUIAchievementSlot[i].transform.GetChild(0).GetComponent<Selectable>();

            if (current != null)
            {
                nav = current.navigation;
                nav.mode = Navigation.Mode.Explicit;

                if(i > 0)
                {
                    nav.selectOnUp = listUIAchievementSlot[i - 1].transform.GetChild(0).GetComponent<Selectable>();
                }
                
                if (i < listUIAchievementSlot.Count - 1)
                {
                    nav.selectOnDown = listUIAchievementSlot[i + 1].transform.GetChild(0).GetComponent<Selectable>();
                }

                current.navigation = nav;
            }
        }
    }

    private void UpdateUIAchievementSlot(int id)
    {
        listUIAchievementSlot[id].Unlock();
    }

    public void ScrollAuto(Selectable item)
    {
        autoScroll.ScrollToIndex(item);

        UIReturn.SetBlocked();

        Navigation nav = buttonReturn.navigation;
        nav.mode = Navigation.Mode.Explicit;

        nav.selectOnUp = item;
        nav.selectOnDown = item;

        buttonReturn.navigation = nav;
    }
}