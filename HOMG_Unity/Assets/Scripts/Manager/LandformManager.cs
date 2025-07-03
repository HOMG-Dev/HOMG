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

    public void Register(string landformType, Landform landformData)
    {
        if (_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("LandformManager Register Error:以" + landformType + "为键的lanformData已经存在!");
            return;
        }
        _landformManager.Add(landformType, landformData);
    }

    public void Unregister(string landformType)
    {
        if (!_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("LandformManager Unregister Error:以" + landformType + "为键的landformData并不存在!");
            return;
        }
        _landformManager.Remove(landformType);
    }

    public Landform GetLandformData(string landformType)
    {
        if (!_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("LandformManager GetLandformData Error:以" + landformType + "为键的landformData并不存在!");
            return null;
        }
        return _landformManager[landformType];
    }

    public LandformManager()
    {
        Init();
    }

}
