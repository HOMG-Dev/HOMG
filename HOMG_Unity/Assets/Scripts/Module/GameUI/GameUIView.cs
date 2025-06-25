using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : BaseView
{
    private bool _isOpeningAvatar = false;

    protected override void OnAwake()
    {
        base.OnAwake();

        //初始化按钮
        InitBtn();
    }

    private void onAvatarView()
    {
        if (_isOpeningAvatar == true)
        {
            closeAvatarView();
            _isOpeningAvatar = false;
        }
        else
        {
            openAvatarView();
            _isOpeningAvatar = true;
        }
    }

    private void openAvatarView()
    {
        //打开头像视图
        ApplyFunc(EventDefine.OpenAvatarView);
    }

    private void closeAvatarView()
    {
        //打开头像视图
        ApplyFunc(EventDefine.CloseAvatarView);
    }

    private void InitBtn()
    {
        //Find<Button>("bg/settingBtn").onClick.AddListener(onSettingBtn);
        Find<Button>("avatar").onClick.AddListener(onAvatarView);
    }
}
