using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIView : BaseView
{
    private bool _isOpeningAvatar = false;
    private Introduction _introduction;
    private Text _militaryFactory;
    private Text _civiliFactory;
    private Text _soldierNum;

    public override void InitData()
    {
        base.InitData();

        //初始化按钮
        InitBtn();
        //获取文本组件
        _militaryFactory = Find("militaryFactory/txt")?.GetComponent<Text>();
        _civiliFactory = Find("civilFactory/txt")?.GetComponent<Text>();
        _soldierNum = Find("soldierNum/txt")?.GetComponent<Text>();

        if(_militaryFactory == null || _civiliFactory == null || _soldierNum == null)
        {
            Debug.LogWarning("Text components not found in GameUIView!");
        }

        GameApp.ControllerManager.GetController(ControllerType.Game).RegisterFunc(EventDefine.UpdateIntroduction, UpdateIntroduction);
    }

    public override void Open(System.Object[] args)
    {
        UpdateIntroduction(args);
    }

    private void onAvatarView()
    {
        if (_isOpeningAvatar == true)
        {
            closeAvatarView();
            _isOpeningAvatar = false;
        }
        else
        {
            openAvatarView();
            _isOpeningAvatar = true;
        }
    }

    private void openAvatarView()
    {
        //打开头像视图
        ApplyFunc(EventDefine.OpenAvatarView);
    }

    private void closeAvatarView()
    {
        //打开头像视图
        ApplyFunc(EventDefine.CloseAvatarView);
    }

    private void InitBtn()
    {
        //Find<Button>("bg/settingBtn").onClick.AddListener(onSettingBtn);
        Find<Button>("avatar").onClick.AddListener(onAvatarView);
    }

    private void UpdateIntroduction(System.Object[] args)
    {
        if(args.Length < 1)
        {
            Debug.LogWarning("Introduction args is null");
            _introduction = new Introduction("0", "0", "0", "0", "0", "0");
        }
        else
        {
            _introduction = args[0] as Introduction;
        }

        _militaryFactory.text = "军 " + _introduction.usedMilitaryFactory + "/" + _introduction.totalMilitaryFactory;
        _civiliFactory.text = "民 " + _introduction.usedCivilFactory + "/" + _introduction.totalCivilFactory;
        _soldierNum.text = "兵 " + _introduction.usedUnit + "/" + _introduction.totalUnit;
    }
}
