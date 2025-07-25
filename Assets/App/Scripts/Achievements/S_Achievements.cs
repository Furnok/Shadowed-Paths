using UnityEngine;

public class S_Achievements : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RSE_UpdateAchievement rseUpdateAchievement;

    [Header("Output")]
    [SerializeField] private SSO_Achievements ssoAchievements;
    [SerializeField] private RSO_Achievements rsoAchievements;
    [SerializeField] private RSE_UpdateUIAchievement rseUpdateUIAchievement;
    [SerializeField] private RSE_ClearAchievement rseClearAchievement;

    private void OnEnable()
    {
        rseUpdateAchievement.action += UpdateAchievement;
    }

    private void OnDisable()
    {
        rsoAchievements.Value = null;

        rseUpdateAchievement.action -= UpdateAchievement;
    }

    private void Start()
    {
        LoadAchievements();
    }

    private void LoadAchievements()
    {
        rsoAchievements.Value = new();

        foreach (var achievement in ssoAchievements.Value)
        {
            rsoAchievements.Value.Add(achievement.Clone());
        }
    }

    private void ValidAchievements(int id)
    {
        rsoAchievements.Value[id].unlocked = true;

        rseUpdateUIAchievement.Call(id);
        rseClearAchievement.Call(rsoAchievements.Value[id]);
    }

    private void UpdateAchievement(int id, int value)
    {
        if (id >= 0 && id < rsoAchievements.Value.Count)
        {
            if (!rsoAchievements.Value[id].unlocked)
            {
                rsoAchievements.Value[id].progress += value;

                if (rsoAchievements.Value[id].progress >= rsoAchievements.Value[id].objective)
                {
                    rsoAchievements.Value[id].progress = rsoAchievements.Value[id].objective;

                    ValidAchievements(id);
                }
            }
        }
    }
}