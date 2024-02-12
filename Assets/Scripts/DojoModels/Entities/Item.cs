using Dojo;
using Dojo.Starknet;
using System;

public class Item : ModelInstance
{
    [ModelField("id")]
    public UInt64 id;

    [ModelField("name")]
    public FieldElement itemName;

    [ModelField("env_entity_id")]
    public UInt64 envEntityId;
}

public enum Tool
{
    Pickaxe = 1,
    Hoe = 2, 
    WateringCan = 3,
}

public enum Seed
{
    Pumpkin = 1000,
    Onion = 1001,
    Carrot = 1002,
    Corn = 1003,
    Mushroom = 1004
}