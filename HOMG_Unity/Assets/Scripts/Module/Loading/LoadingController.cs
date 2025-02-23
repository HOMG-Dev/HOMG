using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 场景加载控制器
/// </summary>
public class LoadingController : BaseController
{
    AsyncOperation asyncOperation;

    public LoadingController() : base()
    {
        GameApp.ViewManager.Register(ViewType.LoadingView, new ViewInfo()
        {
            PrefabName = "LoadingView",
            controller = this,
            parentTf = GameApp.ViewManager.canvasTf,
        });

        //初始化事件
        InitModelEvent();
        InitGlobalEvent();
    }

    public override void InitModelEvent()
    {
        base.InitModelEvent();
        RegisterFunc(EventDefine.LoadingScene, loadSceneCallback);
    }

    private void loadSceneCallback(System.Object[] args)
    {
        LoadingModel loadingModel = args[0] as LoadingModel;

        SetModel(loadingModel);

        //打开加载视图
        GameApp.ViewManager.Open(ViewType.LoadingView);

        //加载场景
        asyncOperation = SceneManager.LoadSceneAsync(loadingModel.SceneName);
        asyncOperation.completed += onLoadedEndCallBack;
    }

    private void onLoadedEndCallBack(AsyncOperation ao)
    {
        asyncOperation.completed -= onLoadedEndCallBack;

        GetModel<LoadingModel>().callback?.Invoke();

        //关闭加载视图
        GameApp.ViewManager.Close(ViewType.LoadingView);
        //打开地图视图
        //GameApp.ViewManager.Open(ViewType.MapView);
        ApplyControllerFunc(ControllerType.Game, EventDefine.OpenMapView);
    }

}
