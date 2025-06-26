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
public enum SpecialType
{
    military_factory,//军工厂
    civilian_factory,//民用工厂
}

public static class LandformType
{
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

public class MapData
{
    public string MapName;//地图名称
    public int Length;//地图长度
    public int Width;//地图宽度
    public List<CellPos> InVisableCell;//不可见区域
    public Dictionary<CellPos, SpecialType> SpecialCell;//特殊区域
    public Dictionary<CellPos, LandformType.Type> Landform;//地形
    public Dictionary<CellPos, CellPos> River;//河流 在两个Cell之间的河流

    public MapData(int length, int width)
    {
        Length = length;
        Width = width;
        InVisableCell = new List<CellPos>();
        SpecialCell = new Dictionary<CellPos, SpecialType>();
        Landform = new Dictionary<CellPos, LandformType.Type>();
        River = new Dictionary<CellPos, CellPos>();
    }
}
