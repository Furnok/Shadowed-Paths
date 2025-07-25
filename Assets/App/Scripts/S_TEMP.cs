using UnityEngine;

public class S_TEMP : MonoBehaviour
{
    [Header("Output")]
    [SerializeField] private RSE_UpdateAchievement rseUpdateAchievement;

    public void UpdateAchievement(int id)
    {
        rseUpdateAchievement.Call(id, 100000);
    }
}