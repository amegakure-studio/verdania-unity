using Dojo;
using Dojo.Starknet;
using System;

public class Crop : ModelInstance
{
    [ModelField("id")]
    public UInt64 id;

    [ModelField("name")]
    public FieldElement cropName;

    [ModelField("harvest_time")]
    public UInt64 harvestTime;

    [ModelField("min_watering_time")]
    public UInt64 minWateringTime;

    [ModelField("drop_item_id")]
    public UInt64 dropItemId;

    [ModelField("quantity")]
    public UInt64 quantity;
}

public enum CropType
{
    Pumpkin = 2000,
    Onion = 2001,
    Carrot = 2002,
    Corn = 2003,
    Mushroom = 2004
}
