using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͳһ���� ������ ���ڴ˴���ʼ�����й�����
/// </summary>
public class GameApp : Singleton<GameApp>
{

    public static SoundManager SoundManager;
    public static ControllerManager ControllerManager;
    public static ViewManager ViewManager;

    public override void Init()
    {
        SoundManager = new SoundManager();
        ControllerManager = new ControllerManager();
        ViewManager = new ViewManager();
    }


}
