using System;
using Dojo;
using Dojo.Starknet;

public enum SkinType
{
    James = 1,
    Alice = 2
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
