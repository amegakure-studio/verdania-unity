using Dojo;
using Dojo.Starknet;
using System;

public class MapFarmPlayer : ModelInstance
{
    [ModelField("farm_id")]
    public UInt64 farm_id;

    [ModelField("owner")]
    public FieldElement owner;
}
