using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声音管理器
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource;//背景音乐

    private Dictionary<string, AudioClip> clips;//cache

    private bool isOpenSound = true;//是否开启声音

    public bool IsOpenSound
    {
        get { return isOpenSound; }
        set
        {
            isOpenSound = value;
            if (isOpenSound == false)
            {
                bgmSource.Stop();
            }
            else
            {
                bgmSource.Play();
            }
        }
    }

    private float bgmVolume = 1;//音量

    public float BgmVolume
    {
        get { return bgmVolume; }
        set
        {
            bgmVolume = value;
            bgmSource.volume = bgmVolume;
        }
    }

    private float effectVolume = 1;//音效音量
    public float EffectVolume
    {
        get { return effectVolume; }
        set
        {
            effectVolume = value;
            //todo..
        }
    }

    public SoundManager()
    {
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        bgmSource.loop = true;
        clips = new Dictionary<string, AudioClip>();
    }

    public void PlayBGM(AudioClip clip)
    {
        if (isOpenSound == false) return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlayBGM(string clipName)
    {
        if (isOpenSound == false) return;
        if (clips.ContainsKey(clipName) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{clipName}");
            clips.Add(clipName, clip);
        }
        bgmSource.clip = clips[clipName];
        bgmSource.Play();
    }
}
