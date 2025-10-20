using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 测试的地图视图
/// </summary>
public class MapView : BaseView
{
    private CellBehavior _highlightingCell;//高亮中的cell
    private CellBehavior _selectedCell;//被选中的cell
    private CellBehavior _targetCell;//鼠标指着的cell

    // 箭头
    private Dictionary<TupleCellPos, GameObject> _arrows; // 存储箭头，key为箭头ID
    private Transform _arrowParent; // 箭头父对象
    private Dictionary<TupleCellPos, List<float>> _arrowAnimationDelays; // 每个箭头的动画延迟

    //箭头动画
    public float arrowSpacing = 8f;     // 箭头间距
    public float fadeSpeed = 2f;        // 渐变速度
    public float minAlpha = 0.3f;       // 最小透明度
    public float maxAlpha = 1f;         // 最大透明度
    public float animationDelay = 0.2f; // 每个箭头的动画延迟

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

        _arrows = new Dictionary<TupleCellPos, GameObject>();
        _arrowAnimationDelays = new Dictionary<TupleCellPos, List<float>>();
        CreateCells();

        // 创建箭头父对象
        GameObject arrowContainer = new GameObject("ArrowContainer");
        arrowContainer.transform.SetParent(GameObject.Find("MapMagic").transform);
        _arrowParent = arrowContainer.transform;

        this.Controller.RegisterFunc(EventDefine.CreateArrow, CreateArrow);
        this.Controller.RegisterFunc(EventDefine.DeleteArrow, DeleteArrow);
    }

    private void CreateCells()
    {
        MapData mapData = this.Controller.GetModel<MapModel>().mapData;
        Debug.Log("---------------------------------------");
        Debug.Log(mapData.MapName);
        Debug.Log(mapData.Landform[new CellPos(0, 0)].Type);
        // MapData mapData = new MapData(20, 20);
        int length = mapData.Length;
        int width = mapData.Width;
        List<CellPos> InvisableCell = mapData.InvisableCell;
        Transform parTf = GameObject.Find("MapMagic").GetComponent<Transform>();

        //向右为length，向上为width
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (InvisableCell.Contains(new CellPos(i, j)))
                {
                    continue;
                }

                GameObject cell = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/CellPrefab"));
                cell.transform.SetParent(parTf);

                float delta = (j % 2 == 0) ? 75f/2f : 0;
                cell.transform.localPosition = new Vector3(i * (ConstantDefine.CellLength) + delta, 25, j * (ConstantDefine.CellWidth/2));
                cell.name = i + "_" + j;
                cell.GetComponent<CellBehavior>().Lowlight();
                cell.GetComponent<CellBehavior>().cellPos = new CellPos(i, j);
                //mc.Lowlight();
                //cell.GetComponent<CellBehavior>().Init(i, j);
            }
        }
    }

    public override void Open(System.Object[] args)
    {
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

        // 添加箭头透明度动画
        UpdateArrowFadeAnimation();
    }

     private void UpdateArrowFadeAnimation()
    {
        foreach (var arrowPair in _arrows)
        {
            TupleCellPos arrowKey = arrowPair.Key;
            GameObject arrowContainer = arrowPair.Value;

            if (arrowContainer != null)
            {
                // 获取容器下的所有箭头子对象
                Transform[] arrowChildren = arrowContainer.GetComponentsInChildren<Transform>();

                // 获取动画延迟信息
                List<float> delays = _arrowAnimationDelays.ContainsKey(arrowKey) ?
                    _arrowAnimationDelays[arrowKey] : new List<float>();

                for (int i = 1; i < arrowChildren.Length; i++) // 跳过容器本身
                {
                    GameObject arrow = arrowChildren[i].gameObject;
                    float delay = (i-1) < delays.Count ? delays[i-1] : 0f;

                    // 带延迟的动画
                    float alpha = Mathf.Lerp(minAlpha, maxAlpha,
                        (Mathf.Sin(Time.time * fadeSpeed + delay) + 1f) / 2f);

                    UpdateSingleArrowAlpha(arrow, alpha);
                }
            }
        }
    }

    private void UpdateSingleArrowAlpha(GameObject arrow, float alpha)
    {
        Renderer renderer = arrow.GetComponent<Renderer>();
        if (renderer != null && renderer.material != null)
        {
            SetMaterialTransparent(renderer.material);

            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
    }

    private void SetMaterialTransparent(Material material)
    {
        // 设置材质为透明模式
        material.SetFloat("_Mode", 3); // Transparent mode
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
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

    private bool IsPointerOverUIWithMinHits(int minHits = 2)
    {
        if (EventSystem.current == null) return false;

        var data = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);

        // 可选：忽略特定“透传层/标签”的 UI（比如你的全屏覆盖层）
        int count = 0;
        for (int i = 0; i < results.Count; i++)
        {
            var go = results[i].gameObject;

            // 如果给覆盖层设置了专用 Layer 或 Tag，这里直接跳过
            // if (go.layer == LayerMask.NameToLayer("UI_PassThrough")) continue;
            // if (go.CompareTag("UI_PassThrough")) continue;

            count++;
            if (count >= minHits) return true;
        }
        return false;
    }

    private void MouseDetect()
    {
        if (IsPointerOverUIWithMinHits(2)) return; // 至少命中两个 UI 才拦地图

        Camera mainCamera = Camera.main;
        Camera mapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();

        // 获取鼠标在屏幕上的点（范围 [0,1]）
        Vector3 viewportPoint = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        // 将视口点映射到 Map Camera 的射线
        Ray ray = mapCamera.ViewportPointToRay(viewportPoint);

        Debug.DrawRay(ray.origin, ray.direction * 400, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            CellBehavior cell = hit.transform.GetComponent<CellBehavior>();
            if (cell != null)
            {
                _targetCell = cell;
                OnClick(cell);
            }
        }
        HighlightCells();
    }

    private void HighlightCells()
    {
        if (_targetCell == _selectedCell || _targetCell == _highlightingCell || _targetCell == null)
            return;

        if (_highlightingCell == _selectedCell)
        {
            _highlightingCell = null;
        }
        else
        {
            if(_highlightingCell != null)
            {
                _highlightingCell.Lowlight();
            }
        }
        _targetCell.Highlight();
        _highlightingCell = _targetCell;
    }


    private void OnClick(CellBehavior myCell)
    {
        if (Input.GetMouseButtonDown(0))
        {
            ApplyFunc(EventDefine.LeftClickCell, myCell);

            if(_selectedCell == myCell)
            {
                myCell.Lowlight();
                _selectedCell = null;
            }
            else
            {
                myCell.OnMouseDown();
                if(_selectedCell != null)
                {
                    _selectedCell.Lowlight();
                    _selectedCell = null;
                }
                _selectedCell = myCell;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // 判断一下是不是selectedCell
            if (_selectedCell != null)
            {
                ApplyFunc(EventDefine.RightClickCell, myCell);
            }
        }
    }

    // 在两个坐标之间创建箭头
    public void CreateArrow(System.Object[] args)
    {
        TupleCellPos arrowCoordinate;

        if(args.Length < 1 || args[0] == null)
        {
            Debug.LogWarning("提供了不合法的箭头");
            return;
        }
        else
        {
            arrowCoordinate = args[0] as TupleCellPos;
        }

        if(ArrowExist(arrowCoordinate))
        {
            Debug.LogWarning("箭头已经存在，无法重复创建");
            return;
        }
        if(arrowCoordinate.st.Equals(arrowCoordinate.ed))
        {
            Debug.LogWarning("起点和终点相同，无法创建箭头");
            return;
        }

        // 获取世界坐标
        Vector3 fromWorldPos = GetWorldPositionFromCellPos(arrowCoordinate.st);
        Vector3 toWorldPos = GetWorldPositionFromCellPos(arrowCoordinate.ed);

        GameObject arrow = CreateArrowGameObject(fromWorldPos, toWorldPos, arrowCoordinate);
        _arrows[arrowCoordinate] = arrow;
    }

    // 删除指定的箭头
    public void DeleteArrow(System.Object[] args)
    {
        TupleCellPos arrowCoordinate;

        if(args.Length < 1 || args[0] == null)
        {
            Debug.LogWarning("提供了不合法的箭头");
            return;
        }
        else
        {
            arrowCoordinate = args[0] as TupleCellPos;
        }

        if(ArrowExist(arrowCoordinate))
        {
            DestroyImmediate(_arrows[arrowCoordinate]);

            _arrows.Remove(arrowCoordinate);

            // 清理动画延迟信息
            if(_arrowAnimationDelays.ContainsKey(arrowCoordinate))
            {
                _arrowAnimationDelays.Remove(arrowCoordinate);
            }
        }
    }

    // 删除所有箭头
    public void DeleteAllArrows()
    {
        foreach (var arrow in _arrows.Values)
        {
            if (arrow != null)
            {
                DestroyImmediate(arrow);
            }
        }
        _arrows.Clear();
        _arrowAnimationDelays.Clear(); // 清理所有动画延迟信息
    }

    // 获取当前箭头数量
    public int GetArrowCount()
    {
        return _arrows.Count;
    }

    // 检查指定的箭头是否存在
    public bool ArrowExist(TupleCellPos arrowCoordinate)
    {
        return _arrows.ContainsKey(arrowCoordinate) && _arrows[arrowCoordinate] != null;
    }

    private GameObject CreateArrowGameObject(Vector3 fromPos, Vector3 toPos, TupleCellPos arrowKey)
    {
        GameObject arrowContainer = new GameObject($"Arrow_from_cell({arrowKey.st})_to_cell({arrowKey.ed})");
        arrowContainer.transform.SetParent(_arrowParent);

        Vector3 direction = (toPos - fromPos).normalized;
        float distance = Vector3.Distance(fromPos, toPos);

        // 计算需要多少个箭头
        int arrowCount = Mathf.Max(1, Mathf.RoundToInt(distance / arrowSpacing));

        // 沿路径创建多个箭头
        CreateMultipleArrows(arrowContainer, fromPos, toPos, direction, distance, arrowCount, arrowKey);

        return arrowContainer;
    }

    private void CreateMultipleArrows(GameObject parent, Vector3 fromPos, Vector3 toPos, Vector3 direction, float distance, int arrowCount, TupleCellPos arrowKey)
    {
        GameObject arrowPrefab = Resources.Load<GameObject>("Prefab/singleArrow");
        if (arrowPrefab == null)
        {
            Debug.LogError("找不到箭头预制体！");
            return;
        }

        // 存储每个箭头的动画延迟
        List<float> delays = new List<float>();

        for (int i = 0; i < arrowCount; i++)
        {
            // 计算每个箭头的位置
            float t = (float)(i + 1) / (arrowCount + 1); // 避开起点和终点
            Vector3 arrowPos = Vector3.Lerp(fromPos, toPos, t);

            // 创建箭头
            GameObject arrow = GameObject.Instantiate(arrowPrefab);
            arrow.transform.SetParent(parent.transform);
            arrow.transform.position = arrowPos;

            // 设置朝向
            arrow.transform.LookAt(arrowPos + direction, Vector3.up);
            arrow.transform.Rotate(90, -90, 0);

            // 保持固定尺寸
            arrow.transform.localScale = new Vector3(2f, 1.2f, 1.2f);
            arrow.name = $"Arrow_{i}";

            // 为每个箭头设置不同的动画延迟，创造波浪效果
            float delay = (arrowCount - 1 - i) * animationDelay;
            delays.Add(delay);
        }

        // 存储动画延迟信息
        _arrowAnimationDelays[arrowKey] = delays;
    }

    // 获取世界坐标
    private Vector3 GetWorldPositionFromCellPos(CellPos cellPos)
    {
        Transform cellTransform = GameObject.Find(cellPos.x + "_" + cellPos.y).transform;
        float x = cellTransform.position.x;
        float y = cellTransform.position.y;
        float z = cellTransform.position.z;

        return new Vector3(x, y, z);
    }
}
