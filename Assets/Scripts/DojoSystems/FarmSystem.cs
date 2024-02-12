using System;
using UnityEngine;
using Dojo;
using Dojo.Starknet;
using dojo_bindings;

public class FarmSystem : MonoBehaviour
{
    private WorldManager worldManager;
    private JsonRpcClient provider;
    private Account account;
    private DojoSystems systems;

    public void Awake()
    {
        Init();
        systems = Resources.Load<DojoSystems>("Config/DojoSystemsData");
    }

    private void Init()
    {
        worldManager = FindObjectOfType<WorldManager>();
        provider = new JsonRpcClient(worldManager.RpcUrl);
        account = new Account(provider, worldManager.PrivateKey, new FieldElement(worldManager.PlayerAddress));
    }

    public async void CreatePlayer(string playerId)
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
