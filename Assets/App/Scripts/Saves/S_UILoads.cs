using System.Collections.Generic;
using UnityEngine;

public class S_UILoads : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject loadSlotPrefab;

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
                slotScript.Setup(rsoDataTemp.Value[i]);
            }

            listUILoadSlot.Add(slotScript);

            S_SaveConversion slotScript2 = slot.GetComponent<S_SaveConversion>();

            if (slotScript2 != null)
            {
                slotScript2.Setup(rsoDataTemp.Value[i].saveName);
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
}