[System.Serializable]
public class Unit
{
    private UnitData _data;
    private string _name;

    public UnitData GetData()
    {
        return _data;
    }

    public string GetName()
    {
        return _name;
    }

    public UnitData UnitData => GetData();
    public string Type => GetData().Type;
    public string Name => GetName();
    public int ATK => GetData().ATK;
    public int DEF => GetData().DEF;

    public Unit(string unitName, string unitType)
    {
        _name = unitName;
        _data = GameApp.ControllerManager.GetController(ControllerType.Game).GetModel<MapModel>().mapData.unitManager.GetUnitData(unitType);
    }
}
