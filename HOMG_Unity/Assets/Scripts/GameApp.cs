using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 统一定义 管理器 ，在此处初始化所有管理器
/// </summary>
public class GameApp : Singleton<GameApp>
{

    public static SoundManager SoundManager;

    public override void Init()
    {
        SoundManager = new SoundManager();
    }


}
