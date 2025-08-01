using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_LoadUISettings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int multiplicatorVolume;

    [Header("References")]
    [SerializeField] private S_Settings settings;
    [SerializeField] private TMP_Dropdown dropDownLanguages;
    [SerializeField] private Toggle toggleFullscreen;
    [SerializeField] private TMP_Dropdown dropDownResolutions;
    [SerializeField] private Slider sliderAudioMain;
    [SerializeField] private Slider sliderAudioMusic;
    [SerializeField] private Slider sliderAudioSounds;
    [SerializeField] private Slider sliderAudioUI;
    [SerializeField] private TextMeshProUGUI textAudioMain;
    [SerializeField] private TextMeshProUGUI textAudioMusic;
    [SerializeField] private TextMeshProUGUI textAudioSounds;
    [SerializeField] private TextMeshProUGUI textAudioUI;

    [Header("Output")]
    [SerializeField] private RSO_SettingsSaved rsoSettingsSaved;

    private int languages;
    private bool fullscreen;
    private int resolutions;
    private float audioMain;
    private float audioMusic;
    private float audioSounds;
    private float audioUI;

    private void OnEnable()
    {
        LoadUI();
    }

    private void LoadUI()
    {
        LoadLanguages();

        LoadFullScreen();

        LoadResolutions();

        LoadAudioMain();

        LoadAudioMusic();

        LoadAudioSounds();

        LoadAudioUI();

        StartCoroutine(S_Utils.DelayFrame(() => settings.SetIsLoaded()));
    }

    private void LoadLanguages()
    {
        languages = rsoSettingsSaved.Value.languageIndex;

        dropDownLanguages.value = languages;
    }

    private void LoadFullScreen()
    {
        fullscreen = rsoSettingsSaved.Value.fullScreen;

        toggleFullscreen.isOn = fullscreen;
    }

    private int GetResolutions(int index)
    {
        List<Resolution> resolutionsPC = new(Screen.resolutions);
        resolutionsPC.Reverse();

        int currentResolutionIndex = 0;

        dropDownResolutions.ClearOptions();

        List<string> options = new();

        for (int i = 0; i < resolutionsPC.Count; i++)
        {
            Resolution res = resolutionsPC[i];
            string option = $"{res.width}x{res.height} {res.refreshRateRatio}Hz";

            options.Add(option);

            if (i == index)
            {
                currentResolutionIndex = i;
            }
        }

        dropDownResolutions.AddOptions(options);
        dropDownResolutions.RefreshShownValue();

        return currentResolutionIndex;
    }

    private void LoadResolutions()
    {
        resolutions = GetResolutions(rsoSettingsSaved.Value.resolutionIndex);

        dropDownResolutions.value = resolutions;
    }

    private void LoadAudioMain()
    {
        audioMain = rsoSettingsSaved.Value.masterVolume;

        sliderAudioMain.value = audioMain / multiplicatorVolume;
        textAudioMain.text = $"{audioMain}%";
    }

    private void LoadAudioMusic()
    {
        audioMusic = rsoSettingsSaved.Value.musicVolume;

        sliderAudioMusic.value = audioMusic / multiplicatorVolume;
        textAudioMusic.text = $"{audioMusic}%";
    }

    private void LoadAudioSounds()
    {
        audioSounds = rsoSettingsSaved.Value.soundsVolume;

        sliderAudioSounds.value = audioSounds / multiplicatorVolume;
        textAudioSounds.text = $"{audioSounds}%";

    }

    private void LoadAudioUI()
    {
        audioUI = rsoSettingsSaved.Value.uiVolume;

        sliderAudioUI.value = audioUI / multiplicatorVolume;
        textAudioUI.text = $"{audioUI}%";
    }
}