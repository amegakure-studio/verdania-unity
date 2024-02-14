using Dojo;
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

    private void Awake()
    {
        finder = UnityUtils.FindOrCreateComponent<PlayerFinder>();
        itemsData = Resources.Load<InventoryItems>("InventoryItems");
        session = UnityUtils.FindOrCreateComponent<Session>();
        m_WorldManager = GameObject.FindObjectOfType<WorldManager>();
    }

    public void Equip()
    {

    }

    public List<ERC1155Balance> GetItems()
    {
        List<ERC1155Balance> items = finder.GetPlayerItems(session.PlayerId.Hex(), m_WorldManager.Entities());
        return items;
    }
}
