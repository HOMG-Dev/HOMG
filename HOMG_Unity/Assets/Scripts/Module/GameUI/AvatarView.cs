using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 头像信息面板
/// </summary>
public class AvatarView : BaseView
{
    private ContentType _currentContentType = ContentType.None;
    private List<int> _correctionList = new List<int>();
    private Text _correctionText;
    private ScrollRect _scroll;
    private RectTransform _contentRT;

    public enum ContentType
    {
        None,
        Correction,
        Introduction
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        //初始化按钮
        InitBtn();
        //获取修正文本组件
        InitCorrectionText();
    }

    //初始化按钮
    private void InitBtn()
    {
        Find<Button>("OptionPanel/CorrectionBtn").onClick.AddListener(onCorrectionBtn);
        Find<Button>("OptionPanel/IntroductionBtn").onClick.AddListener(onIntroductionBtn);
    }

    private void InitCorrectionText()
    {
        _scroll = Find<ScrollRect>("ContentArea/CorrectionContent");
        _contentRT = Find<RectTransform>("ContentArea/CorrectionContent/CorrectionText/Content");

        //获取修正文本组件
        _correctionText = Find<Text>("ContentArea/CorrectionContent/CorrectionText/Content");
        if (_correctionText == null)
        {
            Debug.LogError("CorrectionText not found in CorrectionContent!");
        }
    }

    public override void InitData()
    {
        base.InitData();
        //默认显示修正内容
        ShowContent(ContentType.Correction);

        //示例
        AddCorrectionText("666");
        AddCorrectionText("123");
        AddCorrectionText("999");
    }

    //修正按钮
    private void onCorrectionBtn()
    {
        ShowContent(ContentType.Correction);
    }

    //简介按钮
    private void onIntroductionBtn()
    {
        ShowContent(ContentType.Introduction);
    }

    //显示对应内容
    private void ShowContent(ContentType contentType)
    {
        //先隐藏所有内容
        HideAllContent();

        //显示对应的内容
        switch(contentType)
        {
            case ContentType.Correction:
                Find<Transform>("ContentArea/CorrectionContent").gameObject.SetActive(true);
                _currentContentType = ContentType.Correction;
                break;

            case ContentType.Introduction:
                Find<Transform>("ContentArea/IntroductionContent").gameObject.SetActive(true);
                _currentContentType = ContentType.Introduction;
                break;
        }

        //显示内容区域
        Find<Transform>("ContentArea").gameObject.SetActive(true);
    }

    //隐藏所有内容
    private void HideAllContent()
    {
        Find<Transform>("ContentArea/CorrectionContent").gameObject.SetActive(false);
        Find<Transform>("ContentArea/IntroductionContent").gameObject.SetActive(false);
        Find<Transform>("ContentArea").gameObject.SetActive(false);
        _currentContentType = ContentType.None;
    }

    //获取当前显示的内容类型
    public ContentType GetCurrentContentType()
    {
        return _currentContentType;
    }

    //修正文本相关操作
    public void AddCorrectionText(string correctionText)
    {
        if (_correctionText == null)
            return;

        // 如果当前文本不为空，添加换行符
        if (string.IsNullOrEmpty(_correctionText.text) == false)
        {
            _correctionText.text += "\n" + correctionText;
        }
        else
        {
            _correctionText.text = correctionText;
        }
        StartCoroutine(LateAdjust());
    }

    //设置修正文本内容（替换所有内容）
    public void SetCorrectionText(string correctionText)
    {
        if (_correctionText == null)
            return;

        _correctionText.text = correctionText;
    }

    /// 清空修正文本
    public void ClearCorrectionText()
    {
        if (_correctionText == null)
            return;

        _correctionText.text = "";
    }

    /// 获取当前修正文本内容
    public string GetCorrectionText()
    {
        if (_correctionText == null)
            return "";

        return _correctionText.text;
    }

    IEnumerator LateAdjust(){
        // 等一帧 或者等 Layout rebuild
        yield return null;

        // 根据 Text 的 PreferredHeight 调整 content
        float h = _correctionText.preferredHeight;
        var size = _contentRT.sizeDelta;
        _contentRT.sizeDelta = new Vector2(size.x, h);

        // 再根据比例更新滑块长度
        if (_scroll.verticalScrollbar != null){
            float vpH = (_scroll.viewport as RectTransform).rect.height;
            _scroll.verticalScrollbar.size = Mathf.Clamp01(vpH / h);
        }
    }
}
