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
    Hoe = 2,
    Pickaxe,
    WateringCan,
}

public enum Seed
{
    Carrot,
    Corn,
    Mushroom,
    Onion,
    Pumpkin,
}