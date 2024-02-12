using System;
using UnityEngine;
using Dojo;
using Dojo.Starknet;
using dojo_bindings;

public class FarmSystem : DojoSystem
{
    public async void CreateFarm(string playerId)
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
            to = systems.farmSystemAdress
        };
	
	    try { await account.ExecuteRaw(new[] { call }); }
        catch (Exception e) { Debug.LogError(e); }
    }
}
