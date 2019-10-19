using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DaylightCycle : MonoBehaviour
{
    [ReadOnlyField]
    public bool night;
    [ReadOnlyField]
    public bool day;
    [ReadOnlyField]
    public DateTime time;
    [ReadOnlyField]
    public int hour;

    public bool isSet;
    public bool nightSet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSet)
        {
            time = DateTime.Now;
            hour = time.Hour;
            if (hour >= 19 || hour <= 6)
            {
                night = true;
                day = false;
            }
            else
            {
                night = false;
                day = true;
            }
        }
        else
        {
            if (nightSet)
            {
                night = true;
                day = false;
            }
            else
            {
                day = true;
                night = false;
            }
        }
    }
}
