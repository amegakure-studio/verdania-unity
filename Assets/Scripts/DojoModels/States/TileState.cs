using Dojo;
using Dojo.Torii;
using System;
using Unity.VisualScripting;

public class TileState : ModelInstance
{
    [ModelField("farm_id")]
    public UInt64 farmId;

    [ModelField("id")]
    public UInt64 id;

    [ModelField("entity_type")]
    public UInt64 entityType;

    [ModelField("entity_index")]
    public UInt64 entityIndex;

    public event Action<TileState> TileStateChanged;

    public override void OnUpdate(Model model)
    {
        UInt64 oldEntityType = entityType;
        UInt64 oldEntityIndex = entityIndex;

        base.OnUpdate(model);

        if (oldEntityType != entityType || oldEntityIndex != entityIndex) 
            TileStateChanged?.Invoke(this);
    }
}

public enum TileStateT
{
    Enviroment = 1,
    Crop = 2
}
