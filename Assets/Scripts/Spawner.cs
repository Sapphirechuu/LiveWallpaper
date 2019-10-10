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
        timer += Time.deltaTime;
        if (timer >= timeBetween)
        {
            int rand = Random.Range(0, objectToSpawn.Count);
            GameObject spawned = Instantiate(objectToSpawn[rand], gameObject.transform);
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
