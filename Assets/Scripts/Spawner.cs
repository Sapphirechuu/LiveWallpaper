using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objectPool;
    public List<GameObject> objectToSpawn;

    [ReadOnlyField]
    public float timer = 0;

    public int timeBetween;

    public Transform target;

    public GameObject manager;
    private SeasonCycle seasonCycle;

    public int pathToTake;

    public GameObject camTarg;
    public GameObject emptyObject;

    public GameObject overlord;

    public ShinyCounter shinyCounter;

    private void Awake()
    {
        seasonCycle = manager.GetComponent<SeasonCycle>();
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
            else if (spawned.name.Contains("Espeon"))
            {
                if (manager.GetComponent<DaylightCycle>().night)
                {
                    rand = Random.Range(0, objectToSpawn.Count);
                    spawned = objectToSpawn[rand];
                }
            }


            PokemonData pokemonData = spawned.transform.GetChild(0).GetComponent<PokemonData>();

            if (spawned.name.Contains("Deerling"))
            {
                if (seasonCycle.season == "Winter")
                {
                    spawned = pokemonData.variants[0];
                }
                else if (seasonCycle.season == "Summer")
                {
                    spawned = pokemonData.variants[1];

                }
                else if (seasonCycle.season == "Fall")
                {
                    spawned = pokemonData.variants[2];

                }
            }

            if (randShiny == 888)
            {

                Debug.Log("Shiny");
                shinyCounter.shinyCount++;
                Debug.Log(spawned.name);
                if(pokemonData == null)
                {
                    Debug.Log("There's an issue");
                }
                if (pokemonData.shinyPrefab != null)
                {
                    spawned = Instantiate(pokemonData.shinyPrefab, gameObject.transform);
                }
                else
                {
                    spawned = Instantiate(spawned, gameObject.transform);
                }
            }
            else
            {
                spawned = Instantiate(spawned, gameObject.transform);
            }
            GameObject tempEmpty = Instantiate(emptyObject, gameObject.transform);
            GameObject tempCamTarg = Instantiate(camTarg, gameObject.transform);
            SmoothMovment spawnedSmooth = spawned.GetComponent<SmoothMovment>();
            SmoothMovment camTargSmooth = tempCamTarg.GetComponent<SmoothMovment>();
            spawnedSmooth.curChild = pathToTake;
            camTargSmooth.curChild = pathToTake;
            camTargSmooth.camerTarget = tempEmpty;
            camTargSmooth.overLord = overlord;
            spawnedSmooth.camerTarget = tempCamTarg;
            spawnedSmooth.overLord = overlord;
            spawnedSmooth.delay = 1.0f;
            camTargSmooth.delay = 0.0f;
            //spawned.GetComponent<PokemonData>().seen = true;
            //Debug.Log(spawned.name);
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    spawned.GetComponent<PokemonData>().captured = true;
            //}
            timer = 0;
        }

    }
}
