using Dojo.Starknet;
using dojo_bindings;
using UnityEngine;

public class UpdaterSystem : MonoBehaviour
{
    public dojo.Call Connect(string playerId, string updaterSystemAddress)
    {
        var player_id = new FieldElement(playerId).Inner(); 

        dojo.Call call = new()
        {
            calldata = new dojo.FieldElement[]
            {
                        player_id
            },
            selector = "connect",
            to = updaterSystemAddress
        };

        return call;
    }
}
