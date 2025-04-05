using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
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


    private Resolution GetResolutions()
    {
        Resolution[] resolutionsPC = Screen.resolutions;
        Resolution resolution = resolutionsPC[0];

        List<string> options = new();

        for (int i = 0; i < resolutionsPC.Length; i++)
        {
            Resolution res = resolutionsPC[i];
            string option = res.width + " x " + res.height;

            options.Add(option);

            if (res.width == Screen.width && res.height == Screen.height)
            {
                resolution = res;
            }
        }

        options = new(new HashSet<string>(options));

        return resolution;
    }

    private void Load()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[rsoSettingsSaved.Value.language];

        if (rsoSettingsSaved.Value.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        Screen.fullScreen = rsoSettingsSaved.Value.fullScreen;

        Resolution resolution = GetResolutions();

        Screen.SetResolution(resolution.width, resolution.height, rsoSettingsSaved.Value.fullScreen);

        audioMixer.SetFloat("Main", 40 * Mathf.Log10(Mathf.Max(rsoSettingsSaved.Value.audioMain * multiplicatorVolume, 1) / 100));

        audioMixer.SetFloat("Music", 40 * Mathf.Log10(Mathf.Max(rsoSettingsSaved.Value.audioMain * multiplicatorVolume, 1) / 100));

        audioMixer.SetFloat("Sounds", 40 * Mathf.Log10(Mathf.Max(rsoSettingsSaved.Value.audioMain * multiplicatorVolume, 1) / 100));

        audioMixer.SetFloat("UI", 40 * Mathf.Log10(Mathf.Max(rsoSettingsSaved.Value.audioMain * multiplicatorVolume, 1) / 100));
    }
}