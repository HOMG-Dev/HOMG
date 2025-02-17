using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制器基类
/// </summary>
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message;//事件字典
    protected BaseModel model;//模型数据

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }

    public virtual void OnLoadView(IBaseView view) { }//加载视图

    public virtual void OnDestoryView(IBaseView view) { }//销毁视图


    //打开视图
    public virtual void OpenView(IBaseView view) 
    {
    
    }

    //关闭视图
    public virtual void CloseView(IBaseView view)
    {

    }

    //注册模板事件
    public void RegisterFunc(string eventName, System.Action<object[]> func)
    {
        if (!message.ContainsKey(eventName))
        {
            message.Add(eventName, null);
        }
        message[eventName] += func;
    }

    //删除模板事件
    public void UnRegisterFunc(string eventName, System.Action<object[]> func)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName] -= func;
        }
    }

    //触发本模块事件
    public void ApplyFunc(string eventName, params object[] args)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName]?.Invoke(args);
        } 
        else
        {
            Debug.LogError("事件" + eventName + "不存在");
        }
    }

    //触发其他模块事件
    public void ApplyControllerFunc(int controllerId, string eventName, params object[] args)
    {
        //BaseController controller = ControllerManager.Instance.GetController(controllerId);
        //todo..
    }

    //设置模型
    public void SetModel(BaseModel model)
    {
        this.model = model;
    }

    //获取模型
    public BaseModel GetModel()
    {
        return model;
    }
    public T GetModel<T>() where T : BaseModel
    {
        return (T)model;
    }

    //获取其他模块的模型
    public BaseModel GetModel(int controllerId)
    {
        //BaseController controller = ControllerManager.Instance.GetController(modelId);
        //return controller.GetModel();
        //todo..
        return null;
    }

    //销毁控制器
    public virtual void Destroy()
    {
        //todo..
    }

    //初始化模板事件
    public virtual void InitModelEvent()
    {
        //todo..
    }

    //删除模板事件
    public virtual void RemoveModelEvent()
    {
        //todo..
    }

    //初始化全局事件
    public virtual void InitGlobalEvent()
    {
        //todo..
    }

    //删除全局事件
    public virtual void RemoveGlobalEvent()
    {
        //todo..
    }



}
