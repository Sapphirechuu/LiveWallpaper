using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeDex : MonoBehaviour
{
    public Camera mainCamera;
    public List<PokeDexEntry> theDex;
    // Start is called before the first frame update
    void Start()
    {
        theDex = new List<PokeDexEntry>();
        theDex.Add(new PokeDexEntry());
        for (int i = 1; i < 810; i++)
        {
            theDex.Add(new PokeDexEntry());
            theDex[i].PokeNumber = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Got to catch them all
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "pokeman")
                {
                    if (!hit.collider.gameObject.transform.GetChild(0).GetComponent<PokemonData>().captured)
                    {
                        PokemonData pokemonHit = hit.collider.gameObject.transform.GetChild(0).GetComponent<PokemonData>();
                        pokemonHit.captured = true;
                        if (pokemonHit.shiny)
                        {
                            theDex[pokemonHit.pokeNum].Captured = true;
                            theDex[pokemonHit.pokeNum].ShinyCaptured = true;
                            theDex[pokemonHit.pokeNum].ShiniesCaught++;
                        }
                        else
                        {
                            theDex[pokemonHit.pokeNum].Captured = true;
                            theDex[pokemonHit.pokeNum].NormalCaught++;
                        }
                    }
                }
            }            
        }

        Debug.Log(theDex[7].NormalCaught);
        Debug.Log(theDex[10].NormalCaught);
        Debug.Log(theDex[23].NormalCaught);
        Debug.Log(theDex[42].NormalCaught);
        Debug.Log(theDex[43].NormalCaught);
        Debug.Log(theDex[104].NormalCaught);
        Debug.Log(theDex[127].NormalCaught);
        Debug.Log(theDex[132].NormalCaught);
        Debug.Log(theDex[169].NormalCaught);
        Debug.Log(theDex[197].NormalCaught);
        
    }
}
