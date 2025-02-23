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

    private bool isLoaded = false;


    private void Awake()
    {
        if (isLoaded == true)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            isLoaded = true;
            GameApp.Instance.Init();
        }
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
        GameApp.ControllerManager.Register(ControllerType.Loading, new LoadingController());
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
