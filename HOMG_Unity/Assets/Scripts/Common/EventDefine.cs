using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventDefine
{
    public static readonly string Null = "NULL";

    //控制器事件
    public static readonly string OpenStartView = "OpenStartView";
    public static readonly string CloseStartView = "CloseStartView";

    public static readonly string OpenSettingView = "OpenSettingView";
    public static readonly string CloseSettingView = "CloseSettingView";

    public static readonly string OpenMapView = "OpenMapView";
    public static readonly string CloseMapView = "CloseMapView";

    public static readonly string OpenMainUIView = "OpenMainUIView";
    public static readonly string CloseMainUIView = "CloseMainUIView";

    public static readonly string OpenInGameSettingView = "OpenInGameSettingView";
    public static readonly string CloseInGameSettingView = "CloseInGameSettingView";

    public static readonly string LoadingScene = "LoadingScene";

    public static readonly string CameraMove = "CameraMove";

    public static readonly string QuitGame = "QuitGame";

    public static readonly string OpenAvatarView = "OpenAvatarView";
    public static readonly string CloseAvatarView = "CloseAvatarView";

    public static readonly string OpenCellLandformView = "OpenCellLandformView";
    public static readonly string CloseCellLandformView = "CloseCellLandformView";

    public static readonly string OpenCellUnitView = "OpenCellUnitView";
    public static readonly string CloseCellUnitView = "CloseCellUnitView";

    public static readonly string ClickCell = "ClickCell";

    public static readonly string UpdateCorrection = "UpdateCorrection";
    public static readonly string UpdateIntroduction = "UpdateIntroduction";

    public static readonly string CreateArrow = "CreateArrow";
    public static readonly string DeleteArrow = "DeleteArrow";
}
