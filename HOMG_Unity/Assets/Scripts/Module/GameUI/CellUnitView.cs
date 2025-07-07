using System;
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
    static private float xBias = -100f;
    static private float yBias = -100f;
    Transform _cellUnitPanel;

    //滑动视图相关
    private ScrollRect _scrollRect;
    private Transform _scrollContent;

    //按钮预制体
    public GameObject buttonPrefab;

    //按钮配置
    public float buttonHeight = 30f;

    //监听器相关
    private List<Button> _sellectedButtons = new List<Button>();

    //接口相关
    private List<float> _coordinate = new List<float>();
    private List<Unit> _unitList = new List<Unit>();

    public override void Open(params object[] args)
    {
        if(args.Length < 2)
        {
            Debug.LogWarning("CellUnitView需要至少两个参数");
            return;
        }

        if(args[0] == null)
        {
            Debug.LogWarning("CellUnitView的坐标不能为空");
            _coordinate.Add(2550f);
            _coordinate.Add(1190f);
        }
        else
        {
            _coordinate = args[0] as List<float>;
        }

        _unitList = args[1] as List<Unit>;

        UpdatePosition();
        CreateButtons();
    }

    public override void Close(params object[] args)
    {
        base.Close();

        ClearAllButtons();
    }

    public override void InitData()
    {
        base.InitData();

        _cellUnitPanel = transform.Find("bg");
        buttonPrefab = Resources.Load<GameObject>("Prefab/CellUnitViewButtonPrefab");
        InitScrollView();
    }

    private void UpdatePosition()
    {
        RectTransform cellUnitPanelPosition = _cellUnitPanel.GetComponent<RectTransform>();

        float posX = _coordinate[0] + xBias;
        float posY = _coordinate[1] + yBias;
        cellUnitPanelPosition.anchoredPosition = new Vector2(posX, posY);
    }

    private void InitScrollView()
    {
        _scrollRect = _cellUnitPanel.Find("Scroll View").GetComponent<ScrollRect>();

        _scrollContent = _scrollRect.content.transform;
    }

    // 创建按钮
    public void CreateButton(string buttonText, string unitID)
    {
        GameObject newButton;

        if (buttonPrefab == null)
        {
            Debug.Log("按钮预制体未找到");
        }

        newButton = Instantiate(buttonPrefab, _scrollContent);

        // 设置按钮文本
        Text buttonTextComponent = newButton.GetComponentInChildren<Text>();
        if (buttonTextComponent != null)
        {
            buttonTextComponent.text = buttonText;
        }
        Debug.Log(buttonText);
        newButton.name = unitID;

        // 添加点击事件
        Button buttonComponent = newButton.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => OnButtonClick(buttonComponent));
        }

        // 设置按钮大小
        RectTransform rectTransform = newButton.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, buttonHeight);
    }

    // 批量创建按钮
    public void CreateButtons()
    {
        foreach (var unit in _unitList)
        {
            CreateButton(unit.Type, "1");
        }
    }

    // 清空所有按钮
    public void ClearAllButtons()
    {
        foreach (Transform child in _scrollContent)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnButtonClick(Button button)
    {
        // 处理按钮点击事件（目前尚未实现）
        Debug.Log($"按钮 {button.name} 被点击");
        // ApplyControllerFunc(ControllerType.Game, EventDefine);

        // 高亮按钮
        if(_sellectedButtons.Contains(button) == false)
        {
            _sellectedButtons.Add(button);
        }
        else
        {
            _sellectedButtons.Remove(button);
        }
        HighLightButton(button);
    }

    private void HighLightButton(Button button)
    {
        // 高亮按钮
        if(_sellectedButtons.Contains(button) == true)
        {
            button.GetComponent<Image>().color = Color.green; // 设置高亮颜色
        }
        else
        {
            button.GetComponent<Image>().color = Color.white; // 恢复默认颜色
        }
    }
}
