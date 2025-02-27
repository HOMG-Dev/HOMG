using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel : BaseModel
{
    public MapData mapData;

    public MapModel(MapData mapData) : base()
    {
        this.mapData = mapData;
    }
}
