using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

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
