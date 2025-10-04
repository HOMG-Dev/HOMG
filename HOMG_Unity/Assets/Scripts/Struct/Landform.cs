using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Landform
{
    private string _landformType;
    private int _attackingATKCorrection;
    private int _attackingDEFCorrection;
    private int _defendingATKCorrection;
    private int _defendingDEFCorrection;
    private string _modelPath;

    public string GetModelPath()
    {
        return _modelPath;
    }

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
    public Landform(string landformType, int attackingATKCorrection, int attackingDEFCorrection, int defendingATKCorrection, int defendingDEFCorrection)
    {
        _landformType = landformType;
        _attackingATKCorrection = attackingATKCorrection;
        _attackingDEFCorrection = attackingDEFCorrection;
        _defendingATKCorrection = defendingATKCorrection;
        _defendingDEFCorrection = defendingDEFCorrection;
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
}
