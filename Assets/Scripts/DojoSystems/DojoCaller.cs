using Dojo;
using Dojo.Starknet;
using dojo_bindings;
using System;
using UnityEngine;

public class DojoCaller : MonoBehaviour
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
        account = new Account(provider, worldManager.PrivateKey, new FieldElement(worldManager.WorldAddress));
    }

    public async void ExecuteCall(dojo.Call call)
    {
        if (worldManager == null)
            Init();

        try { await account.ExecuteRaw(new[] { call }); }
        catch (Exception e) { Debug.LogError(e); }
    }
    
    public DojoSystems GetSystems()
    { return systems; }

}
