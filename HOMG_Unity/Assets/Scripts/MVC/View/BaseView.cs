using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour, IBaseView
{
    public int ViewId { get; set; }
    public BaseController Controller { get; set; }
    protected Canvas _canvas;
    protected Dictionary<string, GameObject> m_cache_gos = new Dictionary<string, GameObject>();//»º´æµÄGameObject
    private bool _isInit = false;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {

    }


    public void ApplyControllerFunc(int controllerId, string eventName, params object[] args)
    {
        this.Controller.ApplyControllerFunc(controllerId, eventName, args);
    }

    public void ApplyFunc(string eventName, params object[] args)
    {
        this.Controller.ApplyFunc(eventName, args);
    }

    public virtual void Close(params object[] args)
    {
        SetVisable(false);
    }

    public void DestroyView()
    {
        Controller = null;
        Destroy(gameObject);
    }

    public virtual void InitData()
    {
        _isInit = true;
    }

    public virtual void InitUI()
    {

    }

    public bool IsInit()
    {
        return _isInit;
    }

    public bool IsShow()
    {
        return _canvas.enabled == true;
    }

    public virtual void Open(params object[] args)
    {

    }

    public void SetVisable(bool isVisable)
    {
        this._canvas.enabled = isVisable;
    }

    public GameObject Find(string res)
    {
        if (m_cache_gos.ContainsKey(res))
        {
            return m_cache_gos[res];
        }
        else
        {
            m_cache_gos.Add(res, transform.Find(res).gameObject);
            return m_cache_gos[res];
        }
    }

    public T Find<T>(string res) where T : Component
    {
        return Find(res).GetComponent<T>();
    }

}
