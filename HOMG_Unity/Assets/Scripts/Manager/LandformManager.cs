using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandformManager
{
    private static Dictionary<string, LandformData> _landformManager;

    public static void Init()
    {
        _landformManager = new Dictionary<string, LandformData>();
        /* 测试代码开始 */
        Register("Plain", new LandformData("Plain", 0, 0, 0, 0));
        Register("Mountain", new LandformData("Mountain", -1, 0, 0, 0));
        /* 测试代码结束 */
    }

    public static void Register(string landformType, LandformData landformData)
    {
        if (_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("试图在LandformManager里注册一个键为" + landformType + "的lanformData，然而这个landformData已经被注册过了!");
            return;
        }
        _landformManager.Add(landformType, landformData);
    }

    public static void Unregister(string landformType)
    {
        if (!_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("试图在LandformManager里注销一个键为" + landformType + "的landformData，然而这个landformData并不存在!");
            return;
        }
        _landformManager.Remove(landformType);
    }

    public static LandformData GetLandformData(string landformType)
    {
        if (!_landformManager.ContainsKey(landformType))
        {
            Debug.LogError("试图在LandformManager里查找一个键为" + landformType + "的landformData，然而这个landformData并不存在!");
            return null;
        }
        return _landformManager[landformType];
    }

    static LandformManager()
    {
        Init();
    }

}
