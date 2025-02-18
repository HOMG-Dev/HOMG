using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// …˘“Ùπ‹¿Ì∆˜
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource;//±≥æ∞“Ù¿÷

    private Dictionary<string, AudioClip> clips;//cache

    public SoundManager()
    {
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        bgmSource.loop = true;
        clips = new Dictionary<string, AudioClip>();
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlayBGM(string clipName)
    {
        if (clips.ContainsKey(clipName) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{clipName}");
            clips.Add(clipName, clip);
        }
        bgmSource.clip = clips[clipName];
        bgmSource.Play();
    }
}
