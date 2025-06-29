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
public static class UnitType
{
    public enum Type
    {
        Undefined,
        Infantry,
        Artillery,
        Tank,
    }

    public readonly struct UnitTypeStats
    {
        public readonly int attack;
        public readonly int defense;

        public UnitTypeStats(int attack, int defense)
        {
            this.attack = attack;
            this.defense = defense;
        }
    }

    private static readonly Dictionary<Type, UnitTypeStats> _unitTypeStatsDictionary = new Dictionary<Type, UnitTypeStats>
    {
        { Type.Infantry, new UnitTypeStats(attack: 1, defense: 1) },
        { Type.Artillery, new UnitTypeStats(attack: 3, defense: 0) },
        { Type.Tank, new UnitTypeStats(attack: 3, defense: 3) },
    };

    public static UnitTypeStats Infantry => _unitTypeStatsDictionary[Type.Infantry];

    public static UnitTypeStats Artillery => _unitTypeStatsDictionary[Type.Artillery];

    public static UnitTypeStats Tank => _unitTypeStatsDictionary[Type.Tank];

    public static UnitTypeStats GetUnitTypeStats(UnitType.Type type)
    {
        return _unitTypeStatsDictionary[type];
    }
}

[System.Serializable]
public enum SpecialType
{
    military_factory,//军工厂
    civilian_factory,//民用工厂
}

[System.Serializable]
public static class LandformType
{
    [System.Serializable]
    public enum Type
    {
        Undefined,
        Plain,
        Mountain,
    }

    public readonly struct LandformTypeCorrection
    {
        public readonly int attackingATKCorrection; //作为进攻方战斗时获得的攻击值修正
        public readonly int attackingDEFCorrection; //作为进攻方战斗时获得的防御值修正

        public readonly int defendingATKCorrection; //作为防御方战斗时获得的攻击值修正
        public readonly int defendingDEFCorrection; //作为防御方战斗时获得的防御值修正

        public LandformTypeCorrection(int attackingATKCorrection, int attackingDEFCorrection, int defendingATKCorrection, int defendingDEFCorrection)
        {
            this.attackingATKCorrection = attackingATKCorrection;
            this.attackingDEFCorrection = attackingDEFCorrection;

            this.defendingATKCorrection = defendingATKCorrection;
            this.defendingDEFCorrection = defendingDEFCorrection;
        }
    }

    private static readonly Dictionary<Type, LandformTypeCorrection> _landformCorrectionDictionary = new Dictionary<Type, LandformTypeCorrection>
    {
        { Type.Plain, new LandformTypeCorrection(attackingATKCorrection: 0, attackingDEFCorrection: 0, defendingATKCorrection: 0, defendingDEFCorrection: 0) },
        { Type.Mountain, new LandformTypeCorrection(attackingATKCorrection: -1, attackingDEFCorrection: 0, defendingATKCorrection: 0, defendingDEFCorrection: 0) },
    };

    public static LandformTypeCorrection Plain => _landformCorrectionDictionary[Type.Plain];

    public static LandformTypeCorrection Mountain => _landformCorrectionDictionary[Type.Mountain];

    public static LandformTypeCorrection GetLandformTypeCorrection(LandformType.Type type)
    {
        return _landformCorrectionDictionary[type];
    }
}

[System.Serializable]
public class Landform
{
    private LandformType.Type _landformType;

    public Landform(LandformType.Type landformType)
    {
        _landformType = landformType;
    }

    public LandformType.Type Type()
    {
        return _landformType;
    }

    public String TypeName()
    {
        // 做一个一一对应的转换
        return _landformType switch
        {
            LandformType.Type.Plain => "平原",
            LandformType.Type.Mountain => "山地",
            _ => "未知地形"
        };
    }

    public List<int> GetCorrectionList()
    {
        LandformType.LandformTypeCorrection correction = LandformType.GetLandformTypeCorrection(Type());
        return new List<int>
        {
            correction.attackingATKCorrection,
            correction.attackingDEFCorrection,
            correction.defendingATKCorrection,
            correction.defendingDEFCorrection
        };
    }

    public int AttackingATKCorrection()
    {
        return LandformType.GetLandformTypeCorrection(Type()).attackingATKCorrection;
    }

    public int AttackingDEFCorrection()
    {
        return LandformType.GetLandformTypeCorrection(Type()).attackingDEFCorrection;
    }

    public int DefendingATKCorrection()
    {
        return LandformType.GetLandformTypeCorrection(Type()).defendingATKCorrection;
    }

    public int DefendingDEFCorrection()
    {
        return LandformType.GetLandformTypeCorrection(Type()).defendingDEFCorrection;
    }
}

[System.Serializable]
public class Unit
{
    private UnitType.Type _type;
    private int _ID;
    private static int _IDCounter = 0;

    public UnitType.Type Type()
    {
        return _type;
    }

    private int NewID()
    {
        return ++_IDCounter;
    }

    public int ID()
    {
        return _ID;
    }

    public static int IDCounter()
    {
        return _IDCounter;
    }

    public Unit()
    {
        _ID = NewID();
        _type = UnitType.Type.Undefined;
    }

    public Unit(UnitType.Type unitType)
    {
        _ID = NewID();
        _type = unitType;
    }

    public int Attack()
    {
        return UnitType.GetUnitTypeStats(Type()).attack;
    }

    public int Defense()
    {
        return UnitType.GetUnitTypeStats(Type()).defense;
    }
}


public class Team
{
    public string Name;//队伍名称
    public int ID;//队伍ID
    public string TeamIntro;//队伍介绍
    public string CombatObjective;//战斗目标
    public List<Unit> Units;//队伍中的单位

    public Team(string name, int id)
    {
        Name = name;
        ID = id;
        Units = new List<Unit>();
    }
}


[System.Serializable]
public class MapData
{
    public string MapName;//地图名称
    public int Length;//地图长度
    public int Width;//地图宽度
    public List<CellPos> InVisableCell;//不可见区域
    public Dictionary<CellPos, SpecialType> SpecialCell;//特殊区域
    public Dictionary<CellPos, Landform> Landform;//地形
    public Dictionary<CellPos, CellPos> River;//河流 在两个Cell之间的河流
    public Dictionary<int, Team> Teams;//地图上的所有队伍

    public MapData(int length, int width)
    {
        Length = length;
        Width = width;
        InVisableCell = new List<CellPos>();
        SpecialCell = new Dictionary<CellPos, SpecialType>();
        Landform = new Dictionary<CellPos, Landform>();
        River = new Dictionary<CellPos, CellPos>();
    }
}
