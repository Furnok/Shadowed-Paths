using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class S_Settings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int multiplicatorVolume;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textAudioMain;
    [SerializeField] private TextMeshProUGUI textAudioMusic;
    [SerializeField] private TextMeshProUGUI textAudioSounds;
    [SerializeField] private TextMeshProUGUI textAudioUI;
    [SerializeField] private AudioMixer audioMixer;

    [Header("Input")]
    [SerializeField] private RSO_SettingsSaved rsoSettingsSaved;

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

            rsoSettingsSaved.Value.language = index;
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
        }
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

    public void UpdateResolutions(int index)
    {
        if (isLoaded)
        {
            Resolution resolution = GetResolutions();

            Screen.SetResolution(resolution.width, resolution.height, rsoSettingsSaved.Value.fullScreen);

            rsoSettingsSaved.Value.resolutions = index;
        }
    }

    public void UpdateAudioMain(float value)
    {
        if (isLoaded)
        {
            value = value * multiplicatorVolume;

            audioMixer.SetFloat("Main", 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

            rsoSettingsSaved.Value.audioMain = value;
        }
    }

    public void UpdateAudioMusic(float value)
    {
        if (isLoaded)
        {
            value = value * multiplicatorVolume;

            audioMixer.SetFloat("Music", 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

            rsoSettingsSaved.Value.audioMusic = value;
        }
    }

    public void UpdateAudioSounds(float value)
    {
        if (isLoaded)
        {
            value = value * multiplicatorVolume;

            audioMixer.SetFloat("Sounds", 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

            rsoSettingsSaved.Value.audioSounds = value;
        }
    }

    public void UpdateAudioUI(float value)
    {
        if (isLoaded)
        {
            value = value * multiplicatorVolume;

            audioMixer.SetFloat("UI", 40 * Mathf.Log10(Mathf.Max(value, 1) / 100));

            rsoSettingsSaved.Value.audioUI = value;
        }
    }
}