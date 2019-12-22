using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewYearsEvent : MonoBehaviour
{
    public List<GameObject> pokemonToSpawn;

    public Vector3 targetPos;

    List<GameObject> pokemonInstances = new List<GameObject>();

    private void Awake()
    {
        foreach (GameObject pokemon in pokemonToSpawn)
        {
            GameObject instance = Instantiate(pokemon, gameObject.transform);
            pokemonInstances.Add(instance);
        }
    }

    private void Update()
    {
        foreach(GameObject instance in pokemonInstances)
        {
            if (instance.transform.position != targetPos)
            {
                Debug.Log("Moving?");
                instance.transform.position = Vector3.MoveTowards(instance.transform.position, targetPos, 0.5f);
                //when done, change to idle animation.
            }
        }

        
    }
}
