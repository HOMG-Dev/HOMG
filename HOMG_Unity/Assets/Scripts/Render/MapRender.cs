using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MapRender
{
    private MapModel _mapModel;

    private CellPos _selectedCellPos;

    private Dictionary<CellPos, Player> _playerDictionary;
    public MapRender(MapModel mapModel)
    {
        this._mapModel = mapModel;
    }

    public void ClickCell(CellPos cellPos)
    {
        if (cellPos.Equals(_selectedCellPos))
        {
            // 如果点击的单元格已经被选中，则取消选中
            _selectedCellPos = null;
            Debug.Log("Cell deselected");
            _mapModel.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.CloseCellLandformView);
            return;
        }

        _selectedCellPos = cellPos;
        // 处理点击事件

        // args[0] 是地形名称，args[1] 是修正列表
        // args[1] 中分别存储 攻击时的进攻/防御修正 和 防御时的进攻/防御修正

        Landform landform = null;
        if (_mapModel.mapData.Landform.ContainsKey(cellPos))
        {
            landform = _mapModel.mapData.Landform[cellPos];
        }
        else
        {
            landform = GameApp.ControllerManager.GetController(ControllerType.Game).GetModel<MapModel>().mapData.landformManager.GetLandform("Plain");
        }
        Debug.Log($"Landform at cell ({cellPos.x}, {cellPos.y}): {landform.Type}");
        // 触发打开地形视图的事件
        object[] args = new object[2];
        args[0] = landform.Type;
        args[1] = landform.GetCorrectionList();

        _mapModel.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenCellLandformView, args);
    }

    public void HandleResult(Result result)
    {
        switch (result.Type)
        {
            case ResultType.Undefined:
                Debug.LogError("HandleResult Error:Result的类型是Undefined!");
                break;
            case ResultType.UnitMove:
                UnitMoveResult unitMoveResult = (UnitMoveResult)result;
                _mapModel.cellData[unitMoveResult.OrignalCellPos].Units.Remove(unitMoveResult.Unit);
                _mapModel.cellData[unitMoveResult.TargetCellPos].Units.Add(unitMoveResult.Unit);
                break;
            case ResultType.CellControllerChange:
                CellOccupierChangeResult cellOccupierChangeResult = (CellOccupierChangeResult)result;
                _playerDictionary[cellOccupierChangeResult.CellPos].occupiedCells.Remove(cellOccupierChangeResult.CellPos);
                _playerDictionary[cellOccupierChangeResult.CellPos] = cellOccupierChangeResult.NewOccupier;
                _playerDictionary[cellOccupierChangeResult.CellPos].occupiedCells.Add(cellOccupierChangeResult.CellPos);
                break;
        }

    }
}
