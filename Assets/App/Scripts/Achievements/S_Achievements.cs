using UnityEngine;

public class S_Achievements : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, S_SaveName] private string saveAchievementsName;

    [Header("Input")]
    [SerializeField] private RSE_LoadAchievements rseLoadAchievements;
    [SerializeField] private RSE_UpdateAchievement rseUpdateAchievement;

    [Header("Output")]
    [SerializeField] private SSO_Achievements ssoAchievements;
    [SerializeField] private RSO_AchievementsSave rsoAchievementsSave;
    [SerializeField] private RSO_Achievements rsoAchievements;
    [SerializeField] private RSE_UpdateUIAchievement rseUpdateUIAchievement;
    [SerializeField] private RSE_ClearAchievement rseClearAchievement;
    [SerializeField] private RSE_SaveData rseSaveData;

    private void OnEnable()
    {
        rseLoadAchievements.action += LoadAchievements;
        rseUpdateAchievement.action += UpdateAchievement;
    }

    private void OnDisable()
    {
        rseLoadAchievements.action -= LoadAchievements;
        rseUpdateAchievement.action -= UpdateAchievement;
    }

    private void LoadAchievements()
    {
        foreach (var achievement in ssoAchievements.Value)
        {
            rsoAchievements.Value.Add(achievement.Clone());
            rsoAchievementsSave.Value.Add(achievement.ToSaveData());
        }

        SaveAchievements();
    }

    private void ValidAchievements(int id)
    {
        rsoAchievements.Value[id].unlocked = true;

        rseUpdateUIAchievement.Call(id);
        rseClearAchievement.Call(rsoAchievements.Value[id]);

        SaveStructAchievement(id);
    }

    private void SaveStructAchievement(int id)
    {
        S_StructAchievements achievement = rsoAchievementsSave.Value[id];

        achievement.unlocked = true;

        rsoAchievementsSave.Value[id] = achievement;
        SaveAchievements();
    }

    private void UpdateAchievement(int id, int value)
    {
        if (id >= 0 && id < rsoAchievements.Value.Count)
        {
            if (!rsoAchievements.Value[id].unlocked)
            {
                ValidAchievements(id);
            }
        }
    }

    private void SaveAchievements()
    {
        rseSaveData.Call(saveAchievementsName, false, true);
    }
}