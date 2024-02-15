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

public enum ItemType
{
    None = 0,

    //Tools
    Pickaxe = 1,
    Hoe = 2, 
    WateringCan = 3,

    //Seeds
    Pumpkin_Seeds = 1000,
    Onion_Seeds = 1001,
    Carrot_Seeds = 1002,
    Corn_Seeds = 1003,
    Mushroom_Seeds = 1004,

    //Harvested items
    Pumpkin = 2000,
    Onion = 2001,
    Carrot = 2002,
    Corn = 2003,
    Mushroom = 2004,

    //Material
    Wood = 3000,
    Rock = 3001
}