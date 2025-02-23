using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局游戏控制器，处理游戏整体逻辑
/// </summary>
public class GameController : BaseController
{
    public GameController() : base()
    {

        //注册事件
        InitModelEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        base.Init();

        //测试地图视图
        GameApp.ViewManager.Register(ViewType.MapView, new ViewInfo()
        {
            PrefabName = "MapView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 1,
        });

        //打开开始界面
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenStartView);
    }

    public override void InitModelEvent()
    {
        base.InitModelEvent();

        //打开地图界面
        RegisterFunc(EventDefine.OpenMapView, OpenMapView);
        RegisterFunc(EventDefine.CloseMapView, CloseMapView);

        //摄像头移动
        RegisterFunc(EventDefine.CameraMove, CameraMove);
    }

    private void OpenMapView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.MapView, args);
    }

    private void CloseMapView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.MapView, args);
    }

    private void CameraMove(System.Object[] args)
    {
        Vector3 direction = (Vector3)args[0];
        GameObject.Find("Map Camera").transform.position += direction;
    }
}
