using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public List<ItemData> possibleItems;
    private ItemData itemToSpawn;

    public Camera mainCamera;

    public Canvas canvas;
    public Image image;

    public Text text;

    public GameObject manager;
    public Inventory inventoryComponent;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = gameObject.transform.parent.gameObject.GetComponent<ItemSpawner>().mainCamera;
        }

        //if (canvas == null)
        //{
        //    canvas = gameObject.transform.parent.gameObject.GetComponent<ItemSpawner>().canvas;
        //}

        //if (image == null)
        //{
        //    image = gameObject.transform.parent.gameObject.GetComponent<ItemSpawner>().image;
        //}

        //if (text == null)
        //{
        //    text = gameObject.transform.parent.gameObject.GetComponent<ItemSpawner>().text;
        //}

        if (manager == null)
        {
            manager = gameObject.transform.parent.gameObject.GetComponent<ItemSpawner>().manager;
        }

        inventoryComponent = manager.GetComponent<Inventory>();
    }
    private void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ChooseItem();
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void ChooseItem()
    {
        int rand = Random.Range(0, possibleItems.Count);
        itemToSpawn = possibleItems[rand];

        //canvas.enabled = true;
        //image.GetComponent<Image>().sprite = itemToSpawn.sprite;
        //text.text = itemToSpawn.itemName;
        AddToInventory(itemToSpawn.GetComponent<ItemData>());
    }

    public void AddToInventory(ItemData item)
    {
        bool isInInventory = false;
        int indexToUse = 0;
        int i = 0;
        foreach (Inventory.InventoryItem invItem in inventoryComponent.inventory)
        {
            if (inventoryComponent.inventory[i].itemName == "")

            {
                indexToUse = i;
                break;
            }
            else
            {
                if (invItem.itemName == item.itemName)
                {
                    isInInventory = true;
                    indexToUse = i;
                    break;
                }
            }
            i++;
        }

        if (!isInInventory)
        {
            inventoryComponent.inventory[indexToUse] = new Inventory.InventoryItem(item.itemName, item.itemDescription, 1, item.sprite, indexToUse, true);
        }

        else
        {
            inventoryComponent.inventory[indexToUse] = new Inventory.InventoryItem(item.itemName, item.itemDescription, inventoryComponent.inventory[indexToUse].quantity + 1, item.sprite, indexToUse, true);
        }
        Debug.Log(item.itemName);

        //for (int g = 0; g < 150; g++)
        //{
        //    Debug.Log("Inventory Slot: " + g.ToString() + " Item name: " + inventoryComponent.inventory[g].itemName + " Quantity: " +  inventoryComponent.inventory[g].quantity);
        //}
    }
}
