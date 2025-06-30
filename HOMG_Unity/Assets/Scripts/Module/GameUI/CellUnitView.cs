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
    //滑动视图相关
    private ScrollRect _scrollRect;
    private Transform _scrollContent;

    //按钮预制体
    public GameObject buttonPrefab;

    //按钮配置
    public float buttonHeight = 30f;

    //监听器相关
    private List<Button> _sellectedButtons = new List<Button>();

    public override void Open(params object[] args)
    {
        base.Open(args);

        if(args[0] == null || args[1] == null)
            Debug.Log("不合法的坐标");
        else
        {
            x = (int)args[0];
            y = (int)args[1];
        }

        UpdatePosition();
    }

    public override void InitData()
    {
        base.InitData();

        _cellUnitPanel = transform.Find("bg");
        buttonPrefab = Resources.Load<GameObject>("Prefab/CellUnitViewButtonPrefab");
        InitScrollView();

        // 示例按钮
        CreateTestButtons();
    }

    private void UpdatePosition()
    {
        RectTransform cellUnitPanelPosition = _cellUnitPanel.GetComponent<RectTransform>();

        float posX = x + xBias;
        float posY = y + yBias;
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
    public void CreateButtons(List<ButtonInfo> buttonInfos)
    {
        foreach (var buttonInfo in buttonInfos)
        {
            CreateButton(buttonInfo.text, buttonInfo.ID);
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

    // 测试按钮
    private void CreateTestButtons()
    {
        // 创建一些测试按钮
        for (int i = 0; i < 10; i++)
        {
            int index = i; // 闭包变量
            CreateButton($"按钮 {i + 1}", $"{i}");
        }
    }

    private void OnButtonClick(Button button)
    {
        // 处理按钮点击事件
        Debug.Log($"按钮 {button.name} 被点击");

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

/// <summary>
/// 按钮信息结构体
/// </summary>
[System.Serializable]
public class ButtonInfo
{
    public string text;
    public string ID;

    public ButtonInfo(string text, string ID)
    {
        this.text = text;
        this.ID = ID;
    }
}
