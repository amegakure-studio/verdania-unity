using Dojo.Starknet;
using dojo_bindings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : DojoSystem
{
    public async void CreatePlayer(string playerId, string playerName, SkinType gender)
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

        try { await account.ExecuteRaw(new[] { call }); }
        catch (Exception e) { Debug.LogError(e); }
    }
}
