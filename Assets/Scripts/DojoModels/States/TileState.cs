using Dojo;
using System;

public class TileState : ModelInstance
{
    [ModelField("farm_id")]
    public UInt64 farmId;

    [ModelField("id")]
    public UInt64 id;

    [ModelField("entity_type")]
    public UInt64 entityType;

    [ModelField("entity_index")]
    public UInt64 entityIndex;
}

public enum TileStateT
{
    Enviroment = 1,
    Crop = 2
}
