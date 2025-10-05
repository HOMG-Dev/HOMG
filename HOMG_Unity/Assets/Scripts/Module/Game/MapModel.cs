using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel : BaseModel
{
    public MapData mapData;

    public Dictionary<CellPos, CellData> cellData;

    public Dictionary<int, Player> Players = new Dictionary<int, Player>();

    public int CurrentPlayerId = -1;

    public MapModel(MapData mapData) : base()
    {
        this.mapData = mapData;
        this.cellData = new Dictionary<CellPos, CellData>();
    }
}
