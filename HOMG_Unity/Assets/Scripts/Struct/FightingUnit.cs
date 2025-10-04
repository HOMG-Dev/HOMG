
public class FightingUnit
{
    public Unit unit;
    public bool DEFingRetreat;
    public bool ATKingRetreat;

    public FightingUnit(Unit unit)
    {
        this.unit = unit;
        DEFingRetreat = false;
        ATKingRetreat = false;
    }
}
