using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

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
