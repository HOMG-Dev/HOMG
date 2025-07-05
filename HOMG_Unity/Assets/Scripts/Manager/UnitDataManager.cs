using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitDataManager
{
    private Dictionary<string, UnitData> _unitManager;

    public void Init()
    {
        _unitManager = new Dictionary<string, UnitData>();
    }

    public void Register(string unitType, UnitData unitData)
    {
        if (_unitManager.ContainsKey(unitType))
        {
            Debug.LogError("UnitDataManager Register Error:以" + unitType + "为键的UnitData已经存在!");
            return;
        }
        _unitManager.Add(unitType, unitData);
    }

    public void Unregister(string unitType)
    {
        if (!_unitManager.ContainsKey(unitType))
        {
            Debug.LogError("UnitDataManager Unregister Error:以" + unitType + "为键的unitData并不存在!");
            return;
        }
        _unitManager.Remove(unitType);
    }

    public UnitData GetUnitData(string unitType)
    {
        if (!_unitManager.ContainsKey(unitType))
        {
            Debug.LogError("UnitDataManager GetUnitData Error:以" + unitType + "为键的unitData并不存在!");
            return null;
        }
        return _unitManager[unitType];
    }

    public UnitDataManager()
    {
        Init();
    }
}
