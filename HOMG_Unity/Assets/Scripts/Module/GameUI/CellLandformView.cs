using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 地形信息面板
/// </summary>
public class CellLandformView : BaseView
{
    private string _landformType;
    private List<int> _correctionList = new List<int>();

    // UI组件引用
    private Text _attackingATKText;
    private Text _defendingATKText;
    private Text _attackingDEFText;
    private Text _defendingDEFText;
    private Text _landformNameText;

    private Image _landformImage;

    protected override void OnAwake()
    {
        base.OnAwake();

        // 获取UI组件引用
        InitializeUIComponents();
    }

    private void InitializeUIComponents()
    {
        //获取Text组件
        Transform attackingArea = transform.Find("bg/AttackingArea");
        Transform defendingArea = transform.Find("bg/DefendingArea");

        _landformNameText = Find("bg/LandformName")?.GetComponent<Text>();

        if (attackingArea != null)
        {
            _attackingATKText = attackingArea.Find("atkText")?.GetComponent<Text>();
            _attackingDEFText = attackingArea.Find("defText")?.GetComponent<Text>();
        }

        if (defendingArea != null)
        {
            _defendingATKText = defendingArea.Find("atkText")?.GetComponent<Text>();
            _defendingDEFText = defendingArea.Find("defText")?.GetComponent<Text>();
        }

        // 检查组件是否找到
        if (_attackingATKText == null)
            Debug.LogError("atkText component not found!");
        if (_attackingDEFText == null)
            Debug.LogError("atkText component not found!");
        if (_defendingATKText == null)
            Debug.LogError("atkText component not found!");
        if (_defendingDEFText == null)
            Debug.LogError("defText component not found!");

        _landformImage = Find("bg/LandformImage")?.GetComponent<Image>();

        if (_landformImage == null)
        {

            Debug.LogWarning("LandformImage not found in bg, consider adding it to the hierarchy");
        }
    }

    public override void Open(System.Object[] args)
    {
        // args[0] 是地形名称，args[1] 是修正列表
        // args[1] 中分别存储 攻击时的进攻/防御修正 和 防御时的进攻/防御修正

        if (args == null || args.Length < 2)
        {
            Debug.LogError("CellLandformView.Open: Invalid arguments!");
            return;
        }

        // 获取地形名称
        _landformType = args[0] as string;

        // 获取修正列表
        if (args[1] is List<int> correctionList)
        {
            _correctionList = correctionList;
        }
        else if (args[1] is int[] correctionArray)
        {
            _correctionList = new List<int>(correctionArray);
        }
        else
        {
            Debug.LogError("CellLandformView.Open: Invalid correction data type!");
            return;
        }

        // 更新UI显示
        UpdateLandformImage();
        UpdateCorrectionDisplay();
    }

    private void UpdateCorrectionDisplay()
    {
        if (_landformNameText != null)
        {
            _landformNameText.text = _landformType;
        }

        if (_correctionList.Count >= 2)
        {
            int attackingATKCorrection = _correctionList[0];
            int attackingDEFCorrection = _correctionList[1];
            int defendingATKCorrection = _correctionList[2];
            int defendingDEFCorrection = _correctionList[3];

            if (_attackingATKText != null)
            {
                _attackingATKText.text = FormatCorrectionText(attackingATKCorrection);
            }

            if (_attackingDEFText != null)
            {
                _attackingDEFText.text = FormatCorrectionText(attackingDEFCorrection);
            }

            if (_defendingATKText != null)
            {
                _defendingATKText.text = FormatCorrectionText(defendingATKCorrection);
            }

            if (_defendingDEFText != null)
            {
                _defendingDEFText.text = FormatCorrectionText(defendingDEFCorrection);
            }
        }
        else
        {
            Debug.LogWarning("CellLandformView: Insufficient correction data!");
        }
    }

    private string FormatCorrectionText(int correction)
    {
        // 格式化修正值显示，正数显示+号，负数自动显示-号
        if (correction > 0)
            return "+" + correction.ToString();
        else
            return correction.ToString();
    }

    private void UpdateLandformImage()
    {
        if (_landformImage == null || string.IsNullOrEmpty(_landformType))
            return;

        // 根据地形名称加载对应的图片
        Sprite landformSprite = LoadLandformSprite(_landformType);
        if (landformSprite != null)
        {
            _landformImage.sprite = landformSprite;
        }
        else
        {
            Debug.LogWarning($"No sprite found for landform type: {_landformType}");
        }
    }

    private Sprite LoadLandformSprite(string landformType)
    {
        //从Resources文件夹加载
        string spritePath = $"Landforms/{landformType}";
        Sprite sprite = Resources.Load<Sprite>(spritePath);

        if (sprite != null)
            return sprite;
        else
            return null;
    }
}
