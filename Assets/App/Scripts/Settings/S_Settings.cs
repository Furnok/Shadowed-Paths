using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class S_Settings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, S_SaveName] private string saveSettingsName;

    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Input")]
    [SerializeField] private RSO_SettingsSaved rsoSettingsSaved;

    [Header("Output")]
    [SerializeField] private RSE_SaveData rseSaveData;

    private bool isLoaded = false;
    private int multiplicatorVolume = 0;
    private List<TextMeshProUGUI> listTextAudios = new();

    private void OnEnable()
    {
        isLoaded = false;
    }

    public void Setup(int multiplicator, List<TextMeshProUGUI> listTextVolumes)
    {
        isLoaded = true;

        multiplicatorVolume = multiplicator;
        listTextAudios = listTextVolumes;
    }

    public void UpdateLanguages(int index)
    {
        if (isLoaded)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

            rsoSettingsSaved.Value.languageIndex = index;

            Save();
        }
    }

    public void UpdateFullscreen(bool value)
    {
        if (isLoaded)
        {
            if (value)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }

            Screen.fullScreen = value;

            rsoSettingsSaved.Value.fullScreen = value;

            Save();
        }
    }

    private Resolution GetResolutions(int index)
    {
        List<Resolution> resolutionsPC = new(Screen.resolutions);
        resolutionsPC.Reverse();

        Resolution resolution = resolutionsPC[0];

        for (int i = 0; i < resolutionsPC.Count; i++)
        {
            Resolution res = resolutionsPC[i];

            if (i == index)
            {
                resolution = res;
            }
        }

        return resolution;
    }

    public void UpdateResolutions(int index)
    {
        if (isLoaded)
        {
            Resolution resolution = GetResolutions(index);

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);

            rsoSettingsSaved.Value.resolutionIndex = index;

            Save();
        }
    }

    public void UpdateMainVolume(float value)
    {
        if (isLoaded)
        {
            UpdateVolumes(value, 0);
        }
    }

    public void UpdateMusicVolume(float value)
    {
        if (isLoaded)
        {
            UpdateVolumes(value, 1);
        }
    }

    public void UpdateSoundsVolume(float value)
    {
        if (isLoaded)
        {
            UpdateVolumes(value, 2);
        }
    }

    public void UpdateUIVolume(float value)
    {
        if (isLoaded)
        {
            UpdateVolumes(value, 3);
        }
    }

    private void UpdateVolumes(float value, int index)
    {
        value = value * multiplicatorVolume;

        audioMixer.SetFloat(rsoSettingsSaved.Value.listVolumes[index].name, 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

        rsoSettingsSaved.Value.listVolumes[index].volume = value;

        listTextAudios[index].text = $"{value}%";

        Save();
    }

    private void Save()
    {
        rseSaveData.Call(saveSettingsName, true, false);
    }
}