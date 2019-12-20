using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinkle : MonoBehaviour
{
    //private float start = 1.0f;
    private float current = 1.0f;
   // private float end = 6.0f;
    private SkinnedMeshRenderer lights;
    void Start()
    {
        lights = GetComponent<SkinnedMeshRenderer>();
        Debug.Log(Shader.PropertyToID("Tiling"));
    }

    void Update()
    {

        if(lights.material.mainTextureScale.x < 6)
        {
            current += Time.deltaTime;
            lights.material.SetTextureScale(2535, new Vector2(current, 0));
        }
        if (lights.material.mainTextureScale.x >= 6)
        {
            current -= Time.deltaTime;
            lights.material.SetTextureScale(2535, new Vector2(current, 0));

        }
    }
}
