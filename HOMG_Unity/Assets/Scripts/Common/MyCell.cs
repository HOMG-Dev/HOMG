using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCell : MonoBehaviour
{
    //鼠标放上去的颜色
    public Color mouseEnterColor;
    //鼠标离开的颜色
    public Color mouseExitColor;
    //鼠标按下的颜色
    public Color mouseDownColor;

    public void Highlight()
    {
        mouseEnterColor.a = 0.4f;
        GetComponent<Renderer>().material.color = mouseEnterColor;
    }

    public void Lowlight()
    {
        GetComponent<Renderer>().material.color = mouseExitColor;
    }

    public void OnMouseDown()
    {
        GetComponent<Renderer>().material.color = mouseDownColor;
    }
}
