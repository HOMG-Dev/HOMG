using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始游戏
/// </summary>
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        //初始化按钮
        InitBtn();
    }


    //初始化按钮
    private void InitBtn()
    {
        Find<Button>("startBtn").onClick.AddListener(onStartBtn);
        Find<Button>("setBtn").onClick.AddListener(onSetBtn);
        Find<Button>("quitBtn").onClick.AddListener(onQuitBtn);
    }

    //按钮回调函数
    private void onStartBtn()
    {
        //ApplyFunc(EventDefine.OpenMapView);
        ApplyFunc(EventDefine.CloseStartView);

        LoadingModel loadingModel = new LoadingModel();
        loadingModel.SceneName = "Map";
        Controller.ApplyControllerFunc(ControllerType.Loading, EventDefine.LoadingScene, loadingModel);
    }

    private void onSetBtn()
    {
        ApplyFunc(EventDefine.OpenSettingView);
    }

    private void onQuitBtn()
    {
        //退出游戏
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
