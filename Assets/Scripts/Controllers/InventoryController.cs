using Dojo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    private InventoryItems itemsData;

    private WorldManager m_WorldManager;
    private Inventory inventory;

    private Dictionary<VisualElement, ERC1155Balance> slotItemMap;

    private void OnEnable()
    {
        m_WorldManager = GameObject.FindObjectOfType<WorldManager>();
        m_WorldManager.OnEntityFeched += Init;
    }

    void Awake()
    {
        itemsData = Resources.Load<InventoryItems>("InventoryItems");
        inventory = UnityUtils.FindOrCreateComponent<Inventory>();
        
        VisualElement root = GameObject.FindObjectOfType<UIDocument>().rootVisualElement;

        List<VisualElement> slotsVe = root.Q<VisualElement>("Inventory").Children().ToList();
        slotsVe.ForEach(slotVe => slotVe.Q<Label>("Id").text = slotsVe.IndexOf(slotVe).ToString());

        slotItemMap = new();
        slotsVe.ForEach((slotVe) => slotItemMap.Add(slotVe, null));
    }

    private void Init(WorldManager obj)
    {
        List<ERC1155Balance> items = inventory.GetItems();

        items.ToList().ForEach(item => UpdateSlot(item));
    }

    private void UpdateSlot(ERC1155Balance item)
    {
        List<VisualElement> slotsVe = slotItemMap.Keys.ToList();

        foreach (VisualElement slotVe in slotsVe) 
        {
            if (slotItemMap[slotVe].id.Equals(item.id))
            {
                slotVe.Q<Label>("Quantity").text = item.amount.ToString();

                if (item.amount.IsZero)
                {
                    slotVe.Q<VisualElement>("Item").style.backgroundImage = null;
                    slotVe.Q<Label>("Quantity").text = "";
                    slotItemMap[slotVe] = null;
                }

                return;
            }
        }

        foreach (VisualElement slotVe in slotsVe)
        {
            if (slotItemMap[slotVe] == null)
            {
                ItemType itemType = (ItemType)Int32.Parse(item.id.ToString());

                slotVe.Q<Label>("Quantity").text = item.amount.ToString();
                slotVe.Q<VisualElement>("Item").style.backgroundImage = new StyleBackground(GetItemSprite(itemType));

                slotItemMap[slotVe] = item;
            }
        }
    }

    private Sprite GetItemSprite(ItemType itemType)
    {
        return itemsData.items
            .ToList()
            .Find(item => item.itemType == itemType)
            .itemIcon;
    }

    private void OnDisable()
    {
        m_WorldManager.OnEntityFeched -= Init;
    }

}
