using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public class Data
    {
        private string _unitType;
        private int _atk;
        private int _def;

        public string GetUnitType()
        {
            return _unitType;
        }

        public int GetATK()
        {
            return _atk;
        }

        public int GetDEF()
        {
            return _def;
        }

        public string Type => GetUnitType();
        public int ATK => GetATK();
        public int DEF => GetDEF();

        public Data(string unitType,int atk,int def)
        {
            _unitType = unitType;
            _atk = atk;
            _def = def;
        }
    }

    private static List<Data> DataList;

    private int _typeIndex;

    public static void Init()
    {
        DataList = new List<Data>();
    }

    public Data GetData()
    {
        return GetData(_typeIndex);
    }

    public static Data GetData(int typeIndex)
    {
        if (typeIndex < 0 || typeIndex >= DataList.Count)
        {
            Debug.Log("试图使用无效的index("+ typeIndex + ")来寻找Unit.Data!");
            return null;
        }
        return DataList[typeIndex];
    }

    public static Data GetData(string unitType)
    {
        for (int i = 0; i < DataList.Count; ++i)
        {
            if (DataList[i].Type == unitType)
            {
                return GetData(i);
            }
        }
        Debug.Log("试图使用不存在的unitType(" + unitType + ")来寻找Unit.Data!");
        return null;
    }

    public static bool AddType(Data unitData)
    {
        DataList.Add(unitData);
        return true;
    }

    public static bool AddType(string unitType,int atk,int def)
    {
        return AddType(new Data(unitType, atk, def));
    }

    public Data UnitData => GetData();
    public string Type => GetData().Type;
    public int ATK => GetData().ATK;
    public int DEF => GetData().DEF;

    static Unit()
    {
        Init();
    }
    public Unit(int typeIndex)
    {
        if (typeIndex < 0 || typeIndex >= DataList.Count)
        {
            Debug.Log("试图使用无效的index(" + typeIndex + ")作为Unit的构造函数的参数!");
            _typeIndex = -1;
            return;
        }
        _typeIndex = typeIndex;
    }
    public Unit(Data data)
    {
        bool check = false;
        for (int i = 0;i < DataList.Count; ++i)
        {
            if (DataList[i] == data)
            {
                _typeIndex = i;
                check = true;
                break;
            }
        }
        if (!check)
        {
            Debug.Log("试图使用不存在的Unit.Data(Type=" + data.Type + ")作为Unit的构造函数的参数!");
            _typeIndex = -1;
        }
    }
    public Unit(string unitType)
    {
        bool check = false;
        for (int i = 0; i < DataList.Count; ++i)
        {
            if (DataList[i].Type == unitType)
            {
                _typeIndex = i;
                check = true;
                break;
            }
        }
        if (!check)
        {
            Debug.Log("试图使用不存在的Unit.Type(" + unitType + ")作为Unit的构造函数的参数!");
            _typeIndex = -1;
        }
    }
}

[System.Serializable]
public class Landform
{
    public class Data
    {
        private string _landformType;
        private int _attackingATKCorrection;
        private int _attackingDEFCorrection;
        private int _defendingATKCorrection;
        private int _defendingDEFCorrection;

        public string GetLandformType()
        {
            return _landformType;
        }

        public int GetAttackingATKCorrection()
        {
            return _attackingATKCorrection;
        }

        public int GetAttackingDEFCorrection()
        {
            return _attackingDEFCorrection;
        }

        public int GetDefendingATKCorrection()
        {
            return _defendingATKCorrection;
        }

        public int GetDefendingDEFCorrection()
        {
            return _defendingDEFCorrection;
        }
        public string Type => GetLandformType();
        public int AttackingATKCorrection => GetAttackingATKCorrection();
        public int AttackingDEFCorrection => GetAttackingDEFCorrection();
        public int DefendingATKCorrection => GetDefendingATKCorrection();
        public int DefendingDEFCorrection => GetDefendingDEFCorrection();
        public Data(string landformType, int attackingATKCorrection, int attackingDEFCorrection,int defendingATKCorrection,int defendingDEFCorrection)
        {
            _landformType = landformType;
            _attackingATKCorrection = attackingATKCorrection;
            _attackingDEFCorrection = attackingDEFCorrection;
            _defendingATKCorrection = defendingATKCorrection;
            _defendingDEFCorrection = defendingDEFCorrection;
        }
    }

    private static List<Data> DataList;

    private int _typeIndex;

    public static void Init()
    {
        DataList = new List<Data>();
        /* 测试代码开始 */
        AddType("Plain", 0, 0, 0, 0);
        AddType("Mountain", -1, 0, 0, 0);
        /* 测试代码结束 */
    }

    public Data GetData()
    {
        return GetData(_typeIndex);
    }

    public static Data GetData(int typeIndex)
    {
        if (typeIndex < 0 || typeIndex >= DataList.Count)
        {
            Debug.Log("试图使用无效的index(" + typeIndex + ")来寻找Landform.Data!");
            return null;
        }
        return DataList[typeIndex];
    }

    public static Data GetData(string landformType)
    {
        for (int i = 0; i < DataList.Count; ++i)
        {
            if (DataList[i].Type == landformType)
            {
                return GetData(i);
            }
        }
        Debug.Log("试图使用不存在的LandformName(" + landformType + ")来寻找Landform.Data!");
        return null;
    }

    public static bool AddType(Data landformData)
    {
        DataList.Add(landformData);
        return true;
    }

    public static bool AddType(string landformType, int attackingATKCorrection, int attackingDEFCorrection,int defendingATKCorrection,int defendingDEFCorrection)
    {
        return AddType(new Data(landformType, attackingATKCorrection, attackingDEFCorrection, defendingATKCorrection, defendingDEFCorrection));
    }

    public List<int> GetCorrectionList()
    {
        List<int> returnList = new List<int>();
        returnList.Add(AttackingATKCorrection);
        returnList.Add(AttackingDEFCorrection);
        returnList.Add(DefendingATKCorrection);
        returnList.Add(DefendingDEFCorrection);
        return returnList;
    }

    public Data LandformData => GetData();
    public string Type => GetData().Type;
    public int AttackingATKCorrection => GetData().AttackingATKCorrection;
    public int AttackingDEFCorrection => GetData().AttackingDEFCorrection;
    public int DefendingATKCorrection => GetData().DefendingATKCorrection;
    public int DefendingDEFCorrection => GetData().DefendingDEFCorrection;

    static Landform()
    {
        Init();
    }

    public Landform(int typeIndex)
    {
        if (typeIndex < 0 || typeIndex >= DataList.Count)
        {
            Debug.Log("试图使用无效的index(" + typeIndex + ")作为Landform的构造函数的参数!");
            _typeIndex = -1;
            return;
        }
        _typeIndex = typeIndex;
    }

    public Landform(Data data)
    {
        bool check = false;
        for (int i = 0; i < DataList.Count; ++i)
        {
            if (DataList[i] == data)
            {
                _typeIndex = i;
                check = true;
                break;
            }
        }
        if (!check)
        {
            Debug.Log("试图使用不存在的Landform.Data(Type=" + data.Type + ")作为Landform的构造函数的参数!");
            _typeIndex = -1;
        }
    }

    public Landform(string landformType)
    {
        bool check = false;
        for (int i = 0; i < DataList.Count; ++i)
        {
            if (DataList[i].Type == landformType)
            {
                _typeIndex = i;
                check = true;
                break;
            }
        }
        if (!check)
        {
            Debug.Log("试图使用不存在的Landform.Type(" + landformType + ")作为Landform的构造函数的参数!");
            _typeIndex = -1;
        }
    }
}

[System.Serializable]
public class CellData
{
    private CellPos _cellPos;
    private List<SpecialType> _specialTypeList;
    private Landform _landform;
    private List<Unit> _unitList;

    public void Init(CellPos cellPos, Landform.Data landformData)
    {
        _cellPos = cellPos;
        _specialTypeList = new List<SpecialType>();
        _landform = new Landform(landformData);
        _unitList = new List<Unit>();
    }

    public CellData(CellPos cellPos, Landform.Data landformData)
    {
        _cellPos = cellPos;
        Init(cellPos, landformData);
    }

    public CellData(int x, int y, Landform.Data landformData)
    {
        _cellPos = new CellPos(x, y);
        Init(new CellPos(x, y), landformData);
    }
}
