using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using System;
using System.Numerics;

public class PlayerState : ModelInstance
{
    [ModelField("player")]
    public FieldElement player;

    [ModelField("farm_id")]
    public UInt64 farmId;

    [ModelField("x")]
    public UInt64 x;

    [ModelField("y")]
    public UInt64 y;

    [ModelField("equipment_item_id")]
    public UInt64 equipmentItemId;

    [ModelField("tokens")]
    public UInt64 tokens;

    public Action<PlayerState> positionChanged;

    public override void OnUpdate(Model model)
    {
        UInt64 oldX = x;
        UInt64 oldY = y;

        base.OnUpdate(model);

        if (oldX != x || oldY != y)
            positionChanged?.Invoke(this);
    }
}