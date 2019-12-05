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

    private void Update()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("LeftMouseButton");
                if (hit.collider.gameObject == gameObject)
                {
                    ChooseItem();
                }
            }
        }
    }

    public void ChooseItem()
    {
        int rand = Random.Range(0, possibleItems.Count);
        itemToSpawn = possibleItems[rand];

        canvas.enabled = true;
        image.GetComponent<Image>().sprite = itemToSpawn.sprite;
        text.text = itemToSpawn.itemName;

    }
}
