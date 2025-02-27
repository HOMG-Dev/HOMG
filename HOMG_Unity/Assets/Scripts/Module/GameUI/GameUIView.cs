using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        //初始化按钮
        InitBtn();
    }

    private void InitBtn()
    {
        //Find<Button>("bg/settingBtn").onClick.AddListener(onSettingBtn);
    }
}
