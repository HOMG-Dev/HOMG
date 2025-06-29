using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 格子单位信息面板
/// </summary>
public class CellUnitView : BaseView
{
    private float x;
    private float y;
    static private float xBias = -100f;
    static private float yBias = -100f;
    Transform _cellUnitPanel;

    public override void Open(params object[] args)
    {
        base.Open(args);

        if(args[0] == null || args[1] == null)
            Debug.Log("不合法的坐标");

        x = (int)args[0];
        y = (int)args[1];
    }

    public override void InitData()
    {
        base.InitData();

        _cellUnitPanel = transform.Find("bg");
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        InitPosition();
    }

    private void InitPosition()
    {
        RectTransform cellUnitPanelPosition = _cellUnitPanel.GetComponent<RectTransform>();

        float posX = x + xBias;
        float posY = y + yBias;

        cellUnitPanelPosition.anchoredPosition = new Vector2(posX, posY);
    }
}
