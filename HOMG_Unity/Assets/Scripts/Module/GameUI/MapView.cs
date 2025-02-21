using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 测试的地图视图
/// </summary>
public class MapView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        //初始化按钮
        InitBtn();
    }

    private void InitBtn()
    {
        Find<Button>("quitBtn").onClick.AddListener(onQuitBtn);
    }

    private void onQuitBtn()
    {
        ApplyFunc(EventDefine.CloseMapView);
        ApplyFunc(EventDefine.OpenStartView);
    }
}
