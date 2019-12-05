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
    public bool isSetWinter;

    private void Update()
    {
        if (!isSetWinter)
        {
            switch (DateTime.Now.Month)
            {
                case int n when ((n < 3) || (n > 11)):
                    season = "Winter";
                    break;
                case int n when ((n > 3) && (n < 6)):
                    season = "Spring";
                    break;
                case int n when ((n > 5) && (n < 9)):
                    season = "Summer";
                    break;
                case int n when ((n > 8) && (n < 12)):
                    season = "Fall";
                    break;
                default:
                    season = "Spring";
                    break;
            }
        }
        else
        {
            season = "Winter";
        }

    }
}
