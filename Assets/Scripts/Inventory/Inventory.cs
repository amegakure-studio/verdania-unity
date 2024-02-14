using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private InventoryItems itemsData;
    private PlayerFinder finder;

    private void Awake()
    {
        finder = UnityUtils.FindOrCreateComponent<PlayerFinder>();
        itemsData = Resources.Load<InventoryItems>("InventoryItems");
    }

    public void Equip()
    {

    }

    public List<ERC1155Balance> GetItems()
    {
        return null;
    }
}
