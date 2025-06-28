using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInfo
{
    public string PrefabName;//预制体名称
    public Transform parentTf;//父物体
    public BaseController controller;//所属控制器
    public int sortintOrder;//排序 大的在上面
}

/// <summary>
/// 视图管理器
/// </summary>
public class ViewManager
{
    public Transform canvasTf;//画布
    public Transform worldCanvasTf;//世界坐标画布
    Dictionary<int, IBaseView> _openedViews;//所有打开的视图
    Dictionary<int, IBaseView> _viewCache;//视图缓存
    Dictionary<int, ViewInfo> _views;//注册的所有视图信息，注册过的视图才能被加载 打开

    public ViewManager()
    {
        canvasTf = GameObject.Find("Canvas").transform;
        worldCanvasTf = GameObject.Find("WorldCanvas").transform;
        _openedViews = new Dictionary<int, IBaseView>();
        _viewCache = new Dictionary<int, IBaseView>();
        _views = new Dictionary<int, ViewInfo>();
    }

    //注册视图信息
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

    //删除视图信息
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

    //删除视图
    public void RemoveView(int viewId)
    {
        _views.Remove(viewId);
        _viewCache.Remove(viewId);
        _openedViews.Remove(viewId);
    }

    //删除所属控制器的所有视图
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

    //视图是否开启
    public bool IsViewOpened(int viewId)
    {
        return _openedViews.ContainsKey(viewId);
    }

    public IBaseView GetView(ViewType viewType)
    {
        return GetView((int) viewType);
    }

    //获取视图
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

    public T GetView<T>(ViewType viewId) where T : class, IBaseView
    {
        return GetView<T>((int)viewId);
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

    //销毁视图
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


    //开启视图
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
        if (view == null) //视图未加载
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
            view = go.AddComponent(Type.GetType(type)) as IBaseView;//添加视图脚本
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

    //暂停视图
    public void Pause(ViewType viewType, params object[] args)
    {
        Pause((int)viewType, args);
    }

    public void Pause(int viewId, params object[] args)
    {
        if (IsViewOpened(viewId) == false)
        {
            return;
        }
        IBaseView view = GetView(viewId);
        if (view != null)
        {
            view.Pause(args);
        }
    }

    //恢复视图
    public void Resume(ViewType viewType, params object[] args)
    {
        Resume((int)viewType, args);
    }

    public void Resume(int viewId, params object[] args)
    {
        if (IsViewOpened(viewId) == false)
        {
            return;
        }
        IBaseView view = GetView(viewId);
        if (view != null)
        {
            view.Resume(args);
        }
    }

    //关闭视图
    public void Close(ViewType viewType, params object[] args)
    {
        Close((int)viewType, args);
    }
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
