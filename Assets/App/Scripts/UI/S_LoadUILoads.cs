using TMPro;
using UnityEngine;
using BT.Save;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.Localization.Settings;
using System;
using UnityEngine.Localization;

public class S_LoadUILoads : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, S_SaveName] private string saveName;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textZone;
    [SerializeField] private TextMeshProUGUI textDate;
    [SerializeField] private Button buttonLoad;
    [SerializeField] private Button buttonDelete;

    [Header("Input")]
    [SerializeField] private RSE_DataUI rseDataUI;
    [SerializeField] private RSE_DataTemp rseDataTemp;

    [Header("Output")]
    [SerializeField] private RSE_LoadTempData rseLoadTempData;

    private void OnEnable()
    {
        rseDataUI.action += Initialisation;
        rseDataTemp.action += UpdateData;

        StartCoroutine(S_Utils.DelayFrame(() => Initialisation(saveName)));
    }

    private void OnDisable()
    {
        rseDataUI.action -= Initialisation;
        rseDataTemp.action -= UpdateData;
    }

    private void Initialisation(string name)
    {
        if (name == saveName)
        {
            textZone.text = "";
            textDate.text = "";
            buttonLoad.interactable = false;
            buttonDelete.interactable = false;

            rseLoadTempData.Call(saveName);
        }
    }

    private void UpdateData(S_ContentSaved data)
    {
        if (data.saveName == saveName)
        {
            if (data.dateSaved != "")
            {
                if (DateTime.TryParse(data.dateSaved, out DateTime parsedDate))
                {
                    CultureInfo culture = LocalizationSettings.SelectedLocale.Identifier.CultureInfo;

                    string localizedDate = parsedDate.ToString("d", culture);

                    textZone.text = $"{data.playerName}, {data.currentLevel}";
                    textDate.text = $"{localizedDate}";
                    buttonLoad.interactable = true;
                    buttonDelete.interactable = true;
                }
            }
            else
            {
                textZone.text = "";
                textDate.text = "";
                buttonLoad.interactable = false;
                buttonDelete.interactable = false;
            }
        }
    }
}