using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Schema;
using Unity.VisualScripting;
using UnityEngine;
public enum ResultType
{
    Undefined,
    UnitMove,
    CellControllerChange,
}

public class Result
{
    private readonly ResultType _type;

    public ResultType GetResultType()
    {
        return _type;
    }

    public ResultType Type => GetResultType();

    public Result(ResultType type)
    {
        _type = type;
    }

}

public class UnitMoveResult : Result
{
    private readonly Unit _unit;
    private readonly CellPos _orignalCellPos;
    private readonly CellPos _targetCellPos;

    public Unit GetUnit()
    {
        return _unit;
    }

    public CellPos GetOrignalCellPos()
    {
        return _orignalCellPos;
    }

    public CellPos GetTargetCellPos()
    {
        return _targetCellPos;
    }

    public Unit Unit => GetUnit();
    public CellPos CellPos => GetOrignalCellPos();
    public CellPos OrignalCellPos => GetOrignalCellPos();
    public CellPos TargetCellPos => GetTargetCellPos();

    public UnitMoveResult(ResultType type,Unit unit,CellPos orignalCellPos,CellPos targetCellPos)
        : base(type) 
    {
        _unit = unit;
        _orignalCellPos = orignalCellPos;
        _targetCellPos = targetCellPos;
    }
}

public class CellOccupierChangeResult : Result
{
    private readonly CellPos _cellPos;
    private readonly Player _newOccupier;
    public CellPos GetCellPos()
    {
        return _cellPos;
    }

    public Player GetNewOccupier()
    {
        return _newOccupier;
    }

    public CellPos CellPos => GetCellPos();
    public Player NewOccupier => GetNewOccupier();

    public CellOccupierChangeResult(ResultType type, CellPos cellPos)
        : base(type)
    {
        _cellPos = cellPos;
    }
}
