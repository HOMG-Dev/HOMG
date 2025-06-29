using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局游戏控制器，处理游戏整体逻辑
/// </summary>
public class GameController : BaseController
{
    public MapRender mapRender;

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


        //对局内设置视图
        GameApp.ViewManager.Register(ViewType.InGameSettingView, new ViewInfo()
        {
            PrefabName = "InGameSettingView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 999,
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

        //游戏设置界面
        RegisterFunc(EventDefine.OpenInGameSettingView, OpenInGameSettingView);
        RegisterFunc(EventDefine.CloseInGameSettingView, CloseInGameSettingView);

        //退出对局
        RegisterFunc(EventDefine.QuitGame, QuitGame);

        //Render事件
        RegisterFunc(EventDefine.ClickCell, ClickCell);//点击地图格子事件

    }

    private void OpenMapView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.MapView, args);

        mapRender = new MapRender(GetModel<MapModel>());
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

    private void OpenInGameSettingView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.InGameSettingView, args);
        GameApp.ViewManager.Pause(ViewType.MapView);
    }

    private void CloseInGameSettingView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.InGameSettingView, args);
        GameApp.ViewManager.Resume(ViewType.MapView);
    }

    private void QuitGame(System.Object[] args)
    {
        //退出地图
        ApplyFunc(EventDefine.CloseMapView);

        //关闭地图 或者 禁用脚本的update
        //todo..
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenStartView);
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.CloseAvatarView);
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.CloseGameUIView);
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.CloseCellLandformView);
    }

    private void ClickCell(System.Object[] args)
    {
        CellBehavior cell = args[0] as CellBehavior;
        if (cell == null)
        {
            Debug.LogError("ClickCell: CellBehavior is null");
            return;
        }
        CellPos cellPos = cell.cellPos;
        mapRender.ClickCell(cellPos);
    }

}
