
using System.Collections.Generic;

public class Division
{
    private Player _owner;
    private List<FightingUnit> _units;

    public int ATK;
    public int DEF;

    public Cost ATKCost;

    public Division(Player _owner, List<Unit> _units)
    {
        this._owner = _owner;
        this._units = new List<FightingUnit>();
        foreach (Unit unit in _units)
        {
            this._units.Add(new FightingUnit(unit));
        }
        CalcUnits();
    }

    private void CalcUnits()
    {
        int atk = 0;
        int def = 0;
        Cost _ATKCostcost = new Cost(0, 0);

        foreach (FightingUnit unit in _units)
        {
            _ATKCostcost += unit.unit.UnitData.ATKCost;
        }

        ATK = atk;
        DEF = def;

        ATKCost = _ATKCostcost;
    }

    public List<FightingUnit> GetFightingUnits() => _units;

    public Player GetOwner() => _owner;

}
