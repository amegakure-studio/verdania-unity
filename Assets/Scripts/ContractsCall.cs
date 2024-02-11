using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Dojo;
using Dojo.Starknet;
using dojo_bindings;

public class ContractsCall : MonoBehaviour
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

    private async void CreatePlayer(string playerId)
    {
        var player_id = new FieldElement(playerId).Inner();
        DojoCaller dojoCaller = UnityUtils.FindOrCreateComponent<DojoCaller>();
	
	Debug.Log("P Address: " + worldManager.PlayerAddress);
	
	        dojo.Call call = new()
        {
            calldata = new dojo.FieldElement[]
            {
                        player_id
            },
            selector = "create_farm",
            to = dojoCaller.GetSystems().farmSystemAdress
        };
	
	try { await account.ExecuteRaw(new[] { call }); }
        catch (Exception e) { Debug.LogError(e); }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreatePlayer("0x250");
        }
    }
}
