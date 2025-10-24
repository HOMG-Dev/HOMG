using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局游戏控制器，处理游戏整体逻辑
/// </summary>
public class GameController : BaseController
{
    public MapRender mapRender;
    private CellPos _selectedCellPos;
    private List<string> _selectedUnitNames = new List<string>();
    private Dictionary<string, TupleCellPos> _movePath = new Dictionary<string, TupleCellPos>();
    private Dictionary<TupleCellPos, int> _pathCount = new Dictionary<TupleCellPos, int>();

    public GameController() : base()
    {
        //注册事件
        InitModelEvent();
        InitGlobalEvent();
    }

    public override void Init()
    {
        base.Init();

        //测试地图视图
        GameApp.ViewManager.Register(ViewType.MapView, new ViewInfo()
        {
            PrefabName = "MapView",

            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 1,
        });


        //对局内设置视图
        GameApp.ViewManager.Register(ViewType.InGameSettingView, new ViewInfo()
        {
            PrefabName = "InGameSettingView",
            parentTf = GameApp.ViewManager.canvasTf,
            controller = this,
            sortintOrder = 999,
        });


        //打开开始界面
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.OpenStartView);
    }

    public override void InitModelEvent()
    {
        base.InitModelEvent();

        //打开地图界面
        RegisterFunc(EventDefine.OpenMapView, OpenMapView);
        RegisterFunc(EventDefine.CloseMapView, CloseMapView);

        //摄像头移动
        RegisterFunc(EventDefine.CameraMove, CameraMove);

        //游戏设置界面
        RegisterFunc(EventDefine.OpenInGameSettingView, OpenInGameSettingView);
        RegisterFunc(EventDefine.CloseInGameSettingView, CloseInGameSettingView);

        //退出对局
        RegisterFunc(EventDefine.QuitGame, QuitGame);

        //Render事件
        RegisterFunc(EventDefine.LeftClickCell, LeftClickCell); //点击地图格子事件
        RegisterFunc(EventDefine.RightClickCell, RightClickCell);

        RegisterFunc(EventDefine.OnCellUnitButtonDown, OnCellUnitButtonDown);
        RegisterFunc(EventDefine.ClearSelectedUnitNames, ClearSelectedUnitNames);
        RegisterFunc(EventDefine.OnCellUnitButtonUp, OnCellUnitButtonUp);
    }

    private void OpenMapView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.MapView, args);

        mapRender = new MapRender(GetModel<MapModel>());
    }

    private void CloseMapView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.MapView, args);
    }

    private void CameraMove(System.Object[] args)
    {
        Vector3 direction = (Vector3)args[0];
        GameObject.Find("Map Camera").transform.position += direction;
    }

    private void OpenInGameSettingView(System.Object[] args)
    {
        GameApp.ViewManager.Open(ViewType.InGameSettingView, args);
        GameApp.ViewManager.Pause(ViewType.MapView);
    }

    private void CloseInGameSettingView(System.Object[] args)
    {
        GameApp.ViewManager.Close(ViewType.InGameSettingView, args);
        GameApp.ViewManager.Resume(ViewType.MapView);
    }

    private void QuitGame(System.Object[] args)
    {
        //退出地图
        ApplyFunc(EventDefine.CloseMapView);

        //关闭地图 或者 禁用脚本的update
        //todo..
        ApplyControllerFunc(ControllerType.GameUI, EventDefine.QuitGame);
    }

    private void LeftClickCell(System.Object[] args)
    {
        MapModel mapModel = GetModel<MapModel>();

        CellBehavior cell = args[0] as CellBehavior;
        if (cell == null)
        {
            Debug.LogError("[GameController] LeftClickCell: CellBehavior is null");
            return;
        }

        CellPos cellPos = cell.cellPos;

        bool isSelectedCellPos = cellPos.Equals(_selectedCellPos);
        Landform landform = null;
        object[] unitArgs = new object[2];
        object[] landformArgs = new object[2];

        if (isSelectedCellPos)
        {
            // 如果点击的单元格已经被选中，则取消选中
            _selectedCellPos = null;
            mapRender.LeftClickCell(cellPos, isSelectedCellPos, landformArgs, unitArgs);
            return;
        }

        _selectedCellPos = cellPos;

        // 处理地形
        if (mapModel.mapData.Landform.ContainsKey(cellPos))
        {
            landform = mapModel.mapData.Landform[cellPos];
        }
        else
        {
            landform = GameApp.ControllerManager.GetController(ControllerType.Game).GetModel<MapModel>().mapData
                .landformManager.GetLandform("Plain");
        }

        Debug.Log($"Landform at cell ({cellPos}): {landform.Type}");
        landformArgs[0] = landform.Type;
        landformArgs[1] = landform.GetCorrectionList();

        // 触发打开单位视图的事件
        if (mapModel.cellData.ContainsKey(cellPos) && mapModel.cellData[cellPos]._units.Count > 0)
        {
            unitArgs[1] = mapModel.cellData[cellPos]._units;
        }

        mapRender.LeftClickCell(cellPos, isSelectedCellPos, landformArgs, unitArgs);
    }

    public void ClearSelectedUnitNames(System.Object[] args)
    {
        _selectedUnitNames = new List<string>();
    }

    public void OnCellUnitButtonUp(System.Object[] args)
    {
        string unitName = args[0] as string;
        _selectedUnitNames.Remove(unitName);
    }

    private void OnCellUnitButtonDown(System.Object[] args)
    {
        string unitName = args[0] as string;
        _selectedUnitNames.Add(unitName);
        Debug.Log($"OnCellUnitButtonDown: {unitName}");
    }

    private void RightClickCell(System.Object[] args)
    {
        CellBehavior cell = args[0] as CellBehavior;
        CellPos cellPos = cell.cellPos;

        // to do: 传出接口写在这里
        // to do: 状态机
        foreach (string unitname in _selectedUnitNames)
        {
            TupleCellPos path;

            if (_movePath.ContainsKey(unitname))
            {
                path = _movePath[unitname];
                _pathCount[path]--;
                if (_pathCount[path] == 0)
                {
                    mapRender.RightClickCell(path.st, path.ed, true);
                    _pathCount.Remove(path);
                }

                if (cellPos.Equals(_selectedCellPos))
                {
                    _movePath.Remove(unitname);
                }
                else
                {
                    _movePath[unitname] = new TupleCellPos(_selectedCellPos, cellPos);
                }
            }

            if (cellPos.Equals(_selectedCellPos))
            {
                continue;
            }

            path = new TupleCellPos(_selectedCellPos, cellPos);
            _movePath[unitname] = path;

            if (_pathCount.ContainsKey(path))
            {
                _pathCount[path]++;
            }
            else
            {
                _pathCount[path] = 1;
                mapRender.RightClickCell(_selectedCellPos, cellPos);
            }
        }
    }
}
