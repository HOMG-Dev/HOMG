using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message;//�¼��ֵ�
    protected BaseModel model;//ģ������

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }

    public virtual void OnLoadView(IBaseView view) { }//������ͼ

    public virtual void OnDestoryView(IBaseView view) { }//������ͼ


    //����ͼ
    public virtual void OpenView(IBaseView view) 
    {
    
    }

    //�ر���ͼ
    public virtual void CloseView(IBaseView view)
    {

    }

    //ע��ģ���¼�
    public void RegisterFunc(string eventName, System.Action<object[]> func)
    {
        if (!message.ContainsKey(eventName))
        {
            message.Add(eventName, null);
        }
        message[eventName] += func;
    }

    //ɾ��ģ���¼�
    public void UnRegisterFunc(string eventName, System.Action<object[]> func)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName] -= func;
        }
    }

    //������ģ���¼�
    public void ApplyFunc(string eventName, params object[] args)
    {
        if (message.ContainsKey(eventName))
        {
            message[eventName]?.Invoke(args);
        } 
        else
        {
            Debug.LogError("�¼�" + eventName + "������");
        }
    }

    //��������ģ���¼�
    public void ApplyControllerFunc(int controllerId, string eventName, params object[] args)
    {
        //BaseController controller = ControllerManager.Instance.GetController(controllerId);
        //todo..
    }

    //����ģ��
    public void SetModel(BaseModel model)
    {
        this.model = model;
    }

    //��ȡģ��
    public BaseModel GetModel()
    {
        return model;
    }
    public T GetModel<T>() where T : BaseModel
    {
        return (T)model;
    }

    //��ȡ����ģ���ģ��
    public BaseModel GetModel(int controllerId)
    {
        //BaseController controller = ControllerManager.Instance.GetController(modelId);
        //return controller.GetModel();
        //todo..
        return null;
    }

    //���ٿ�����
    public virtual void Destroy()
    {
        //todo..
    }

    //��ʼ��ģ���¼�
    public virtual void InitModelEvent()
    {
        //todo..
    }

    //ɾ��ģ���¼�
    public virtual void RemoveModelEvent()
    {
        //todo..
    }

    //��ʼ��ȫ���¼�
    public virtual void InitGlobalEvent()
    {
        //todo..
    }

    //ɾ��ȫ���¼�
    public virtual void RemoveGlobalEvent()
    {
        //todo..
    }



}
