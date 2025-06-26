using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class Unit
{
    private UnitType.Type _type;
    private int _ID;
    private static int _IDCounter = 0;

    public UnitType.Type Type()
    {
        return _type;
    }

    private int NewID()
    {
        return ++_IDCounter;
    }

    public int ID()
    {
        return _ID;
    }

    public static int IDCounter()
    {
        return _IDCounter;
    }

    public Unit()
    {
        _ID = NewID();
        _type = UnitType.Type.Undefined;
    }

    public Unit(UnitType.Type unitType)
    {
        _ID = NewID();
        _type = unitType;
    }

    public int Attack()
    {
        return UnitType.GetUnitTypeStats(Type()).attack;
    }

    public int Defense()
    {
        return UnitType.GetUnitTypeStats(Type()).defense;
    }
}

public class Landform
{
    private LandformType.Type _landformType;

    public Landform(LandformType.Type landformType)
    {
        _landformType = landformType;
    }

    public LandformType.Type Type()
    {
        return _landformType;
    }

    public int AttackingATKCorrection()
    {
        return LandformType.GetLandformTypeCorrection(Type()).attackingATKCorrection;
    }

    public int AttackingDEFCorrection()
    {
        return LandformType.GetLandformTypeCorrection(Type()).attackingDEFCorrection;
    }

    public int DefendingATKCorrection()
    {
        return LandformType.GetLandformTypeCorrection(Type()).defendingATKCorrection;
    }

    public int DefendingDEFCorrection()
    {
        return LandformType.GetLandformTypeCorrection(Type()).defendingDEFCorrection;
    }
}

public class CellData
{
    private CellPos _cellPos;
    private List<SpecialType> _specialTypeList;
    private Landform _landform;
    private List<Unit> _unitList;

    public void Init(CellPos cellPos,LandformType.Type landformType)
    {
        _cellPos = cellPos;
        _specialTypeList = new List<SpecialType>();
        _landform = new Landform(landformType);
        _unitList = new List<Unit>();
    }

    public CellData(CellPos cellPos,LandformType.Type landformType)
    {
        _cellPos = cellPos;
        Init(cellPos, landformType);
    }

    public CellData(int x,int y, LandformType.Type landformType)
    {
        _cellPos = new CellPos(x,y);
        Init(new CellPos(x,y),landformType);
    }
}
