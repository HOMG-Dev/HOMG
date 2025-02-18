using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ������������
/// </summary>
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules;//���еĿ�����

    public ControllerManager()
    {
        _modules = new Dictionary<int, BaseController>();
    }

    //ע�������
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

    //ɾ��������
    public void Unregister(int controllerId)
    {
        if (_modules.ContainsKey(controllerId) == false)
        {
            Debug.LogError("ControllerManager Unregister Error: controllerId " + controllerId + " doesn't exist!");
            return;
        }
        _modules.Remove(controllerId);
    }

    //������п�����
    public void Clear()
    {
        _modules.Clear();
    }


    //������п��������ͷ���Դ
    public void ClearAllModules()
    {
        List<int> keys = _modules.Keys.ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            _modules[keys[i]].Destroy();
            _modules.Remove(keys[i]);
        }
    }

    //ִ�����г�ʼ�������
    public void InitAllModules()
    {
        foreach (var module in _modules)
        {
            module.Value.Init();
        }
    }


    //�����¼�
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

    //��ȡ��������ģ��
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

