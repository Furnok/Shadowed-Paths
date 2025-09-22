using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_UILoads : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private S_AutoScroll autoScroll;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject loadSlotPrefab;
    [SerializeField] private Selectable buttonReturn;
    [SerializeField] private S_UIReturn UIReturn;

    [Header("Input")]
    [SerializeField] private RSE_UpdateDataUI rseUpdateDataUI;
    [SerializeField] private RSE_ClearDataUI rseClearDataUI;

    [Header("Output")]
    [SerializeField] private RSO_DataTemp rsoDataTemp;

    private List<S_UILoadSlot> listUILoadSlot = new();

    private void OnEnable()
    {
        rseUpdateDataUI.action += UpdateUILoadSlot;
        rseClearDataUI.action += ClearUILoadSlot;

        PopulateLoadSlots();
    }

    private void OnDisable()
    {
        rseUpdateDataUI.action -= UpdateUILoadSlot;
        rseClearDataUI.action -= ClearUILoadSlot;
    }

    private void PopulateLoadSlots()
    {
        listUILoadSlot.Clear();

        foreach (Transform child in scrollContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < rsoDataTemp.Value.Count; i++)
        {
            GameObject slot = Instantiate(loadSlotPrefab, scrollContent);
            slot.transform.SetParent(scrollContent);

            S_UILoadSlot slotScript = slot.GetComponent<S_UILoadSlot>();

            if (slotScript != null)
            {
                slotScript.Setup(this, rsoDataTemp.Value[i], scrollRect);
            }

            listUILoadSlot.Add(slotScript);

            S_SaveConversion slotScript2 = slot.GetComponent<S_SaveConversion>();

            if (slotScript2 != null)
            {
                slotScript2.Setup(rsoDataTemp.Value[i].saveName);
            }
        }

        SetupNavigation();
    }

    private void SetupNavigation()
    {
        Navigation nav = buttonReturn.navigation;
        nav.mode = Navigation.Mode.Explicit;

        nav.selectOnUp = listUILoadSlot[0].transform.GetChild(0).GetComponent<Selectable>();
        nav.selectOnDown = listUILoadSlot[0].transform.GetChild(0).GetComponent<Selectable>();

        buttonReturn.navigation = nav;

        for (int i = 0; i < listUILoadSlot.Count; i++)
        {
            var current = listUILoadSlot[i].transform.GetChild(0).GetComponent<Selectable>();

            if (current != null)
            {
                nav = current.navigation;
                nav.mode = Navigation.Mode.Explicit;

                if (i > 0)
                {
                    nav.selectOnUp = listUILoadSlot[i - 1].transform.GetChild(0).GetComponent<Selectable>();
                }

                if (i < listUILoadSlot.Count - 1)
                {
                    nav.selectOnDown = listUILoadSlot[i + 1].transform.GetChild(0).GetComponent<Selectable>();
                }

                current.navigation = nav;
            }
        }
    }

    private void UpdateUILoadSlot(string name)
    {
        for (int i = 0; i < listUILoadSlot.Count; i++)
        {
            if (listUILoadSlot[i].saveName == name)
            {
                listUILoadSlot[i].UpdateSlot(rsoDataTemp.Value[i]);
            }
        }
    }

    private void ClearUILoadSlot(string name)
    {
        foreach (S_UILoadSlot slot in listUILoadSlot)
        {
            if (slot.saveName == name)
            {
                slot.ClearSlot();
            }
        }
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