using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerTargetManager : MonoBehaviour
{
    private SmoothMovment movment;
    // Start is called before the first frame update
    void Start()
    {
        movment = gameObject.GetComponent<SmoothMovment>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (movment.camTarg)
        //{

        //}
    }
}
