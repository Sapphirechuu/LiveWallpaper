using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedEvent : MonoBehaviour
{
    string goalTime;

    public Vector3 date = new Vector3();
    public Vector3 time = new Vector3();

    public enum TimeOfDay
    {
        AM,
        PM
    };
    public TimeOfDay timeofDay;

    private string dateXString;
    private string dateYString;

    private string timeYString;
    private string timeZString;

    bool eventTriggered;

    public GameObject scriptToCall;

    // Start is called before the first frame update
    void Start()
    {
        if (date.z < 2000)
        {
            date.z += 2000;
        }

        if (date.x < 10)
        {
            dateXString = "0" + date.x.ToString();
        }
        else
        {
            dateXString = date.x.ToString();
        }

        if (date.y < 10)
        {
            dateYString = "0" + date.y.ToString();
        }
        else
        {
            dateYString = date.y.ToString();
        }

        if (time.y < 10)
        {
            timeYString = "0" + time.y.ToString();
        }
        else
        {
            timeYString = time.y.ToString();
        }

        if (time.z < 10)
        {
            timeZString = "0" + time.z.ToString();
        }
        else
        {
            timeZString = time.z.ToString();
        }

        goalTime = dateXString + "/" + dateYString + "/" + date.z.ToString() + " " + time.x.ToString() + ":" + timeYString + ":" + timeZString + " " + timeofDay.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (DateTime.Now.ToString() == goalTime && !eventTriggered)
        {
            eventTriggered = true;
            Instantiate(scriptToCall, gameObject.transform);
        }
    }
}
