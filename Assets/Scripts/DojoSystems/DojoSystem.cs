using Dojo.Starknet;
using Dojo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using dojo_bindings;

public class DojoSystem : MonoBehaviour
{
    protected WorldManager worldManager;
    protected JsonRpcClient provider;
    protected Account account;
    private DojoSystemsData systems;

    public DojoSystemsData Systems { get => systems; set => systems = value; }

    private void Awake()
    {
        Init();
        Systems = Resources.Load<DojoSystemsData>("Config/DojoSystemsData");
    }

    private void Init()
    {
        worldManager = FindObjectOfType<WorldManager>();
        provider = new JsonRpcClient(worldManager.RpcUrl);
        account = new Account(provider, worldManager.PrivateKey, new FieldElement(worldManager.PlayerAddress));
    }

    public async void ExecuteCalls(dojo.Call[] calls)
    {
        try
        {
            await account.ExecuteRaw(calls);
        }
        catch (Exception e) { Debug.LogError(e); throw new Exception("Error in tx's"); }
    }
}
