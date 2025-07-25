using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_UIAchievementClear : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float displayDuration;
    [SerializeField] private float delayBetweenAchievements;

    [Header("References")]
    [SerializeField] private GameObject content;
    [SerializeField] private Animator animator;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    [Header("Input")]
    [SerializeField] private RSE_ClearAchievement rseClearAchievement;

    private Queue<S_ClassAchievements> achievementsQueue = new();
    private bool isDisplaying = false;

    private void OnEnable()
    {
        rseClearAchievement.action += QueueAchievement;
    }

    private void OnDisable()
    {
        rseClearAchievement.action -= QueueAchievement;
    }

    private void QueueAchievement(S_ClassAchievements achievement)
    {
        achievementsQueue.Enqueue(achievement);

        if (!isDisplaying)
        {
            StartCoroutine(DisplayNextAchievement());
        }
    }

    private IEnumerator DisplayNextAchievement()
    {
        while (achievementsQueue.Count > 0)
        {
            isDisplaying = true;
            animator.SetBool("Show", true);

            S_ClassAchievements achievement = achievementsQueue.Dequeue();

            ShowAchievement(achievement);

            content.SetActive(true);

            yield return new WaitForSeconds(displayDuration);

            content.SetActive(false);
            animator.SetBool("Show", false);

            yield return new WaitForSeconds(delayBetweenAchievements);
        }

        isDisplaying = false;
    }

    private void ShowAchievement(S_ClassAchievements achievement)
    {
        image.sprite = achievement.image;
        title.text = achievement.title.GetLocalizedString();
        description.text = achievement.description.GetLocalizedString();
    }
}