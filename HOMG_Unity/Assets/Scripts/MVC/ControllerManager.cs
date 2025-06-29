using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 控制器管理器
/// </summary>
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules;//所有的控制器

    public ControllerManager()
    {
        _modules = new Dictionary<int, BaseController>();
    }

    //注册控制器
    public void Register(ControllerType type, BaseController controller)
    {
        Register((int)type, controller);
    }

    public void Register(int controllerId, BaseController controller)
    {
        if (_modules.ContainsKey(controllerId) == true)
        {
            Debug.LogError("ControllerManager Register Error: controllerId " + controllerId + " is already exist!");
            return;
        }
        _modules.Add(controllerId, controller);
    }

    //获取控制器
    public BaseController GetController(ControllerType type)
    {
        return GetController((int)type);
    }
    public BaseController GetController(int controllerId)
    {
        if (_modules.ContainsKey(controllerId) == true)
        {
            return _modules[controllerId];
        }
        else
        {
            Debug.LogError("ControllerManager GetController Error: controllerId " + controllerId + " doesn't exist!");
            return null;
        }
    }

    //删除控制器
    public void Unregister(int controllerId)
    {
        if (_modules.ContainsKey(controllerId) == false)
        {
            Debug.LogError("ControllerManager Unregister Error: controllerId " + controllerId + " doesn't exist!");
            return;
        }
        _modules.Remove(controllerId);
    }

    //清空所有控制器
    public void Clear()
    {
        _modules.Clear();
    }


    //清空所有控制器并释放资源
    public void ClearAllModules()
    {
        List<int> keys = _modules.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            _modules[keys[i]].Destroy();
            _modules.Remove(keys[i]);
        }
    }

    //执行所有初始化后调用
    public void InitAllModules()
    {
        foreach (var module in _modules)
        {
            module.Value.Init();
        }
    }


    //触发事件
    public void ApplyFunc(int controllerId, string eventName, params object[] args)
    {
        if (_modules.ContainsKey(controllerId) == true)
        {
            _modules[controllerId].ApplyFunc(eventName, args);
        }
        else
        {
            Debug.LogError("ControllerManager ApplyFunc Error: controllerId " + controllerId + " doesn't exist.");
        }
    }

    //获取控制器的模型
    public T GetModel<T>(ControllerType type) where T : BaseModel
    {
        return (T)GetModel((int)type);
    }
    public BaseModel GetModel(ControllerType type)
    {
        return GetModel((int)type);
    }
    public BaseModel GetModel(int controllerId)
    {
        if (_modules.ContainsKey(controllerId) == true)
        {
            return _modules[controllerId].GetModel();
        }
        else
        {
            Debug.LogError("ControllerManager GetModel Error: controllerId " + controllerId + " doesn't exist.");
            return null;
        }
    }




}

