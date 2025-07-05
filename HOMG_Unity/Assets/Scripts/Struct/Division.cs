
using System.Collections.Generic;

public class Division
{
    private List<Unit> _units;

    public int ATK;
    public int DEF;

    public Cost ATKCost;

    public Division(List<Unit> _units)
    {
        this._units = _units;
        CalcUnits();
    }

    private void CalcUnits()
    {
        int atk = 0;
        int def = 0;
        Cost _ATKCostcost = new Cost(0, 0);

        foreach (Unit unit in _units)
        {
            _ATKCostcost += unit.UnitData.ATKCost;
        }

        ATK = atk;
        DEF = def;

        ATKCost = _ATKCostcost;
    }

}
