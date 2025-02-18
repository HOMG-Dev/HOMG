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


        //初始化事件
        InitModelEvent();
        InitGlobalEvent();
    }

    public override void InitModelEvent()
    {
        base.InitModelEvent();
        RegisterFunc(EventDefine.OpenStartView, OpenStartView);
    }

    public override void InitGlobalEvent()
    {
        base.InitGlobalEvent();
    }

    private void OpenStartView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.StartView, args);
    }
}
