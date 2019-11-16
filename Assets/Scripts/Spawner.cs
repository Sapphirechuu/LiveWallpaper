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
    private DaylightCycle daylight;

    [ReadOnlyField]
    public string timeOfDay = "";

    public PokeDex dex;

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

    private bool canSpawn = true;

    private bool spawnPoolInit;

    private void Awake()
    {
        daylight = manager.GetComponent<DaylightCycle>();

        //Get the seasonCycle
        seasonCycle = manager.GetComponent<SeasonCycle>();
    }

    private void Start()
    {
        

    }

    private void Update()
    {

        if (gameObject.transform.childCount == 1)
        {
            if (gameObject.transform.GetChild(0).name.Contains("Empty"))
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
                Debug.Log("Bye Becky");
            }
        }
        if (spawnPool.Count == 0)
        {
            if (daylight.morning)
            {
                foreach (GameObject spawnable in objectPool)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comMorn)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }

                UpdateSpawnPool();
                timeOfDay = "Morning";
            }
            else if (daylight.day)
            {
                foreach (GameObject spawnable in objectPool)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comDay)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }

                UpdateSpawnPool();
                timeOfDay = "Day";
            }
            else if (daylight.evening)
            {
                foreach (GameObject spawnable in objectPool)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comEven)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }

                UpdateSpawnPool();
                timeOfDay = "Evening";
            }
            else if (daylight.night)
            {
                foreach (GameObject spawnable in objectPool)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comNight)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }

                UpdateSpawnPool();
                timeOfDay = "Night";
            }
        }
        UpdateTime();
        //If the spawner doesn't have a pokemon spawned
        if (transform.childCount == 0 && spawnPool.Count > 0)
        {
            //Start the spawn timer
            spawnTimer += Time.deltaTime;
            //If one second has passed since the pokemon has despawned
            if (spawnTimer > 1)
            {
                //50/50 chance of a pokemon spawning
                int randSpawnChance = Random.Range(0, 2);
                //If the chance = 1, spawn the pokemon
                if (randSpawnChance == 1)
                {
                    Debug.Log("Spawned");
                    //Set the rand variable based on the spawnPool count
                    int rand = Random.Range(0, spawnPool.Count);
                    //The spawned object is equal to the index of the spawn pool at the rand variable
                    GameObject spawned = spawnPool[rand];
                    //Choose a random number between 0, 8191
                    int randShiny = Random.Range(0, 8192);

                    //Get the pokemon data from the spawned pokemon
                    PokemonData pokemonData = spawned.transform.GetChild(0).GetComponent<PokemonData>();

                    //If the spawned pokemon only spawns at night
                    while (pokemonData.nocturnal)
                    {
                        bool allNocturnal = true;
                        for (int i = 0; i < objectPool.Count; i++)
                        {
                            if (!objectPool[i].transform.GetChild(0).GetComponent<PokemonData>().nocturnal)
                            {
                                allNocturnal = false;
                            }
                        }

                        if (objectPool.Count <= 1 || allNocturnal)
                        {
                            canSpawn = false;
                            break;
                        }
                        //If it is not currently night
                        if (!daylight.night)
                        {
                            //Choose another pokemon to spawn
                            rand = Random.Range(0, spawnPool.Count);
                            //Set the spawned variable to the new pokemon
                            spawned = spawnPool[rand];
                            pokemonData = spawned.transform.GetChild(0).GetComponent<PokemonData>();
                        }
                        else
                        {
                            break;
                        }
                    }
                    //If the spawned pokemon only spawns during the day
                    while (pokemonData.diurnal)
                    {
                        bool allDiurnal = true;
                        for (int i = 0; i < objectPool.Count; i++)
                        {
                            if (!objectPool[i].transform.GetChild(0).GetComponent<PokemonData>().diurnal)
                            {
                                allDiurnal = false;
                            }
                        }

                        if (objectPool.Count <= 1 || allDiurnal)
                        {
                            canSpawn = false;
                            break;
                        }

                        //If it is not currently day
                        if (!daylight.day)
                        {
                            //Choose another pokemon to spawn
                            rand = Random.Range(0, spawnPool.Count);
                            //Set the spawned variable to the new pokemon
                            spawned = spawnPool[rand];
                            pokemonData = spawned.transform.GetChild(0).GetComponent<PokemonData>();
                        }
                        else
                        {
                            break;
                        }
                    }
                    //If chance = 0, don't spawn and reset the timer

                    if (canSpawn)
                    {
                        if (pokemonData.variants.Count > 0)
                        {

                            //If the spawned pokemon has a seasonal variant
                            if (pokemonData.seasonal)
                            {
                                //If the season is currently winter
                                if (seasonCycle.season == "Winter")
                                {
                                    //Spawn the winter variant
                                    spawned = pokemonData.variants[0];
                                }
                                //If the season is currently summer
                                else if (seasonCycle.season == "Summer")
                                {
                                    //Spawn the summer variant
                                    spawned = pokemonData.variants[1];
                                }
                                //If the season is currently Fall
                                else if (seasonCycle.season == "Fall")
                                {
                                    //Spawn the Fall variant
                                    spawned = pokemonData.variants[2];

                                }
                            }

                            else if (pokemonData.weathered)
                            {
                                //Add weather cycle support here...
                            }

                            else
                            {
                                int randVar = Random.Range(0, 2);
                                if (randVar == 0)
                                {
                                    spawned = pokemonData.variants[0];
                                }
                            }
                        }
                        //If the shiny chance variable is equal to 888, this is a one in 8192 chance
                        if (randShiny == 888)
                        {
                            //Add one to the shiny count
                            shinyCounter.shinyCount++;
                            //If the pokemonData script does not exist
                            if (pokemonData == null)
                            {
                                //Print to the console that there's an issue
                                Debug.Log("There's an issue");
                            }
                            //If the pokemon's shiny prefab does exist
                            if (pokemonData.shinyPrefab != null)
                            {
                                pokemonData = pokemonData.shinyPrefab.GetComponent<PokemonData>();
                                //Spawn the shiny prefab
                                spawned = Instantiate(pokemonData.shinyPrefab, gameObject.transform);
                            }
                            //If the shiny pregab doesn't exist
                            else
                            {
                                //Spawn the normal pokemon
                                spawned = Instantiate(spawned, gameObject.transform);
                            }
                        }

                        //If the shiny chance variable is not equal to 888
                        else
                        {
                            //Spawn the spawned variable
                            spawned = Instantiate(spawned, gameObject.transform);
                        }


                        if (pokemonData.shiny)
                        {
                            dex.theDex[pokemonData.pokeNum].ShiniesSeen++;
                            dex.theDex[pokemonData.pokeNum].Seen = true;
                        }
                        else
                        {
                            dex.theDex[pokemonData.pokeNum].Seen = true;
                            dex.theDex[pokemonData.pokeNum].NormalSeen++;
                        }


                        //Create the tmep empty object for smooth movement
                        GameObject tempEmpty = Instantiate(emptyObject, gameObject.transform);
                        //Create the tmem camTarg object for smooth movement
                        GameObject tempCamTarg = Instantiate(camTarg, gameObject.transform);
                        //Get the smooth movement variable from the spawned pokemon
                        SmoothMovment spawnedSmooth = spawned.GetComponent<SmoothMovment>();
                        //Get the smooth movement variable from the temp camtarg
                        SmoothMovment camTargSmooth = tempCamTarg.GetComponent<SmoothMovment>();
                        //set the spawned smooth current child to the pathToTake
                        spawnedSmooth.curChild = pathToTake;
                        //set the camTarg smooth current child to the pathToTake
                        camTargSmooth.curChild = pathToTake;
                        //Set the CamerTarg of the temp camtarg to the temp empty
                        camTargSmooth.camerTarget = tempEmpty;
                        //Set the overlord of the camtarg
                        camTargSmooth.overLord = overlord;
                        //Set the spawned cam targ to the temp cam targ
                        spawnedSmooth.camerTarget = tempCamTarg;
                        //Set the spawned overlord
                        spawnedSmooth.overLord = overlord;
                        //Give the spawned smooth move a delay
                        spawnedSmooth.delay = 1.0f;
                        //Make sure the camtarg smooth move does not have a delay
                        camTargSmooth.delay = 0.0f;
                        //Set the spawn timer to 0
                        spawnTimer = 0;
                    }

                    else
                    {
                        Debug.Log("Nothing can spawn");
                    }
                }
                else
                {
                    Debug.Log("Did not spawn");
                    spawnTimer = 0;
                }
                
            }
            
        }

    }

    void UpdateTime()
    {
        if (timeOfDay == "Morning" && !daylight.morning)
        {
            foreach (GameObject spawnable in objectPool)
            {
                if (daylight.day)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comDay)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
                else if (daylight.evening)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comEven)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
                else if (daylight.night)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comNight)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
            }

            UpdateSpawnPool();

            if (daylight.day)
            {
                timeOfDay = "Day";
            }
            else if (daylight.evening)
            {
                timeOfDay = "Evening";
            }
            else if (daylight.night)
            {
                timeOfDay = "Night";
            }
        }
        else if (timeOfDay == "Day" && !daylight.day)
        {
            foreach (GameObject spawnable in objectPool)
            {
                if (daylight.morning)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comMorn)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
                else if (daylight.evening)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comEven)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
                else if (daylight.night)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comNight)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
            }

            UpdateSpawnPool();

            if (daylight.morning)
            {
                timeOfDay = "Morning";
            }
            else if (daylight.evening)
            {
                timeOfDay = "Evening";
            }
            else if (daylight.night)
            {
                timeOfDay = "Night";
            }
        }
        else if (timeOfDay == "Evening" && !daylight.evening)
        {
            foreach (GameObject spawnable in objectPool)
            {
                if (daylight.morning)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comMorn)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
                else if (daylight.day)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comDay)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
                else if (daylight.night)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comNight)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
            }

            UpdateSpawnPool();

            if (daylight.morning)
            {
                timeOfDay = "Morning";
            }
            else if (daylight.day)
            {
                timeOfDay = "Day";
            }
            else if (daylight.night)
            {
                timeOfDay = "Night";
            }
        }
        else if (timeOfDay == "Night" && !daylight.night)
        {
            foreach (GameObject spawnable in objectPool)
            {
                if (daylight.morning)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comMorn)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
                else if (daylight.day)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comDay)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
                else if (daylight.evening)
                {
                    if (spawnable.transform.GetChild(0).GetComponent<PokemonData>().comEven)
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity + 1;
                    }
                    else
                    {
                        spawnable.transform.GetChild(0).GetComponent<PokemonData>().rarity = spawnable.transform.GetChild(0).GetComponent<PokemonData>().defaultRarity;
                    }
                }
            }

            UpdateSpawnPool();

            if (daylight.morning)
            {
                timeOfDay = "Morning";
            }
            else if (daylight.day)
            {
                timeOfDay = "Day";
            }
            else if (daylight.evening)
            {
                timeOfDay = "Evening";
            }
        }
    }

    void UpdateSpawnPool()
    {
        if (spawnPool.Count != 0)
        {
            spawnPool.Clear();
        }
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
}
