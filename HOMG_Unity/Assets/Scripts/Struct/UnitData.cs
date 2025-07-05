using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData
{
    private string _unitType;
    private int _atk;
    private int _def;
    private Cost _ATKCost;
    private Cost _maintenanceCost;

    public string GetUnitType()
    {
        return _unitType;
    }

    public int GetATK()
    {
        return _atk;
    }

    public int GetDEF()
    {
        return _def;
    }

    public Cost GetATKCost()
    {
        if (_ATKCost == null)
        {
            _ATKCost = new Cost(0, 0);
        }
        return _ATKCost;
    }

    public Cost GetMaintenanceCost()
    {
        if (_maintenanceCost == null)
        {
            _maintenanceCost = new Cost(0, 0);
        }
        return _maintenanceCost;
    }


    public string Type => GetUnitType();
    public int ATK => GetATK();
    public int DEF => GetDEF();
    public Cost ATKCost => GetATKCost();
    public Cost MaintenanceCost => GetMaintenanceCost();

    public UnitData(string unitType, int atk, int def)
    {
        _unitType = unitType;
        _atk = atk;
        _def = def;
    }
}

