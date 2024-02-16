using Dojo.Starknet;
using dojo_bindings;
using System;
using UnityEngine;

public class PlayerSystem: MonoBehaviour
{
    public dojo.Call CreatePlayer(string playerId, string playerName, SkinType gender, string playerSystemAdress)
    {
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
            to = playerSystemAdress
        };

        return call;
    }

    public dojo.Call Move(string playerId, UInt64 x, UInt64 y, string playerSystemAddress)
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
            selector = "move",
            to = playerSystemAddress
        };

        return call;
    }

    public dojo.Call EquipItem(string playerId, UInt64 itemId, string playerSystemAddress)
    {
        var player_id = new FieldElement(playerId).Inner();
        var item_id = new FieldElement(itemId.ToString("X")).Inner();


        dojo.Call call = new()
        {
            calldata = new dojo.FieldElement[]
            {
                        player_id, item_id
            },
            selector = "equip",
            to = playerSystemAddress
        };

        return call;
    }
}
