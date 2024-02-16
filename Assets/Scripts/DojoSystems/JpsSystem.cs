using Dojo.Starknet;
using dojo_bindings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JpsSystem : MonoBehaviour
{
    public dojo.Call FindPath(string playerId, UInt64 x, UInt64 y, string jpsSystemAddress)
    {
        var player_id = new FieldElement(playerId).Inner();
        var x_id = new FieldElement(x.ToString("X")).Inner();
        var y_id = new FieldElement(y.ToString("X")).Inner();

        dojo.Call call = new()
        {
            calldata = new dojo.FieldElement[]
            {
                        player_id, x_id, y_id
            },
            selector = "find_path",
            to = jpsSystemAddress
        };

        return call;
    }
}
