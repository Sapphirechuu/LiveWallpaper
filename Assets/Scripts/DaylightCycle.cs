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
    public bool morning;
    [ReadOnlyField]
    public bool evening;

    [ReadOnlyField]
    public DateTime time;
    [ReadOnlyField]
    public int hour;

    private int nightHour;
    private int dayHour;
    private int morningHour;
    private int eveningHour;

    public bool isSet;

    public bool nightSet;

    private SeasonCycle seasonCycle;

    // Start is called before the first frame update
    void Start()
    {
        seasonCycle = gameObject.GetComponent<SeasonCycle>();
        string caseSwitch = seasonCycle.season;

        switch (caseSwitch)
        {
            case "Winter":
                morningHour = 7;
                dayHour = 11;
                eveningHour = 17;
                nightHour = 19;
                break;

            case "Spring":
                morningHour = 5;
                dayHour = 10;
                eveningHour = 17;
                nightHour = 20;
                break;

            case "Summer":
                morningHour = 4;
                dayHour = 9;
                eveningHour = 19;
                nightHour = 21;
                break;

            case "Fall":
                morningHour = 6;
                dayHour = 10;
                eveningHour = 18;
                nightHour = 19;
                break;

            default:
                morningHour = 7;
                dayHour = 10;
                eveningHour = 17;
                nightHour = 19;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSet)
        {
            time = DateTime.Now;
            hour = time.Hour;
            if (hour >= nightHour || hour <= dayHour)
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
                morning = false;
                evening = false;
                night = true;
                day = false;
            }
            else
            {
                day = true;
                night = false;
                morning = false;
                evening = false;
            }
        }
    }
}
