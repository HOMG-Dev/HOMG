using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapRender
{
    private MapModel mapModel;

    private CellPos _selectedCellPos;

    public MapRender(MapModel mapModel)
    {
        this.mapModel = mapModel;
    }

    public void ClickCell(CellPos cellPos)
    {
        if (cellPos.Equals(_selectedCellPos))
        {
            // 如果点击的单元格已经被选中，则取消选中
            _selectedCellPos = null;
            Debug.Log("Cell deselected");
            mapModel.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.CloseCellLandformView);
            return;
        }

        _selectedCellPos = cellPos;
        // 处理点击事件

        // args[0] 是地形名称，args[1] 是修正列表
        // args[1] 中分别存储 攻击时的进攻/防御修正 和 防御时的进攻/防御修正

        Landform landform = null;
        if (mapModel.mapData.Landform.ContainsKey(cellPos))
        {
            landform = mapModel.mapData.Landform[cellPos];
        }
        else
        {
            landform = new Landform(LandformType.Type.Plain);
        }
        Debug.Log($"Landform at cell ({cellPos.x}, {cellPos.y}): {landform.Type()}");
        // 触发打开地形视图的事件
        object[] args = new object[2];
        args[0] = landform.TypeName();
        args[1] = landform.GetCorrectionList();

        mapModel.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenCellLandformView, args);
    }
}
