using Dojo;
using System;

public class EnvEntityState : ModelInstance
{
    [ModelField("farm_id")]
    public UInt64 farmId;

    [ModelField("index")]
    public UInt64 index;

    [ModelField("env_entity_id")]
    public UInt64 envEntityId;

    [ModelField("x")]
    public UInt64 x;

    [ModelField("y")]
    public UInt64 y;

    [ModelField("active")]
    public bool active;
}
