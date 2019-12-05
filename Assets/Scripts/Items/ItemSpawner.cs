using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> gameObjects;
    [ReadOnlyField]
    public float timer;
    public int timeTillSpawn;

    public Camera mainCamera;

    public Canvas canvas;
    public Image image;

    public Text text;

    public GameObject manager;
    [ReadOnlyField]
    public int rand;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeTillSpawn && gameObject.transform.childCount == 0)
        {
            rand = Random.Range(0, 100);
            switch(rand)
            {
                case int n when ((n >= 0) && (n < 50)):
                    Instantiate(gameObjects[0], gameObject.transform);
                    //Debug.Log("Common");
                    break;
                case int n when ((n >= 50) && (n < 80)):
                    Instantiate(gameObjects[1], gameObject.transform);
                    //Debug.Log("Uncommon");
                    break;
                case int n when ((n >= 80) && (n < 90)):
                    Instantiate(gameObjects[2], gameObject.transform);
                    //Debug.Log("Rare");
                    break;
                case int n when ((n >= 90) && (n < 96)):
                    Instantiate(gameObjects[3], gameObject.transform);
                    //Debug.Log("Super Rare");
                    break;
                case int n when ((n >= 96) && (n < 98)):
                    Instantiate(gameObjects[4], gameObject.transform);
                    //Debug.Log("Ultra Rare");
                    break;
                case int n when ((n >= 98) && (n < 100)):
                    Instantiate(gameObjects[5], gameObject.transform);
                    //Debug.Log("Ultra Rare");
                    break;
                default:
                    Instantiate(gameObjects[0], gameObject.transform);
                    Debug.Log("Rand is not in correct bounds or something is wrong");
                    break;
            }
            timer = 0;
        }
    }
}
