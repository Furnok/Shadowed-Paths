using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class S_ClassAudio
{
    public AudioClip clip = null;
    public AudioMixerGroup mixerGroup = null;
    public bool fade = false;
    public bool loop = false;
}