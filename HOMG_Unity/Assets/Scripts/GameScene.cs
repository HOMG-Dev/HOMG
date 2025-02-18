using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 继承自Mono，挂在场景中的空物体上，用于初始化整个游戏
/// </summary>
public class GameScene : MonoBehaviour
{

    public Texture2D mouseTxt;//鼠标贴图

    float deltaTime = 0.0f;


    private void Awake()
    {
        GameApp.Instance.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        //设置鼠标贴图
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);
        //播放BGM
        GameApp.SoundManager.PlayBGM("axistheme");


        //注册控制器（模块）
        RegisterModule();

        //初始化所有模块
        InitModules();
    }

    void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
    }

    void InitModules()
    {
        GameApp.ControllerManager.InitAllModules();
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;
        GameApp.Instance.Update(deltaTime);
    }
}
