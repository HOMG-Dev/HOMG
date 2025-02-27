using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPos
{
    public int x;
    public int y;

    public CellPos()
    {
        x = 0;
        y = 0;
    }
    public CellPos(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public enum SpecialType
{
    military_factory,//军工厂
    civilian_factory,//民用工厂

}

public enum LandformType
{

}

public class MapData
{
    public string MapName;//地图名称
    public int Length;//地图长度
    public int Width;//地图宽度
    public List<CellPos> InVisableCell;//不可见区域
    public Dictionary<CellPos, SpecialType> SpecialCell;//特殊区域
    public Dictionary<CellPos, LandformType> Landform;//地形
    public Dictionary<CellPos, CellPos> River;//河流 在两个Cell之间的河流

    public MapData(int length, int width)
    {
        Length = length;
        Width = width;
        InVisableCell = new List<CellPos>();
        SpecialCell = new Dictionary<CellPos, SpecialType>();
        Landform = new Dictionary<CellPos, LandformType>();
        River = new Dictionary<CellPos, CellPos>();
    }
}
