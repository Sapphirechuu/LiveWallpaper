using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2)
        {
            Destroy(gameObject);
        }
    }
}
