using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel : BaseModel
{
    public MapData mapData;

    public Dictionary<CellPos, CellData> cellData;

    public MapModel(MapData mapData) : base()
    {
        this.mapData = mapData;
        this.cellData = new Dictionary<CellPos, CellData>();
    }
}
