using System;
using UnityEngine;
using Dojo;
using Dojo.Starknet;
using dojo_bindings;
using System.Threading.Tasks;

public class FarmSystem: MonoBehaviour
{
    public dojo.Call CreateFarm(string playerId, string farmSystemAdress)
    {
        Debug.Log("created: " + playerId);
        var player_id = new FieldElement(playerId).Inner();
	        
        dojo.Call call = new()
        {
            calldata = new dojo.FieldElement[]
            {
                        player_id
            },
            selector = "create_farm",
            to = farmSystemAdress
        };

        return call;
    }
}
