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

        Init();
    }

    public void Init()
    {
        LoadAllCells();
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
        if (mapModel.cellData.ContainsKey(cellPos) && mapModel.cellData[cellPos]._units.Count > 0)
        {
            unitArgs[1] = mapModel.cellData[cellPos]._units;
            this.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenCellUnitView, unitArgs);
        }
        else
        {
            this.controller.ApplyControllerFunc(ControllerType.GameUI, EventDefine.CloseCellUnitView);
        }
    }

    public void LoadSingleCell(CellPos cellPos)
    {
        // 找到格子和固定内容容器
        GameObject realCell = GameObject.Find(cellPos.x + "_" + cellPos.y);
        if (realCell == null)
        {
            Debug.LogError($"[MapRender] Cell root not found: {cellPos.x}_{cellPos.y}");
            return;
        }

        Transform content = realCell.transform.Find("Content");
        if (content == null)
        {
            var contentGO = new GameObject("Content");
            content = contentGO.transform;
            content.SetParent(realCell.transform, false);
            content.localPosition = Vector3.zero;
            content.localRotation = Quaternion.identity;
            content.localScale    = Vector3.one;
        }

        // 删掉原有物体
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(content.GetChild(i).gameObject);
        }

        // 加载地形（没有键则回退到 Plain）
        Landform landform;
        if (!mapModel.mapData.Landform.TryGetValue(cellPos, out landform))
        {
            landform = mapModel.mapData.landformManager.GetLandform("Plain");
        }

        GameObject landPrefab = Resources.Load<GameObject>(landform.GetModelPath());
        if (landPrefab == null)
        {
            Debug.LogWarning($"[MapRender] Missing landform prefab at {cellPos} for {landform.Type}.");
        }
        else
        {
            GameObject landGO = GameObject.Instantiate(landPrefab, content);
            landGO.name = landform.Type;
            landGO.transform.localPosition = Vector3.zero; // 以 Content 为原点
            landGO.transform.localRotation = Quaternion.identity;
            landGO.transform.localScale = new Vector3(10, 10, 10);
        }

        // 加载单位
        if (mapModel.cellData.TryGetValue(cellPos, out var cell) && cell._units != null && cell._units.Count > 0)
        {
            for (int i = 0; i < cell._units.Count; i++)
            {
                var unit = cell._units[i];
                GameObject unitPrefab = Resources.Load<GameObject>(unit.UnitData.GetModelPath());
                if (unitPrefab == null)
                {
                    Debug.LogWarning($"[MapRender] Missing unit prefab at {cellPos} for {unit}.");
                    continue;
                }

                GameObject unitGO = GameObject.Instantiate(unitPrefab, content);
                unitGO.name = unit.Name;

                // 简单错位
                var offset = new Vector3((i % 2) * 0.4f, 0f, (i / 2) * 0.4f);
                unitGO.transform.localPosition = offset;
                unitGO.transform.localRotation = Quaternion.identity;
                unitGO.transform.localScale = new Vector3(10, 10, 10);
            }
        }
    }

    public void LoadAllCells()
    {
        // 加载所有Cell的渲染
        foreach (var cellPos in mapModel.cellData.Keys)
        {
            LoadSingleCell(cellPos);
        }
    }
}
