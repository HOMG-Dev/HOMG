using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapRender
{
    private BaseController controller = GameApp.ControllerManager.GetController(ControllerType.Game);
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
            this.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.CloseCellLandformView);
            this.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.CloseCellUnitView);
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
            landform = GameApp.ControllerManager.GetController(ControllerType.Game).GetModel<MapModel>().mapData.landformManager.GetLandform("Plain");
        }
        Debug.Log($"Landform at cell ({cellPos}): {landform.Type}");

        // 触发打开地形视图的事件
        object[] landformArgs = new object[2];
        landformArgs[0] = landform.Type;
        landformArgs[1] = landform.GetCorrectionList();

        this.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenCellLandformView, landformArgs);

        // 出发打开单位视图的事件
        object[] unitArgs = new object[2];
        if(mapModel.cellData.ContainsKey(cellPos) && mapModel.cellData[cellPos]._units.Count > 0)
        {
            unitArgs[1] = mapModel.cellData[cellPos]._units;
            this.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenCellUnitView, unitArgs);
        }
    }

}
