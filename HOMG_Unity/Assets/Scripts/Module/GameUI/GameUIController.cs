using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ͨ��UI������
/// ��ʼ��Ϸ ���� ��ʾ
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {

        //��ʼ��ͼ
        GameApp.ViewManager.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 0,
        } );


        //��ʼ���¼�
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
