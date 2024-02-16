using System;
using UnityEngine;
using Dojo;
using Dojo.Starknet;
using dojo_bindings;
using System.Threading.Tasks;

public class InteractSystem : MonoBehaviour
{
    public dojo.Call Interact(string playerId, UInt64 tileId, string interactSystemAddress)
    {
        Debug.Log("created: " + playerId);
        var player_id = new FieldElement(playerId).Inner();
        var tile_id = new FieldElement(tileId.ToString("X")).Inner();
	        
        dojo.Call call = new()
        {
            calldata = new dojo.FieldElement[]
            {
                        player_id, tile_id
            },
            selector = "interact",
            to = interactSystemAddress
        };

        return call;
    }
}
