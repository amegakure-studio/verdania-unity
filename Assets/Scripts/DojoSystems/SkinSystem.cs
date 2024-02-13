using Dojo.Starknet;
using dojo_bindings;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class SkinSystem: MonoBehaviour
{
    public dojo.Call CreatePlayer(string playerId, string playerName, SkinType gender, string skinSystemAdress)
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
            to = skinSystemAdress
        };

        return call;
    }
}
