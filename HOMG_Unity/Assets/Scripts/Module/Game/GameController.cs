using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ȫ����Ϸ��������������Ϸ�����߼�
/// </summary>
public class GameController : BaseController
{
    public GameController() : base()
    {

        //ע���¼�
        InitModelEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        base.Init();

        //�򿪿�ʼ����
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenStartView);
    }
}
