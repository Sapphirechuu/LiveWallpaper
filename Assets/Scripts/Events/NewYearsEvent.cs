using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewYearsEvent : MonoBehaviour
{
    public List<GameObject> pokemonToSpawn;

    public Vector3 targetPos;

    List<GameObject> pokemonInstances = new List<GameObject>();

    bool allSpawned;
    [ReadOnlyField]
    public float timer;

    bool positionUpdated = false;
    public float delay;

    List<GameObject> spawners = new List<GameObject>();
    private void Update()
    {
        if (!allSpawned)
        {
            StartCoroutine(SpawnPokemon());
            allSpawned = true;
        }
        if (GameObject.FindGameObjectsWithTag("Spawners") != null)
        {
            foreach (GameObject game in GameObject.FindGameObjectsWithTag("Spawners"))
            {
                spawners.Add(game);
            }
        }
        if (spawners != null)
        {
            foreach (GameObject spawner in spawners)
            {
                spawner.gameObject.SetActive(false);
            }
        }

        if (pokemonInstances.Count == pokemonToSpawn.Count)
            {
            for (int i = 0; i < pokemonInstances.Count; i++)
            {
                if (i < pokemonInstances.Count / 2)
                {
                    targetPos.x = 10 * i;
                }
                else if (i >= pokemonInstances.Count / 2)
                {
                    targetPos.x = (i - ((pokemonInstances.Count / 2) - 1)) * -10;
                }

                if (i < pokemonInstances.Count / 2)
                {
                    targetPos.z = i * 10;
                }
                else if (i >= pokemonInstances.Count / 2)
                {
                    targetPos.z = (i - ((pokemonInstances.Count / 2) - 1)) * 10;


                }
                positionUpdated = true;
                if (pokemonInstances[i].transform.position != targetPos && !pokemonInstances[i].GetComponent<EventPokemonData>().hasMoved)
                {
                    pokemonInstances[i].transform.position = Vector3.MoveTowards(pokemonInstances[i].transform.position, targetPos, 0.5f);
                }
                else
                {
                    pokemonInstances[i].GetComponent<EventPokemonData>().hasMoved = true;

                    pokemonInstances[i].transform.GetChild(0).GetComponent<Animator>().SetInteger("AnimationState", 1);
                }

            }
        }

        
    }

    IEnumerator SpawnPokemon()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        foreach (GameObject pokemon in pokemonToSpawn)
        {
            GameObject instance = Instantiate(pokemon/*, gameObject.transform*/);
            if (instance.GetComponent<SmoothMovment>().enabled)
            {
                instance.GetComponent<SmoothMovment>().enabled = false;
            }
            pokemonInstances.Add(instance);
            instance.AddComponent<EventPokemonData>();

            yield return wait;
        }
    }
}
