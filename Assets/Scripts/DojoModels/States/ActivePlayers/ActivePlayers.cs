using Dojo;
using Dojo.Starknet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayers : ModelInstance
{
    [ModelField("idx")]
    public UInt64 idx;

    [ModelField("player")]
    public FieldElement player;

    [ModelField("last_timestamp_activity")]
    public UInt64 last_timestamp_activity;
}
