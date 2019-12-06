using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadItems : MonoBehaviour
{
    public GameObject template;

    private int curNum = 0;

    void Update()
    {
        if (curNum < 150)
        {
            GameObject cur = Instantiate(template, gameObject.transform);
            //cur.GetComponent<PokeDexVisual>().PokeNum = curNum;
            cur.transform.GetChild(0).gameObject.GetComponent<ItemVisual>().itemNum = curNum;
            cur.name = "entry " + curNum.ToString();
            curNum++;
        }
    }
}
