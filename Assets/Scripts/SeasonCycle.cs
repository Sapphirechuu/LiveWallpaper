using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeasonCycle : MonoBehaviour
{
    [ReadOnlyField]
    public DateTime date;
    [ReadOnlyField]
    public int month;
    [ReadOnlyField]
    public string season;

    private void Update()
    {
        date = DateTime.Now;
        month = date.Month;
        if (month < 3 || month > 11)
        {
            season = "Winter";
        }
        if (month > 3 && month < 6)
        {
            season = "Spring";
        }
        if (month > 5 && month < 9)
        {
            season = "Summer";
        }
        if (month > 8 && month < 12)
        {
            season = "Fall";
        }
    }
}
