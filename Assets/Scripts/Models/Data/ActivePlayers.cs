using System;
using Dojo;

public class ActivePlayers : ModelInstance
{
    [ModelField("world_id")]
    public UInt64 world_id;

    [ModelField("index")]
    public UInt64 index;

    [ModelField("player")]
    public UInt64 player;

    [ModelField("last_activity_time")]
    public UInt64 last_activity_time;
}
