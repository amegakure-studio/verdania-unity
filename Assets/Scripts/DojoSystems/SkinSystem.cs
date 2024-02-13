using Dojo.Starknet;
using dojo_bindings;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class SkinSystem : DojoSystem
{
    public async Task<Boolean> CreatePlayer(string playerId, string playerName, SkinType gender)
    {
        Debug.Log("created: " + playerId);
        var player_id = new FieldElement(playerId).Inner();
        var name = new FieldElement(playerName).Inner();
        var genderType = new FieldElement(gender).Inner();

        dojo.Call call = new()
        {
            calldata = new dojo.FieldElement[]
            {
                        player_id, name, genderType
            },
            selector = "create",
            to = systems.skinSystemAdress
        };

        try
        {
            await account.ExecuteRaw(new[] { call }); 
            return true;
        }
        catch (Exception e) { Debug.LogError(e); return false;}
    }
}
