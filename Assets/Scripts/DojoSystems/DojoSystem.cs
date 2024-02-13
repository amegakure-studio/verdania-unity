using Dojo.Starknet;
using Dojo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DojoSystem : MonoBehaviour
{
    protected WorldManager worldManager;
    protected JsonRpcClient provider;
    protected Account account;
    protected DojoSystemsData systems;

    private void Awake()
    {
        Init();
        systems = Resources.Load<DojoSystemsData>("Config/DojoSystemsData");
    }

    private void Init()
    {
        worldManager = FindObjectOfType<WorldManager>();
        provider = new JsonRpcClient(worldManager.RpcUrl);
        account = new Account(provider, worldManager.PrivateKey, new FieldElement(worldManager.PlayerAddress));
    }
}
