using Dojo;
using Dojo.Starknet;
using System;

public class MarketplaceMeta : ModelInstance
{
    [ModelField("id")]
    public FieldElement id;

    [ModelField("owner")]
    public FieldElement owner;

    [ModelField("open")]
    public bool open;

    [ModelField("spawn_time")]
    public UInt64 spawnTime;

    [ModelField("item_list_len")]
    public FieldElement itemListLength;
}
