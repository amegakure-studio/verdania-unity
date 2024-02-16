using Dojo;
using Dojo.Starknet;
using System;

public class ActivePlayersLen : ModelInstance
{
    [ModelField("key")]
    public FieldElement key;

    [ModelField("len")]
    public UInt64 len;
}
