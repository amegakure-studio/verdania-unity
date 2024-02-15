using Dojo;
using dojo_bindings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private InventoryItems itemsData;
    private PlayerFinder finder;
    private Session session;
    private WorldManager m_WorldManager;
    private PlayerSystem playerSystem;
    private DojoSystem dojoSystem;

    private void Awake()
    {
        finder = UnityUtils.FindOrCreateComponent<PlayerFinder>();
        itemsData = Resources.Load<InventoryItems>("InventoryItems");
        session = UnityUtils.FindOrCreateComponent<Session>();
        m_WorldManager = GameObject.FindObjectOfType<WorldManager>();

        playerSystem = UnityUtils.FindOrCreateComponent<PlayerSystem>();
        dojoSystem = UnityUtils.FindOrCreateComponent<DojoSystem>();

    }

    public void Equip(UInt64 itemId)
    {
        dojo.Call equipItemCall = playerSystem.EquipItem(session.PlayerId.Hex(), itemId, dojoSystem.Systems.playerSystemAdress);

        try
        {
            dojoSystem.ExecuteCalls(new[] { equipItemCall });
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw new Exception("Couldn't equip the item: " + itemId);
        }


    }

    public List<ERC1155Balance> GetItems()
    {
        List<ERC1155Balance> items = finder.GetPlayerItems(session.PlayerId.Hex(), m_WorldManager.Entities());
        return items;
    }
}
