using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    //The objects that can spawn
    public List<GameObject> objectPool;
    //All the objects that can spawn, this is determinded during runtime by the object pool times their rarity
    [ReadOnlyField]
    public List<GameObject> spawnPool;
    //The Manager for the scene (Deals with daylight cycle and season cycle)
    public GameObject manager;
    //The seasonCycle off of the manager
    private SeasonCycle seasonCycle;

    //Which path to take in scene
    public int pathToTake;
    
    //Used for Smooth movement. This is to keep the object facing the correct way during movement
    [ReadOnlyField]
    public GameObject camTarg;
    [ReadOnlyField]
    public GameObject emptyObject;
    [ReadOnlyField]
    public GameObject overlord;

    //The component that counts the amount of shiny pokemon encountered
    public ShinyCounter shinyCounter;

    //Timer to randomly spawn pokemon (instead of continuous)
    [ReadOnlyField]
    public float spawnTimer = 0;

    private void Awake()
    {
        //Get the seasonCycle
        seasonCycle = manager.GetComponent<SeasonCycle>();
        //Use i to know which index the foreach loop is at
        int i = 0;
        //For each object in the object pool
        foreach (GameObject obj in objectPool)
        {
            //Loop as many times as the rarity int is
            for (int j = 0; j < objectPool[i].transform.GetChild(0).GetComponent<PokemonData>().rarity; j++)
            {
                //Add the object to the spawn pool (ex; if rarity is 2, add 2 of the same object to spawn pool)
                spawnPool.Add(objectPool[i]);    
            }
            //Add to i
            i++;
        }
    }

    private void Update()
    {
        //If the spawner doesn't have a pokemon spawned
        if (transform.childCount == 0)
        {
            //Start the spawn timer
            spawnTimer += Time.deltaTime;
            //If one second has passed since the pokemon has despawned
            if (spawnTimer > 1)
            {
                int randSpawnChance = Random.Range(0, 2);
                if (randSpawnChance == 1)
                {
                    Debug.Log("Spawned");
                    int rand = Random.Range(0, spawnPool.Count);
                    GameObject spawned = spawnPool[rand];
                    int randShiny = Random.Range(0, 8192);
                    if (spawned.name.Contains("Umbreon"))
                    {
                        if (manager.GetComponent<DaylightCycle>().day)
                        {
                            rand = Random.Range(0, spawnPool.Count);
                            spawned = spawnPool[rand];
                        }
                    }
                    else if (spawned.name.Contains("Espeon"))
                    {
                        if (manager.GetComponent<DaylightCycle>().night)
                        {
                            rand = Random.Range(0, spawnPool.Count);
                            spawned = spawnPool[rand];
                        }
                    }


                    PokemonData pokemonData = spawned.transform.GetChild(0).GetComponent<PokemonData>();

                    if (spawned.name.Contains("Deerling") || spawned.name.Contains("Sawsbuck"))
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
                        if (pokemonData == null)
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
                    spawnTimer = 0;
                }
                else
                {
                    Debug.Log("Did not spawn");
                    spawnTimer = 0;
                }
            }
            
            
            //spawned.GetComponent<PokemonData>().seen = true;
            //Debug.Log(spawned.name);
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    spawned.GetComponent<PokemonData>().captured = true;
            //}
        }

    }
}
