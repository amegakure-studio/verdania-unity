using Dojo;
using Dojo.Torii;
using System;
using UnityEngine;

public class CropState : ModelInstance
{
    [ModelField("farm_id")]
    public UInt64 farmId;

    [ModelField("index")]
    public UInt64 index;

    [ModelField("crop_id")]
    public UInt64 cropId;

    [ModelField("x")]
    public UInt64 x;

    [ModelField("y")]
    public UInt64 y;

    [ModelField("growing_progress")]
    public UInt64 growingProgress;

    [ModelField("planting_time")]
    public UInt64 plantingTime;

    [ModelField("last_watering_time")]
    public UInt64 lastWateringTime;

    [ModelField("harvested")]
    public bool harvested;

    [ModelField("watered")]
    public bool watered;

    public Action<CropState> cropChanged;

    public override void OnUpdate(Model model)
    {
        bool oldWatered = watered;
        UInt64 oldGrowingProgress = growingProgress;
        bool oldHarvested = harvested;

        base.OnUpdate(model);

        if (oldWatered != watered || oldGrowingProgress != growingProgress || oldHarvested != harvested)
        {
            Debug.Log("CROP CHANGED EVENT EMITTED");
            cropChanged?.Invoke(this);
        }
    }

}
