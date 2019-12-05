using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public struct InventoryItem
    {
        public string itemName;
        public int quantity;
        public Sprite sprite;
        
        public InventoryItem(string itemName, int quantity, Sprite sprite)
        {
            this.itemName = itemName;
            this.quantity = quantity;
            this.sprite = sprite;
        }
    }

    public List<InventoryItem> inventory;

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        for (int i = 0; i < 150; i++)
        {
            inventory.Add(new InventoryItem("", 0, null));
        }
    }
}
