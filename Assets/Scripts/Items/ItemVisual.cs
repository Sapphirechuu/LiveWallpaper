using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemVisual : MonoBehaviour
{
    Inventory invComp;
    Sprite itemSprite;
    string itemName;
    string itemDescription;
    public int itemNum;
    public int quantity;

    bool initialized;

    private void Awake()
    {
        invComp = GameObject.Find("Manager").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (!initialized)
        {
            if (invComp != null)
            {
                SetItems();
                initialized = true;
            }
        }
        else
        {

        }
        UpdateItems();
    }

    void SetItems()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemSprite;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = itemName;
        gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = itemDescription;
        gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = quantity.ToString();
    }

    void UpdateItems()
    {
        itemSprite = invComp.inventory[itemNum].sprite;
        itemName = invComp.inventory[itemNum].itemName;
        itemDescription = invComp.inventory[itemNum].itemDescription;
        quantity = invComp.inventory[itemNum].quantity;
        if (invComp.inventory[itemNum].inInventory)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemSprite;
            gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = itemName;
            gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = itemDescription;
            gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = quantity.ToString();
        }
    }
}
