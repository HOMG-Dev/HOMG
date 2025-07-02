using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Unit
{
    private string _unitType;

    public UnitData GetData()
    {
        return UnitManager.GetUnitData(_unitType);
    }

    public UnitData UnitData => GetData();
    public string Type => GetData().Type;
    public int ATK => GetData().ATK;
    public int DEF => GetData().DEF;

    public Unit(string unitType)
    {
        _unitType = unitType;
    }
    
}

[System.Serializable]
public class Landform
{
    private string _landformType;

    public LandformData GetData()
    {
        return LandformManager.GetLandformData(_landformType);
    }

    public List<int> GetCorrectionList()
    {
        List<int> returnList = new List<int>();
        returnList.Add(AttackingATKCorrection);
        returnList.Add(AttackingDEFCorrection);
        returnList.Add(DefendingATKCorrection);
        returnList.Add(DefendingDEFCorrection);
        return returnList;
    }

    public LandformData LandformData => GetData();
    public string Type => GetData().Type;
    public int AttackingATKCorrection => GetData().AttackingATKCorrection;
    public int AttackingDEFCorrection => GetData().AttackingDEFCorrection;
    public int DefendingATKCorrection => GetData().DefendingATKCorrection;
    public int DefendingDEFCorrection => GetData().DefendingDEFCorrection;

    public Landform(string landformType)
    {
        _landformType = landformType;
    }

}

[System.Serializable]
public class CellData
{
    private CellPos _cellPos;
    private List<SpecialType> _specialTypeList;
    private Landform _landform;
    private List<Unit> _unitList;

    public void Init(CellPos cellPos, string landformType)
    {
        _cellPos = cellPos;
        _specialTypeList = new List<SpecialType>();
        _landform = new Landform(landformType);
        _unitList = new List<Unit>();
    }

    public CellData(CellPos cellPos, string landformType)
    {
        _cellPos = cellPos;
        Init(cellPos, landformType);
    }

    public CellData(int x, int y, string landformType)
    {
        _cellPos = new CellPos(x, y);
        Init(new CellPos(x, y), landformType);
    }
}
