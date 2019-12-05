using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public struct InventoryItem
    {
        public string itemName;
        public string itemDescription;
        public int quantity;
        public Sprite sprite;
        public int itemNum;
        public bool inInventory;

        public InventoryItem(string itemName, string itemDescription, int quantity, Sprite sprite, int itemNum, bool inInventory)
        {
            this.itemName = itemName;
            this.itemDescription = itemDescription;
            this.quantity = quantity;
            this.sprite = sprite;
            this.itemNum = itemNum;
            this.inInventory = inInventory;
        }
    }

    public List<InventoryItem> inventory;

    public Sprite defaultSprite;

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        for (int i = 0; i < 150; i++)
        {
            inventory.Add(new InventoryItem("", "", 0, defaultSprite, -1, false));
        }
    }
}
