using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class S_Settings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int multiplicatorVolume;
    [SerializeField, S_SaveName] private string saveSettingsName;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textAudioMain;
    [SerializeField] private TextMeshProUGUI textAudioMusic;
    [SerializeField] private TextMeshProUGUI textAudioSounds;
    [SerializeField] private TextMeshProUGUI textAudioUI;
    [SerializeField] private AudioMixer audioMixer;

    [Header("Input")]
    [SerializeField] private RSO_SettingsSaved rsoSettingsSaved;

    [Header("Output")]
    [SerializeField] private RSE_SaveData rseSaveData;

    private bool isLoaded = false;

    private void OnEnable()
    {
        isLoaded = false;
    }

    public void SetIsLoaded()
    {
        isLoaded = true;
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

    public void UpdateAudioMain(float value)
    {
        if (isLoaded)
        {
            value = value * multiplicatorVolume;

            audioMixer.SetFloat("Main", 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

            rsoSettingsSaved.Value.masterVolume = value;

            textAudioMain.text = $"{value}%";

            Save();
        }
    }

    public void UpdateAudioMusic(float value)
    {
        if (isLoaded)
        {
            value = value * multiplicatorVolume;

            audioMixer.SetFloat("Music", 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

            rsoSettingsSaved.Value.musicVolume = value;

            textAudioMusic.text = $"{value}%";

            Save();
        }
    }

    public void UpdateAudioSounds(float value)
    {
        if (isLoaded)
        {
            value = value * multiplicatorVolume;

            audioMixer.SetFloat("Sounds", 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

            rsoSettingsSaved.Value.soundsVolume = value;

            textAudioSounds.text = $"{value}%";

            Save();
        }
    }

    public void UpdateAudioUI(float value)
    {
        if (isLoaded)
        {
            value = value * multiplicatorVolume;

            audioMixer.SetFloat("UI", 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

            rsoSettingsSaved.Value.uiVolume = value;

            textAudioUI.text = $"{value}%";

            Save();
        }
    }

    private void Save()
    {
        rseSaveData.Call(saveSettingsName, true);
    }
}