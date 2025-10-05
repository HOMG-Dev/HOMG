using System.Collections.Generic;

public class FightOperation : IOperation
{
    public int id { get; set; }

    public Division division { get; set; }

    public CellPos srcPos { get; set; }
    public CellPos dstPos { get; set; }

    public void Process(Dictionary<int, Player> players)
    {
        throw new System.NotImplementedException();
    }

}
