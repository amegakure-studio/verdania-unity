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
    private Dictionary<KeyCode, VisualElement> keyItemMap;
    private VisualElement selectedSlot;

    private void OnEnable()
    {
        m_WorldManager = GameObject.FindObjectOfType<WorldManager>();
        m_WorldManager.OnEntityFeched += Init;
        EventManager.Instance.Subscribe(GameEvent.SPAWN_ERC1155BALANCE, HandleItemSpawn);
    }

    private void HandleItemSpawn(Dictionary<string, object> context)
    {
        try
        {
            ERC1155Balance item = (ERC1155Balance)context["Item"];
            UpdateSlot(item);

        }
        catch (Exception e) { Debug.LogError(e); }
    }

    void Awake()
    {
        keyItemMap = new();

        itemsData = Resources.Load<InventoryItems>("InventoryItems");
        inventory = UnityUtils.FindOrCreateComponent<Inventory>();
        InitSoltItemsMap();
    }

    private void InitSoltItemsMap()
    {
        VisualElement root = GameObject.FindObjectOfType<UIDocument>().rootVisualElement;

        List<VisualElement> slotsVe = root.Q<VisualElement>("Inventory").Children().ToList();
        slotsVe.ForEach(slotVe => slotVe.Q<Label>("Id").text = (slotsVe.IndexOf(slotVe) + 1).ToString());
        slotsVe[slotsVe.Count - 1].Q<Label>("Id").text = "0";

        slotsVe.ForEach(slotve =>
        {
            int index = slotsVe.IndexOf(slotve);
            if (index == slotsVe.Count - 1)
                keyItemMap[KeyCode.Alpha0] = slotve;
            else
            {
                KeyCode keyCode = KeyCode.Alpha1 + index;
                keyItemMap[keyCode] = slotve;
            }
        });


        slotItemMap = new();
        slotsVe.ForEach((slotVe) => slotItemMap.Add(slotVe, null));
    }

    private void Init(WorldManager obj)
    {
        if (slotItemMap == null)
            InitSoltItemsMap();

        List<ERC1155Balance> items = inventory.GetItems();

        items.ToList().ForEach(item =>
        {
            UpdateSlot(item);
            item.balanceChanged += UpdateSlot;
        }
        );

        SelectSlot(inventory.GetEquippedItem());
    }

    private void SelectSlot(ItemType selectedItemType)
    {
        foreach(VisualElement slotVe in slotItemMap.Keys)
        {
            ERC1155Balance item = slotItemMap[slotVe];
            Debug.Log("selectedItemType : " + selectedItemType);
            Debug.Log("Null? : " + item == null + " ID: " + item.id);

            ItemType itemType = (ItemType)Int32.Parse(item.id.ToString());

            if (selectedItemType == itemType)
            {
                if (selectedSlot != null)
                    selectedSlot.RemoveFromClassList("selected");

                slotVe.AddToClassList("selected");
                selectedSlot = slotVe;

                return;
            }
        }
    }

    private void UpdateSlot(ERC1155Balance item)
    {
        List<VisualElement> slotsVe = slotItemMap.Keys.ToList();

        foreach(VisualElement slotVe in slotsVe)
        {
            if (slotItemMap[slotVe] != null && slotItemMap[slotVe].id.Equals(item.id))
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

                return;
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
        EventManager.Instance.Unsubscribe(GameEvent.SPAWN_ERC1155BALANCE, HandleItemSpawn);
    }

    private void Update()
    {
        List<KeyCode> keycodes = keyItemMap.Keys.ToList();
        keycodes.ForEach(keyCode =>
                                    {
                                        if (Input.GetKeyDown(keyCode))
                                        {
                                            try
                                            {
                                                if (keyItemMap.Keys.ToList().Contains(keyCode))
                                                {
                                                    VisualElement slotve = keyItemMap[keyCode];
                                                    ERC1155Balance item = slotItemMap[slotve];

                                                    if (item != null)
                                                    {
                                                        ItemType itemtype = (ItemType)Int32.Parse(item.id.ToString());

                                                        inventory.Equip((ulong)item.id);
                                                        Debug.Log("item type: " + itemtype + " id: " + (ulong)item.id);
                                                        SelectSlot(itemtype);
                                                    }
                                                }
                                            }
                                            catch (Exception e) { Debug.LogError(e); }

                                        }
                                    });
    }
}
