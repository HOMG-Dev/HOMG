using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 视图接口
/// </summary>
public interface IBaseView
{
    bool IsInit();//是否初始化
    bool IsShow();//是否显示

    void InitUI();//初始化视图

    void InitData();//初始化数据

    void Open(params object[] args);//打开视图

    void Close(params object[] args);//关闭视图

    void DestroyView();//销毁视图

    void ApplyFunc(string eventName, params object[] args);//触发自身事件

    void ApplyControllerFunc(int controllerId, string eventName, params object[] args);//触发控制器事件

    void SetVisable(bool isVisable);//设置显示隐藏

    int ViewId { get; set ; }//视图ID

    BaseController Controller { get ; set ; }//视图所属控制器

}
