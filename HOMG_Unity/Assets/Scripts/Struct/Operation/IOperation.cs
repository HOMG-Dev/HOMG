

using System.Collections.Generic;

public enum OperationType
{
    Move,
    Attack,
    //...
}

public interface IOperation
{

    int id { get; set ; }

    void Process(Dictionary<int, Player> players);

}


