using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 头像信息面板
/// </summary>
public class AvatarView : BaseView
{
    private ContentType _currentContentType;
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

    public override void Open(System.Object[] args)
    {
        UpdateAvatarViewCorrection(args);

        //默认显示修正
        ShowContent(ContentType.Correction);
    }

    //初始化按钮
    private void InitBtn()
    {
        Find<Button>("OptionPanel/CorrectionBtn").onClick.AddListener(onCorrectionBtn);
        Find<Button>("OptionPanel/IntroductionBtn").onClick.AddListener(onIntroductionBtn);
    }

    private void InitCorrectionText()
    {
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
        //初始化按钮
        InitBtn();
        //获取修正文本组件
        InitCorrectionText();

        GameApp.ControllerManager.GetController(ControllerType.Game).RegisterFunc(EventDefine.UpdateCorrection, UpdateAvatarViewCorrection);
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
    }

    //设置修正文本内容（替换所有内容）
    public void SetCorrectionText()
    {
        if (_correctionText == null)
            return;

        foreach (var x in _correctionList)
        {
            // 需要字典的实现
            AddCorrectionText("");
        }
    }

    // 清空修正文本
    public void ClearCorrectionText()
    {
        if (_correctionText == null)
            return;

        _correctionText.text = "";
    }

    // 简介相关操作（未实现）


    // 更新修正内容
    private void UpdateAvatarViewCorrection(System.Object[] args)
    {
        if (args == null || args.Length < 1)
        {
            Debug.LogWarning("UpdateAvatarViewCorrection: No correction data provided!");
            _correctionList = new List<int>(1);
        }
        else
        {
            _correctionList = args[0] as List<int>;
        }

        ClearCorrectionText();

        SetCorrectionText();
    }
}
