using UnityEngine;

public class S_Achievements : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Header("Input")]

    [Header("Output")]
    [SerializeField] private SSO_Achievements ssoAchievements;
    [SerializeField] private RSO_Achievements rsoAchievements;

    private void OnDisable()
    {
        rsoAchievements.Value = null;
    }

    private void Start()
    {
        rsoAchievements.Value = new();

        foreach (var achievement in ssoAchievements.Value)
        {
            rsoAchievements.Value.Add(achievement.Clone());
        }
    }
}