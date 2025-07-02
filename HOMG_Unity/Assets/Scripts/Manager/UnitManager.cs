using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{
    private static Dictionary<string, UnitData> _unitManager;

    public static void Init()
    {
        _unitManager = new Dictionary<string, UnitData>();
    }

    public static void Register(string unitType, UnitData unitData)
    {
        if (_unitManager.ContainsKey(unitType))
        {
            Debug.LogError("试图在UnitManager里注册一个键为" + unitType + "的unitData，然而这个unitData已经被注册过了!");
            return;
        }
        _unitManager.Add(unitType, unitData);
    }

    public static void Unregister(string unitType)
    {
        if (!_unitManager.ContainsKey(unitType))
        {
            Debug.LogError("试图在UnitManager里注销一个键为" + unitType + "的unitData，然而这个unitData并不存在!");
            return;
        }
        _unitManager.Remove(unitType);
    }

    public static UnitData GetUnitData(string unitType)
    {
        if (!_unitManager.ContainsKey(unitType))
        {
            Debug.LogError("试图在UnitManager里查找一个键为" + unitType + "的unitData，然而这个unitData并不存在!");
            return null;
        }
        return _unitManager[unitType];
    }

    static UnitManager()
    {
        Init();
    }
}
