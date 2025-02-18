using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̳���Mono�����ڳ����еĿ������ϣ����ڳ�ʼ��������Ϸ
/// </summary>
public class GameScene : MonoBehaviour
{

    public Texture2D mouseTxt;//�����ͼ

    float deltaTime = 0.0f;


    private void Awake()
    {
        GameApp.Instance.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        //���������ͼ
        Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);
        //����BGM
        GameApp.SoundManager.PlayBGM("axistheme");


        //ע���������ģ�飩
        RegisterModule();

        //��ʼ������ģ��
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
