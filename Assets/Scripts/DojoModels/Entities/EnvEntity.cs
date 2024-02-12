using Dojo;
using Dojo.Starknet;
using System;

public class EnvEntity : ModelInstance
{
    [ModelField("id")]
    public UInt64 id;

    [ModelField("name")]
    public FieldElement entityName;

    [ModelField("drop_item_id")]
    public UInt64 dropItemId;

    [ModelField("quantity")]
    public UInt64 quantity;

    [ModelField("durability")]
    public byte durability;
}

public enum EnvEntityT
{
    Pumpkin = 1,
    Carrot = 3,
    Onion = 2,
    Corn = 4,
    Mushroom = 5,

    Tree = 1001,
    Rock = 1002,
    SuitableForCrop = 1003,
    Trunk = 1004
}