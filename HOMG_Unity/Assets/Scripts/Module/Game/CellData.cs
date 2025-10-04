using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Unit
{
    private UnitData _data;
    private string _name;

    public UnitData GetData()
    {
        return _data;
    }

    public string GetName()
    {
        return _name;
    }

    public UnitData UnitData => GetData();
    public string Type => GetData().Type;
    public string Name => GetName();
    public int ATK => GetData().ATK;
    public int DEF => GetData().DEF;

    public Unit(string unitType)
    {
        _data = GameApp.ControllerManager.GetController(ControllerType.Game).GetModel<MapModel>().mapData.unitManager.GetUnitData(unitType);
    }
}

[System.Serializable]
public class CellData
{
    private CellPos _cellPos;
    private List<SpecialType> _specialTypeList;
    private Landform _landform;
    //private List<Unit> _units;
    public List<Unit> _units;

    private MapModel _mapModel;

    public void UpdateMapModel()
    {
        _mapModel = GameApp.ControllerManager.GetController(ControllerType.Game).GetModel<MapModel>();
    }

    public List<Unit> GetUnits() => _units;

    public CellPos GetCellPos() => _cellPos;

    public void Init(CellPos cellPos, string landformType)
    {
        _cellPos = cellPos;
        _specialTypeList = new List<SpecialType>();
        UpdateMapModel();
        _landform = _mapModel.mapData.landformManager.GetLandform(landformType);
        _units = new List<Unit>();
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

    public CellData(CellPos cellPos)
    {
        _cellPos = cellPos;
    }
}
