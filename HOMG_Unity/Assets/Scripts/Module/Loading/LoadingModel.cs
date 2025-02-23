using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingModel : BaseModel
{
    public string SceneName;//要加载的场景名称
    public System.Action callback;//加载完成回调

    public LoadingModel() : base()
    {

    }
}
