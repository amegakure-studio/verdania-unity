using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItems", menuName = "ScriptableObjects/InventoryItems", order = 1)]

public class InventoryItems : ScriptableObject
{
    [SerializeField] public ItemMap[] items;

    [Serializable]
    public struct ItemMap
    {
        public ItemType itemType;
        public string itemName;

        public Sprite itemIcon;
        public GameObject itemPrefab;

        public GameEvent interactEvent;
    }
}
