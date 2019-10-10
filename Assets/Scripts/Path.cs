using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [ReadOnlyField]
    public Spawner spawner;

    [ReadOnlyField]
    public Transform target;

    public float speed = 20.0f;

    private void Awake()
    {
        spawner = gameObject.transform.parent.GetComponent<Spawner>();
        target = spawner.target;
    }
    private void Update()
    {
        gameObject.transform.LookAt(target);
        //gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, (Time.deltaTime * speed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            Destroy(gameObject);
        }
    }
}