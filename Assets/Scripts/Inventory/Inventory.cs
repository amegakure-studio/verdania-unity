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
    private Character character;
    private GameObject equipedItemGO;

    private void OnEnable()
    {
        EventManager.Instance.Subscribe(GameEvent.PLAYER_CREATED, HandlePlayerSpawn);
    }

    private void OnDisable()
    {
        EventManager.Instance.Unsubscribe(GameEvent.PLAYER_CREATED, HandlePlayerSpawn);
    }

    private void Awake()
    {
        finder = UnityUtils.FindOrCreateComponent<PlayerFinder>();
        itemsData = Resources.Load<InventoryItems>("InventoryItems");
        session = UnityUtils.FindOrCreateComponent<Session>();
        m_WorldManager = GameObject.FindObjectOfType<WorldManager>();

        playerSystem = UnityUtils.FindOrCreateComponent<PlayerSystem>();
        dojoSystem = UnityUtils.FindOrCreateComponent<DojoSystem>();
    }

    private void HandlePlayerSpawn(Dictionary<string, object> context)
    {
        try
        {
            Character contextCharacter = (Character)context["Player"];
            if (contextCharacter != null &&
                contextCharacter.DojoId.Equals(session.PlayerId.Hex()))
            {
                character = contextCharacter;
                SetItem(GetEquippedItem());
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void Equip(UInt64 itemId)
    {
        dojo.Call equipItemCall = playerSystem.EquipItem(session.PlayerId.Hex(), itemId, dojoSystem.Systems.playerSystemAdress);

        try
        {
            dojoSystem.ExecuteCalls(new[] { equipItemCall });
            SetItem((ItemType)itemId);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw new Exception("Couldn't equip the item: " + itemId);
        }
    }

    private void SetItem(ItemType itemId)
    {
            ItemGoBinding[] itemGoBindings = character.ItemsGo;

            foreach (ItemGoBinding binding in itemGoBindings)
            {
                if (binding.itemType == itemId)
                {
                    if (equipedItemGO != null)
                        equipedItemGO.SetActive(false);

                    equipedItemGO = binding.itemGo;
                    equipedItemGO.SetActive(true);
                    return;
                }
            }
    }

    public ItemType GetEquippedItem()
    {
        PlayerState playerState = finder.GetPlayerStateById(session.PlayerId.Hex(), m_WorldManager.Entities());

        return (ItemType)playerState.equipmentItemId;
    }

    public List<ERC1155Balance> GetItems()
    {
        List<ERC1155Balance> items = finder.GetPlayerItems(session.PlayerId.Hex(), m_WorldManager.Entities());
        return items;
    }
}
