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

    //Switch string season to enum eventually. Needs to be implemented into other code that uses Season Cycle
    public enum Season
    {
        WINTER,
        SPRING,
        SUMMER,
        FALL
    }

    private Season seasonEnum;
    public bool isSetWinter;

    private void Update()
    {
        if (!isSetWinter)
        {
            switch (DateTime.Now.Month)
            {
                case int n when ((n < 3) || (n > 11)):
                    seasonEnum = Season.WINTER;
                    season = "Winter";
                    break;
                case int n when ((n > 3) && (n < 6)):
                    seasonEnum = Season.SPRING;
                    season = "Spring";
                    break;
                case int n when ((n > 5) && (n < 9)):
                    seasonEnum = Season.SUMMER;
                    season = "Summer";
                    break;
                case int n when ((n > 8) && (n < 12)):
                    season = "Fall";
                    seasonEnum = Season.FALL;
                    break;
                default:
                    season = "Spring";
                    seasonEnum = Season.SPRING;
                    break;
            }
        }
        else
        {
            seasonEnum = Season.WINTER;
            season = "Winter";
        }

    }
}
