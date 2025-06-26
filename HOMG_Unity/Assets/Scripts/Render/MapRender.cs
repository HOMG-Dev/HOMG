using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapRender
{
    private MapModel mapModel;

    public MapRender(MapModel mapModel)
    {
        this.mapModel = mapModel;
    }

    public void ClickCell(CellPos cellPos)
    {
        // 处理点击事件
        Debug.Log($"Clicked on cell at position: {cellPos.x}, {cellPos.y}");

        mapModel.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.Null, null);
    }
}
