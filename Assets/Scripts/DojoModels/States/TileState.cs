using Dojo;
using Dojo.Torii;
using System;
using UnityEngine;

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

    public override void Initialize(Model model)
    {
        base.Initialize(model);
        //Debug.Log("Type: " + entityType + " Index: " + entityIndex);
    }

    public override void OnUpdate(Model model)
    {
        
        UInt64 oldEntityType = entityType;
        UInt64 oldEntityIndex = entityIndex;

        base.OnUpdate(model);

        if (oldEntityType != entityType || oldEntityIndex != entityIndex)
        {
            Debug.Log("Tile changed: " + id);
            TileStateChanged?.Invoke(this);
        }
    }
}

public enum TileStateT
{
    Enviroment = 1,
    Crop = 2
}
