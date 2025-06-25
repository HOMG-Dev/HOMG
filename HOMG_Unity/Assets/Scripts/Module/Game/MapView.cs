using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 测试的地图视图
/// </summary>
public class MapView : BaseView
{
    private List<CellBehavior> _highlightingCells;//正在高亮的cell
    private List<CellBehavior> _targetCells;//准备要高亮的cell
    private List<CellBehavior> _selectedCells;//选中的cell



    public override void Close(params object[] args)
    {
        base.Close(args);
        _highlightingCells = null;
        _targetCells = null;
        _selectedCells = null;

    }

    public override void Pause(params object[] args)
    {
        base.Pause(args);
        this.enabled = false;
        Debug.Log("Pause");
    }

    public override void Resume(params object[] args)
    {
        base.Resume(args);
        this.enabled = true;
        Debug.Log("Resume");
    }

    public override void InitData()
    {
        base.InitData();
        _highlightingCells = new List<CellBehavior>();
        _targetCells = new List<CellBehavior>();
        _selectedCells = new List<CellBehavior>();
        CreateCells();
    }

    private void CreateCells()
    {
        //MapData mapData = this.Controller.GetModel<MapModel>().mapData;
        MapData mapData = new MapData(20, 20);
        int length = mapData.Length;
        int width = mapData.Width;
        List<CellPos> inVisableCell = mapData.InVisableCell;
        Transform parTf = GameObject.Find("MapMagic").GetComponent<Transform>();

        //向右为length，向上为width
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (inVisableCell.Contains(new CellPos(i, j)))
                {
                    continue;
                }

                GameObject cell = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/CellPrefab"));
                cell.transform.SetParent(parTf);

                float delta = (j % 2 == 0) ? 75f/2f : 0;
                cell.transform.localPosition = new Vector3(i * (ConstantDefine.CellLength) + delta, 25, j * (ConstantDefine.CellWidth/2));
                cell.name = i + "_" + j;
                cell.GetComponent<CellBehavior>().Lowlight();
                //mc.Lowlight();
                //cell.GetComponent<CellBehavior>().Init(i, j);
            }
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        //初始化按钮
        InitBtn();
    }

    private void InitBtn()
    {
        Find<Button>("quitBtn").onClick.AddListener(onQuitBtn);
    }

    private void onQuitBtn()
    {
        ApplyFunc(EventDefine.QuitGame);
    }

    private void Update()
    {
        KeyDetect();
        MouseDetect();
    }

    private void KeyDetect()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ApplyFunc(EventDefine.CameraMove, Vector3.forward * Time.deltaTime * ConstantDefine.CameraMoveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            ApplyFunc(EventDefine.CameraMove, Vector3.back * Time.deltaTime * ConstantDefine.CameraMoveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            ApplyFunc(EventDefine.CameraMove, Vector3.left * Time.deltaTime * ConstantDefine.CameraMoveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            ApplyFunc(EventDefine.CameraMove, Vector3.right * Time.deltaTime * ConstantDefine.CameraMoveSpeed);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ApplyFunc(EventDefine.OpenInGameSettingView);
        }
    }


    private void MouseDetect()
    {
        Camera mainCamera = Camera.main;
        Camera mapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();

        // 获取鼠标在屏幕上的点（范围 [0,1]）
        Vector3 viewportPoint = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        // 将视口点映射到 Map Camera 的射线
        Ray ray = mapCamera.ViewportPointToRay(viewportPoint);

        _targetCells.Clear();

        Debug.DrawRay(ray.origin, ray.direction * 400, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            CellBehavior cell = hit.transform.GetComponent<CellBehavior>();
            if (cell != null)
            {
                _targetCells.Add(cell);
                OnClick(cell);
            }
        }
        HighlightCells();
    }

    private void HighlightCells()
    {
        foreach (var cell in _highlightingCells)
        {
            if (_selectedCells.Contains(cell) == false)
            {
                cell.Lowlight();
            }
        }
        foreach (var cell in _targetCells)
        {
            if (_selectedCells.Contains(cell) == false)
            {
                cell.Highlight();
                _highlightingCells.Add(cell);
            }
        }
    }

    private void OnClick(CellBehavior myCell)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedCells.Contains(myCell) == true)
            {
                myCell.Lowlight();
                _selectedCells.Remove(myCell);
            }
            else
            {
                myCell.OnMouseDown();
                _selectedCells.Add(myCell);
            }
        }
    }
}
