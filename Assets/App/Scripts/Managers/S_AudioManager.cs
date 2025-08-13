using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class S_AudioManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float timeFadeInAudio;
    [SerializeField] private float timeFadeOutAudio;
    [SerializeField, Range(0f, 1f)] private float volumeMax;
    [SerializeField] private GameObject audioSourcePrefab;

    [Header("Input")]
    [SerializeField] private RSE_PlayAudio rsePlayAudio;
    [SerializeField] private RSE_StopAllAudio rseStopAllAudio;

    private List<AudioSource> activeAudioSources = new();

    private void OnEnable()
    {
        rsePlayAudio.action += PlayAudio;
        rseStopAllAudio.action += StopAllAudios;
    }

    private void OnDisable()
    {
        rsePlayAudio.action -= PlayAudio;
        rseStopAllAudio.action -= StopAllAudios;
    }

    private void PlayAudio(S_ClassAudio classAudio)
    {
        GameObject audioObj = Instantiate(audioSourcePrefab, gameObject.transform);
        audioObj.transform.SetParent(gameObject.transform);
        audioObj.name = $"Audio_{classAudio.clip.name}";

        AudioSource audioSource = audioObj.GetComponent<AudioSource>();
        audioSource.clip = classAudio.clip;
        audioSource.outputAudioMixerGroup = classAudio.mixerGroup;
        audioSource.loop = classAudio.loop;

        S_AudioSource script = audioObj.GetComponent<S_AudioSource>();
        script.Setup(this);

        if (classAudio.fade)
        {
            FadeInAudio(audioSource);
        }
        else
        {
            audioSource.volume = volumeMax;

            audioSource.Play();
        }

        activeAudioSources.Add(audioSource);
    }

    private void FadeInAudio(AudioSource audioSource)
    {
        audioSource.volume = 0;

        audioSource.Play();

        audioSource.DOFade(volumeMax, timeFadeInAudio).SetEase(Ease.Linear).SetLink(audioSource.gameObject);
    }

    private void FadeOutAudio(AudioSource audioSource)
    {
        audioSource.DOFade(0, timeFadeOutAudio).SetEase(Ease.Linear).SetLink(audioSource.gameObject).OnComplete(() => StopAudio(audioSource));
    }

    private void StopAudio(AudioSource audioSource)
    {
        audioSource.Stop();

        UpdateList(audioSource);
    }

    public void UpdateList(AudioSource audioSource)
    {
        activeAudioSources.Remove(audioSource);
    }

    private void StopAllAudios()
    {
        foreach (AudioSource audioSource in activeAudioSources)
        {
            if (audioSource != null)
            {
                FadeOutAudio(audioSource);
            }
        }
    }
}