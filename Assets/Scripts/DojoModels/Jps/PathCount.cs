using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCount : ModelInstance
{
    [ModelField("player")]
    public FieldElement player;

    [ModelField("index")]
    public UInt32 index;

    [ModelField("last_update")]
    public UInt64 last_update;

    public override void Initialize(Model model)
    {
        //UInt64 oldX = x;
        //UInt64 oldY = y;

        UInt64 oldLastUpdate = last_update;

        base.Initialize(model);

        Debug.Log("Path on init");

        //if (oldX != x || oldY != y)
        //{
        //    EventManager.Instance.Publish(GameEvent.PATH_UPDATED, new() { { "Path", this } });

        //    Debug.Log("Change: Old X: " + oldX + " X: " + x + "\n Old Y: " + oldY + " Y: " + y);

        //}

        if (last_update != oldLastUpdate) 
        {
            EventManager.Instance.Publish(GameEvent.PATH_UPDATED, new() { { "Path", this } });
            Debug.Log("Change: Old last_update: " + oldLastUpdate + ", New: " + last_update);
        }

        
    }
}
