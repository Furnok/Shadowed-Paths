using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class S_LoadSettings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int multiplicatorVolume;

    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Input")]
    [SerializeField] private RSE_LoadSettings rseLoadSettings;

    [Header("Output")]
    [SerializeField] private RSO_SettingsSaved rsoSettingsSaved;

    private void OnEnable()
    {
        rseLoadSettings.action += Load;
    }

    private void OnDisable()
    {
        rseLoadSettings.action -= Load;
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

    private void Load()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[rsoSettingsSaved.Value.language];

        Resolution resolution = GetResolutions(rsoSettingsSaved.Value.resolutions);

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);

        if (rsoSettingsSaved.Value.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        Screen.fullScreen = rsoSettingsSaved.Value.fullScreen;

        audioMixer.SetFloat("Main", 40 * Mathf.Log10(Mathf.Max(rsoSettingsSaved.Value.audioMain, 1) / 100));

        audioMixer.SetFloat("Music", 40 * Mathf.Log10(Mathf.Max(rsoSettingsSaved.Value.audioMain, 1) / 100));

        audioMixer.SetFloat("Sounds", 40 * Mathf.Log10(Mathf.Max(rsoSettingsSaved.Value.audioMain, 1) / 100));

        audioMixer.SetFloat("UI", 40 * Mathf.Log10(Mathf.Max(rsoSettingsSaved.Value.audioMain, 1) / 100));
    }
}