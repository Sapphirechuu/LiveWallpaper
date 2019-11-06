using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [ReadOnlyField]
    public Spawner spawner;

    [ReadOnlyField]
    public Transform target;

    public PokemonData pokemonData;

    public float speed = 20.0f;

    private void Awake()
    {
        spawner = gameObject.transform.parent.GetComponent<Spawner>();
        //target = spawner.target;
        pokemonData = gameObject.transform.GetChild(0).GetComponent<PokemonData>();
    }
    private void Update()
    {
        gameObject.transform.LookAt(target);
        //gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, (Time.deltaTime * speed));
        if (!pokemonData.isFlying && gameObject.transform.position.y != 0)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        if (other.transform == target)
        {
            Destroy(gameObject);
        }
    }
}