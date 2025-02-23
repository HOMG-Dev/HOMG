using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 测试的地图视图
/// </summary>
public class MapView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        //初始化按钮
        InitBtn();
    }

    private void InitBtn()
    {
        Find<Button>("quitBtn").onClick.AddListener(onQuitBtn);
    }

    private void onQuitBtn()
    {
        //退出地图
        ApplyFunc(EventDefine.CloseMapView);
        //关闭地图 或者 禁用脚本的update
        //todo..
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenStartView);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            ApplyFunc(EventDefine.CameraMove, Vector3.forward * Time.deltaTime * ConstantDefine.CameraMoveSpeed);
        }
        if(Input.GetKey(KeyCode.S)) {
            ApplyFunc(EventDefine.CameraMove, Vector3.back * Time.deltaTime * ConstantDefine.CameraMoveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            ApplyFunc(EventDefine.CameraMove, Vector3.left * Time.deltaTime * ConstantDefine.CameraMoveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyFunc(EventDefine.CameraMove, Vector3.right * Time.deltaTime * ConstantDefine.CameraMoveSpeed);
        }
    }


}
