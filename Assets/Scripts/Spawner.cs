using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;

    [ReadOnlyField]
    public float timer = 0;
    public int timeBetween;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetween)
        {
            int rand = Random.Range(0, objectsToSpawn.Count);
            Instantiate(objectsToSpawn[rand], gameObject.transform);
            timer = 0;
        }
    }
}
