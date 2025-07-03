using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[System.Serializable]
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

    // 重写 Equals 方法，用于比较 CellPos 对象是否相等
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        CellPos other = (CellPos)obj;
        return x == other.x && y == other.y;
    }

    // 重写 GetHashCode 方法，与 Equals 配合使用
    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }
}

[System.Serializable]
public enum SpecialType
{
    military_factory,//军工厂
    civilian_factory,//民用工厂
}

[System.Serializable]
public class MapData
{
    public string MapName;//地图名称
    public int Length;//地图长度
    public int Width;//地图宽度
    public List<CellPos> InVisableCell;//不可见区域
    public Dictionary<CellPos, SpecialType> SpecialCell;//特殊区域
    public Dictionary<CellPos, Landform> Landform;//每一个cell的地形
    public Dictionary<CellPos, CellPos> River;//河流 在两个Cell之间的河流
    public LandformManager landformManager;
    public UnitManager unitManager;

    public MapData(int length, int width)
    {
        Length = length;
        Width = width;
        InVisableCell = new List<CellPos>();
        SpecialCell = new Dictionary<CellPos, SpecialType>();
        Landform = new Dictionary<CellPos, Landform>();
        River = new Dictionary<CellPos, CellPos>();
        landformManager = new LandformManager();
        unitManager = new UnitManager();
    }
}
