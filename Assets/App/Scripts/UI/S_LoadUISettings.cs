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

        StartCoroutine(S_Utils.Delay(0.1f, () => settings.SetIsLoaded()));
    }

    private void LoadLanguages()
    {
        languages = rsoSettingsSaved.Value.language;

        dropDownLanguages.value = languages;
    }

    private void LoadFullScreen()
    {
        fullscreen = rsoSettingsSaved.Value.fullScreen;

        toggleFullscreen.isOn = fullscreen;
    }

    private int GetResolutions()
    {
        Resolution[] resolutionsPC = Screen.resolutions;
        int currentResolutionIndex = 0;

        dropDownResolutions.ClearOptions();

        List<string> options = new();

        for (int i = 0; i < resolutionsPC.Length; i++)
        {
            Resolution res = resolutionsPC[i];
            string option = res.width + " x " + res.height;

            options.Add(option);

            if (res.width == Screen.width && res.height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        options = new(new HashSet<string>(options));

        dropDownResolutions.AddOptions(options);
        dropDownResolutions.RefreshShownValue();

        return currentResolutionIndex;
    }

    private void LoadResolutions()
    {
        resolutions = GetResolutions();

        dropDownResolutions.value = resolutions;
    }

    private void LoadAudioMain()
    {
        audioMain = rsoSettingsSaved.Value.audioMain;

        sliderAudioMain.value = audioMain / multiplicatorVolume;
        textAudioMain.text = $"{audioMain}%";
    }

    private void LoadAudioMusic()
    {
        audioMusic = rsoSettingsSaved.Value.audioMusic;

        sliderAudioMusic.value = audioMusic / multiplicatorVolume;
        textAudioMusic.text = $"{audioMusic}%";
    }

    private void LoadAudioSounds()
    {
        audioSounds = rsoSettingsSaved.Value.audioSounds;

        sliderAudioSounds.value = audioSounds / multiplicatorVolume;
        textAudioSounds.text = $"{audioSounds}%";

    }

    private void LoadAudioUI()
    {
        audioUI = rsoSettingsSaved.Value.audioUI;

        sliderAudioUI.value = audioUI / multiplicatorVolume;
        textAudioUI.text = $"{audioUI}%";
    }
}