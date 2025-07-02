using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData
{
    private string _unitType;
    private int _atk;
    private int _def;

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

    public string Type => GetUnitType();
    public int ATK => GetATK();
    public int DEF => GetDEF();

    public UnitData(string unitType, int atk, int def)
    {
        _unitType = unitType;
        _atk = atk;
        _def = def;
    }
}

