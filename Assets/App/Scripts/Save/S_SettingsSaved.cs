using System;

[Serializable]
public class S_SettingsSaved
{
    public int languageIndex = 0;
    public bool fullScreen = true;
    public int resolutionIndex = 0;

    public float masterVolume = 100;
    public float musicVolume = 100;
    public float soundsVolume = 100;
    public float uiVolume = 100;
}