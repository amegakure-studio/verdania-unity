using System;
using Dojo;
using Dojo.Starknet;

public enum GenderType
{
    None,
    Boy,
    Girl
}

public class PlayerSkin : ModelInstance
{
    [ModelField("player")]
    public FieldElement playerId;

    [ModelField("name")]
    public FieldElement playerSkinName;

    [ModelField("gender")]
    public GenderType gender;
}
