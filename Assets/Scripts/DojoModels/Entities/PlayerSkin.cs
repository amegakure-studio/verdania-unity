using System;
using Dojo;
using Dojo.Starknet;

public enum SkinType
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
    public SkinType gender;
}
