using Dojo;
using Dojo.Starknet;
using System;

public class PlayerState : ModelInstance
{
    [ModelField("player")]
    public FieldElement player;

    [ModelField("farm_id")]
    public UInt64 farmId;

    [ModelField("x")]
    public UInt64 x;

    [ModelField("y")]
    public UInt64 y;

    [ModelField("equipment_item_id")]
    public UInt64 equipmentItemId;

    [ModelField("tokens")]
    public FieldElement tokens;
}