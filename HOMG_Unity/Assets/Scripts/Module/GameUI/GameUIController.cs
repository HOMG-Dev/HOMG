using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 通用UI控制器
/// 开始游戏 设置 提示
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {

        //开始视图
        GameApp.ViewManager.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 0,
        } );

        //设置视图
        GameApp.ViewManager.Register(ViewType.SettingView, new ViewInfo()
        {
            PrefabName = "SettingView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 999,
        });

        //对局内UI视图
        GameApp.ViewManager.Register(ViewType.GameUIView, new ViewInfo()
        {
            PrefabName = "GameUIView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 888,
        });

        //头像视图
        GameApp.ViewManager.Register(ViewType.AvatarView, new ViewInfo()
        {
            PrefabName = "AvatarView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 777,
        });

        //地形视图
        GameApp.ViewManager.Register(ViewType.CellLandformView, new ViewInfo()
        {
            PrefabName = "CellLandformView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 777,
        });

        //初始化事件
        InitModelEvent();
        InitGlobalEvent();
    }

    public override void InitModelEvent()
    {
        base.InitModelEvent();
        RegisterFunc(EventDefine.OpenStartView, OpenStartView);
        RegisterFunc(EventDefine.CloseStartView, CloseStartView);

        RegisterFunc(EventDefine.OpenGameUIView, OpenGameUIView);
        RegisterFunc(EventDefine.CloseGameUIView, CloseGameUIView);

        RegisterFunc(EventDefine.OpenSettingView, OpenSettingView);
        RegisterFunc(EventDefine.CloseSettingView, CloseSettingView);

        //在这里注册新的函数
        RegisterFunc(EventDefine.OpenAvatarView, OpenAvatarView);
        RegisterFunc(EventDefine.CloseAvatarView, CloseAvatarView);

        RegisterFunc(EventDefine.CloseCellLandformView, CloseCellLandformView);
        RegisterFunc(EventDefine.OpenCellLandformView, OpenCellLandformView);
    }

    public override void InitGlobalEvent()
    {
        base.InitGlobalEvent();
    }

    private void OpenAvatarView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.AvatarView, args);
    }

    private void CloseAvatarView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.AvatarView, args);
    }

    private void OpenStartView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.StartView, args);
    }
    private void CloseStartView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.StartView, args);
    }


    private void OpenSettingView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.SettingView, args);
        //GameApp.ViewManager.Open(ViewType.GameUIView, args);
    }
    private void CloseSettingView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.SettingView, args);
    }

    private void OpenGameUIView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.GameUIView, args);
    }

    private void CloseGameUIView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.GameUIView, args);
    }

    private void OpenCellLandformView(System.Object[] args)
    {
        if (GameApp.ViewManager.IsViewOpened(ViewType.CellLandformView))
        {
            // 如果视图已经打开，则关闭它
            GameApp.ViewManager.Close(ViewType.CellLandformView, null);
        }
        GameApp.ViewManager.Open(ViewType.CellLandformView, args);
    }

    private void CloseCellLandformView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.CellLandformView, args);
    }
}
