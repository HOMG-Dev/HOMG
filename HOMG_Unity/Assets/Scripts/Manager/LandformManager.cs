using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LandformManager
{
    private Dictionary<string, Landform> _landformManager;

    public void Init()
    {
        _landformManager = new Dictionary<string, Landform>();
        /* 测试代码开始 */
        Register("Plain", new Landform("Plain", 0, 0, 0, 0));
        Register("Mountain", new Landform("Mountain", -1, 0, 0, 0));
        /* 测试代码结束 */
    }

    public void Register(string landformType, Landform landform)
    {
        if (_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("LandformManager Register Error:以" + landformType + "为键的lanform已经存在!");
            return;
        }
        _landformManager.Add(landformType, landform);
    }

    public void Unregister(string landformType)
    {
        if (!_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("LandformManager Unregister Error:以" + landformType + "为键的landform并不存在!");
            return;
        }
        _landformManager.Remove(landformType);
    }

    public Landform GetLandform(string landformType)
    {
        if (!_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("LandformManager GetLandform Error:以" + landformType + "为键的landform并不存在!");
            return null;
        }
        return _landformManager[landformType];
    }

    public LandformManager()
    {
        Init();
    }

}
