using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        //初始化按钮
        InitBtn();
        //初始化数据
        InitData();
    }

    private void InitBtn()
    {
        Find<Button>("bg/closeBtn").onClick.AddListener(onCloseBtn);
        Find<Toggle>("bg/IsOpenSound").onValueChanged.AddListener(onIsOpenSound);
        Find<Slider>("bg/soundCount").onValueChanged.AddListener(onSoundCount);
        Find<Slider>("bg/effectCount").onValueChanged.AddListener(onEffectCount);
    }

    private void onCloseBtn()
    {
        ApplyFunc(EventDefine.CloseSettingView);
    }

    private void onIsOpenSound(bool value)
    {
        GameApp.SoundManager.IsOpenSound = !value;
    }

    private void onSoundCount(float value)
    {
        Debug.Log("onSoundCount:" + value);
        GameApp.SoundManager.BgmVolume = value;
    }

    private void onEffectCount(float value)
    {
        GameApp.SoundManager.EffectVolume = value;
    }

    // Ensure InitData is public to match the base class
    public override void InitData()
    {
        Find<Toggle>("bg/IsOpenSound").isOn = GameApp.SoundManager.IsOpenSound;
        Find<Slider>("bg/soundCount").value = GameApp.SoundManager.BgmVolume;
        Find<Slider>("bg/effectCount").value = GameApp.SoundManager.EffectVolume;
    }
}
