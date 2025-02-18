using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfo
{
    public string PrefabName;//Ԥ��������
    public Transform parentTf;//������
    public BaseController controller;//����������
    public int sortintOrder;//���� ���������
}

/// <summary>
/// ��ͼ������
/// </summary>
public class ViewManager
{
    public Transform canvasTf;//����
    public Transform worldCanvasTf;//�������껭��
    Dictionary<int, IBaseView> _openedViews;//���д򿪵���ͼ
    Dictionary<int, IBaseView> _viewCache;//��ͼ����
    Dictionary<int, ViewInfo> _views;//ע���������ͼ��Ϣ��ע�������ͼ���ܱ����� ��

    public ViewManager()
    {
        canvasTf = GameObject.Find("Canvas").transform;
        worldCanvasTf = GameObject.Find("WorldCanvas").transform;
        _openedViews = new Dictionary<int, IBaseView>();
        _viewCache = new Dictionary<int, IBaseView>();
        _views = new Dictionary<int, ViewInfo>();
    }

    //ע����ͼ��Ϣ
    public void Register(int viewId, ViewInfo viewInfo)
    {
        if (_views.ContainsKey(viewId) == true)
        {
            Debug.LogError("ViewManager Register Error: viewId " + viewId + " is already exist!");
            return;
        }
        _views.Add(viewId, viewInfo);
    }
    public void Register(ViewType viewType, ViewInfo viewInfo)
    {
        Register((int)viewType, viewInfo);
    }

    //ɾ����ͼ��Ϣ
    public void Unregister(int viewId)
    {
        if (_views.ContainsKey(viewId) == true)
        {
            _views.Remove(viewId);
        }
        else
        {
            Debug.LogError("ViewManager Unregister Error: viewId " + viewId + " doesn't exist!");
            return;
        }
    }

    //ɾ����ͼ
    public void RemoveView(int viewId)
    {
        _views.Remove(viewId);
        _viewCache.Remove(viewId);
        _openedViews.Remove(viewId);
    }

    //ɾ��������������������ͼ
    public void RemoveViewByController(BaseController controller)
    {
        foreach (var item in _views)
        {
            if (item.Value.controller == controller)
            {
                RemoveView(item.Key);
            }
        }
    }

    //��ͼ�Ƿ���
    public bool IsViewOpened(int viewId)
    {
        return _openedViews.ContainsKey(viewId);
    }

    //��ȡ��ͼ
    public IBaseView GetView(int viewId)
    {
        if (_openedViews.ContainsKey(viewId) == true)
        {
            return _openedViews[viewId];
        }
        if (_viewCache.ContainsKey(viewId) == true)
        {
            return _viewCache[viewId];
        }
        //Debug.Log("ViewManager GetView Error: viewId " + viewId + " doesn't exist.");  
        return null;
    }

    public T GetView<T>(int viewId) where T : class, IBaseView
    {
        IBaseView view = GetView(viewId);
        if (view != null)
        {
            return (T)view;
        }
        return null;
    }

    //������ͼ
    public void Destroy(int viewId)
    {
        IBaseView view = GetView(viewId);
        if (view != null)
        {
            Unregister(viewId);
            view.DestroyView();
            _viewCache.Remove(viewId);
        }
    }


    //������ͼ
    public void Open(ViewType viewType, params object[] args)
    {
        Open((int)viewType, args);
    }

    public void Open(int viewId, params object[] args)
    {
        if (IsViewOpened(viewId) == true)
        {
            Debug.Log("ViewManager Open Error: viewId " + viewId + " is already opened.");
            return;
        }
        if (_views.ContainsKey(viewId) == false)
        {
            Debug.LogError("ViewManager Open Error: viewId " + viewId + " doesn't be registed.");
            return;
        }

        ViewInfo viewInfo = _views[viewId];
        IBaseView view = GetView(viewId);
        if (view == null) //��ͼδ����
        {
            string type = ((ViewType)viewId).ToString();
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>($"View/{viewInfo.PrefabName}"), viewInfo.parentTf);
            if (go == null)
            {
                Debug.LogError("ViewManager Open Error: view prefab " + type + " doesn't exist.");
                return;
            }
            Canvas canvas = go.GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = go.AddComponent<Canvas>();
            }
            if (go.GetComponent<GraphicRaycaster>() == null)
            {
                go.AddComponent<GraphicRaycaster>();
            }
            canvas.overrideSorting = true;
            canvas.sortingOrder = viewInfo.sortintOrder;
            view = go.AddComponent(Type.GetType(type)) as IBaseView;//�����ͼ�ű�
            view.ViewId = viewId;
            view.Controller = viewInfo.controller;

            _viewCache.Add(viewId, view);
            viewInfo.controller.OnLoadView(view);
        }

        if (view.IsInit() == false)
        {
            view.InitUI();
            view.InitData();
        }
        view.SetVisable(true);
        view.Open(args);
        _openedViews.Add(viewId, view);
        _views[viewId].controller.OpenView(view);
    }

    //�ر���ͼ
    public void Close(int viewId, params object[] args)
    {
        if (IsViewOpened(viewId) == false)
        {
            return;
        }

        IBaseView view = GetView(viewId);
        if (view != null)
        {
            view.Close(args);
            _openedViews.Remove(viewId);
            _views[viewId].controller.CloseView(view);
        }
    }

}
