using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPoke : MonoBehaviour
{

    public GameObject template;

    private int curNum = 0;


    

 
    void Update()
    {
        if (curNum < 809)
        {
            curNum++;
            GameObject cur = Instantiate(template, gameObject.transform);
            cur.GetComponent<PokeDexVisual>().PokeNum = curNum;
            cur.name = "entry " + curNum.ToString();
        }
    }
}
