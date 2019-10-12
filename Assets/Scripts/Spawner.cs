using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objectPool;
    public List<GameObject> objectToSpawn;

    [ReadOnlyField]
    public float timer = 0;

    public int timeBetween;

    public Transform target;

    public GameObject manager;

    private void Awake()
    {
        int i = 0;
        foreach (GameObject obj in objectPool)
        {
            
            for (int j = 0; j < objectPool[i].transform.GetChild(0).GetComponent<PokemonData>().rarity; j++)
            {
                objectToSpawn.Add(objectPool[i]);
            }
            i++;
        }
    }

    private void Update()
    {
        //timer += Time.deltaTime;
        if (/*timer >= timeBetween*/transform.childCount == 0)
        {
            //Add Timer here to do CHANCE of spawn every second after the last pokemon is destoryed
            int rand = Random.Range(0, objectToSpawn.Count);
            GameObject spawned = objectToSpawn[rand];
            int randShiny = Random.Range(0, 8192);
            if (spawned.name.Contains("Umbreon"))
            {
                if (manager.GetComponent<DaylightCycle>().day)
                {
                    rand = Random.Range(0, objectToSpawn.Count);
                    spawned = objectToSpawn[rand];
                }
            }

            if (spawned.name.Contains("Deerling"))
            {
                if (manager.GetComponent<SeasonCycle>().season == "Winter")
                {
                    spawned = spawned.GetComponent<PokemonData>().variants[0];
                }
                else if (manager.GetComponent<SeasonCycle>().season == "Summer")
                {
                    spawned = spawned.GetComponent<PokemonData>().variants[1];

                }
                else if (manager.GetComponent<SeasonCycle>().season == "Fall")
                {
                    spawned = spawned.GetComponent<PokemonData>().variants[2];

                }
            }

            if (randShiny == 888)
            {
                Debug.Log("Shiny");
                if (spawned.GetComponent<PokemonData>().shinyPrefab != null)
                {
                    Instantiate(spawned.GetComponent<PokemonData>().shinyPrefab);
                }
                else
                {
                    Instantiate(spawned, gameObject.transform);
                }
            }
            else
            {
                Instantiate(spawned, gameObject.transform);
            }
            //spawned.GetComponent<PokemonData>().seen = true;
            Debug.Log(spawned.name);
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    spawned.GetComponent<PokemonData>().captured = true;
            //}
            timer = 0;
        }

    }
}
