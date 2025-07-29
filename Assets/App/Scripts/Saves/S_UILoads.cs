using System.Collections.Generic;
using UnityEngine;

public class S_UILoads : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, S_SaveName] private List<string> saveNames;

    [Header("References")]
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject loadSlotPrefab;

    private List<S_UILoadSlot> listUILoadSlot = new();

    private void OnEnable()
    {
        PopulateLoadSlots();
    }

    private void PopulateLoadSlots()
    {
        listUILoadSlot.Clear();

        foreach (Transform child in scrollContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < saveNames.Count; i++)
        {
            GameObject slot = Instantiate(loadSlotPrefab, scrollContent);
            slot.transform.SetParent(scrollContent);

            S_UILoadSlot slotScript = slot.GetComponent<S_UILoadSlot>();

            if (slotScript != null)
            {
                slotScript.Setup(saveNames[i]);
            }

            listUILoadSlot.Add(slotScript);

            S_SaveConversion slotScript2 = slot.GetComponent<S_SaveConversion>();

            if (slotScript2 != null)
            {
                slotScript2.Setup(saveNames[i]);
            }
        }
    }

    private void UpdateUILoadSlot()
    {
        
    }
}