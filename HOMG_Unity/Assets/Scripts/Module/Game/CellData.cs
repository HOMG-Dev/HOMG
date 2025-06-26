using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
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
    public int Attack() { return UnitType.GetUnitTypeStats(Type()).attack;}
    public int Defense() { return UnitType.GetUnitTypeStats(Type()).defense;}
}
public static class LandFormType
{
    public enum Type
    {
        Undefined,
        Plain,
        Mountain,
    }

    public readonly struct LandFormTypeCorrection
    {
        public readonly int attackingATKCorrection; //作为进攻方战斗时获得的攻击值修正
        public readonly int attackingDEFCorrection; //作为进攻方战斗时获得的防御值修正

        public readonly int defendingATKCorrection; //作为防御方战斗时获得的攻击值修正
        public readonly int defendingDEFCorrection; //作为防御方战斗时获得的防御值修正

        public LandFormTypeCorrection(int attackingATKCorrection, int attackingDEFCorrection, int defendingATKCorrection, int defendingDEFCorrection)
        {
            this.attackingATKCorrection = attackingATKCorrection;
            this.attackingDEFCorrection = attackingDEFCorrection;

            this.defendingATKCorrection = defendingATKCorrection;
            this.defendingDEFCorrection = defendingDEFCorrection;
        }
    }

    private static readonly Dictionary<Type, LandFormTypeCorrection> _landFormCorrectionDictionary = new Dictionary<Type, LandFormTypeCorrection>
    {
        { Type.Plain, new LandFormTypeCorrection(attackingATKCorrection: 0, attackingDEFCorrection: 0, defendingATKCorrection: 0, defendingDEFCorrection: 0) },
        { Type.Mountain, new LandFormTypeCorrection(attackingATKCorrection: -1, attackingDEFCorrection: 0, defendingATKCorrection: 0, defendingDEFCorrection: 0) },
    };

    public static LandFormTypeCorrection Plain => _landFormCorrectionDictionary[Type.Plain];
    public static LandFormTypeCorrection Mountain => _landFormCorrectionDictionary[Type.Mountain];
    public static LandFormTypeCorrection GetLandFormTypeCorrection(LandFormType.Type type)
    {
        return _landFormCorrectionDictionary[type];
    }
}
public class LandForm
{
    private LandFormType.Type _landFormType;
    public LandForm(LandFormType.Type landFormType)
    {
        _landFormType = landFormType;
    }
    public LandFormType.Type Type()
    {
        return _landFormType;
    }
    public int AttackingATKCorrection()
    {
        return LandFormType.GetLandFormTypeCorrection(Type()).attackingATKCorrection;
    }
    public int AttackingDEFCorrection()
    {
        return LandFormType.GetLandFormTypeCorrection(Type()).attackingDEFCorrection;
    }
    public int DefendingATKCorrection()
    {
        return LandFormType.GetLandFormTypeCorrection(Type()).defendingATKCorrection;
    }
    public int DefendingDEFCorrection()
    {
        return LandFormType.GetLandFormTypeCorrection(Type()).defendingDEFCorrection;
    }
}

public class CellData
{
    private CellPos _cellPos;
    private List<SpecialType> _specialTypeList;
    private LandForm _landForm;
    private List<Unit> _unitList;
    public void Init(CellPos cellPos,LandFormType.Type landFormType)
    {
        _cellPos = cellPos;
        _specialTypeList = new List<SpecialType>();
        _landForm = new LandForm(landFormType);
        _unitList = new List<Unit>();
    }

    public CellData(CellPos cellPos,LandFormType.Type landFormType)
    {
        _cellPos = cellPos;
        Init(cellPos, landFormType);
    }
    public CellData(int x,int y, LandFormType.Type landFormType)
    {
        _cellPos = new CellPos(x,y);
        Init(new CellPos(x,y),landFormType);
    }
}
