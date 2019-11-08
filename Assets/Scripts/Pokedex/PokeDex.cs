using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PokeDex : MonoBehaviour
{
    public Camera mainCamera;
    public List<PokeDexEntry> theDex;

    private string filePath = "fun.txt";
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
        LoadTheDex();
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

        //Debug.Log(theDex[7].NormalCaught);
        //Debug.Log(theDex[10].NormalCaught);
        //Debug.Log(theDex[23].NormalCaught);
        //Debug.Log(theDex[42].NormalCaught);
        //Debug.Log(theDex[43].NormalCaught);
        //Debug.Log(theDex[104].NormalCaught);
        //Debug.Log(theDex[127].NormalCaught);
        //Debug.Log(theDex[132].NormalCaught);
        //Debug.Log(theDex[169].NormalCaught);
        //Debug.Log(theDex[197].NormalCaught);

        //Debug.Log(theDex[7].PokeNumber.ToString() + "  " + theDex[7].NormalCaught);
        //Debug.Log(theDex[10].PokeNumber.ToString() + "  " + theDex[10].NormalCaught);
        //Debug.Log(theDex[23].PokeNumber.ToString() + "  " + theDex[23].NormalCaught);
        //Debug.Log(theDex[42].PokeNumber.ToString() + "  " + theDex[42].NormalCaught);
        //Debug.Log(theDex[43].PokeNumber.ToString() + "  " + theDex[43].NormalCaught);
        //Debug.Log(theDex[104].PokeNumber.ToString() + "  " + theDex[104].NormalCaught);
        //Debug.Log(theDex[127].PokeNumber.ToString() + "  " + theDex[127].NormalCaught);
        //Debug.Log(theDex[132].PokeNumber.ToString() + "  " + theDex[132].NormalCaught);
        //Debug.Log(theDex[169].PokeNumber.ToString() + "  " + theDex[169].NormalCaught);
        //Debug.Log(theDex[197].PokeNumber.ToString() + "  " + theDex[197].NormalCaught);

    }

    public void LoadTheDex()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            int curEntry = 0;
            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                theDex[curEntry].PokeNumber = int.Parse(values[0]);

                if (values[1] == "True") { theDex[curEntry].Captured = true; }
                else { theDex[curEntry].Captured = false; }

                if (values[2] == "True") { theDex[curEntry].Seen = true; }
                else { theDex[curEntry].Seen = false; }

                if (values[3] == "True") { theDex[curEntry].ShinyCaptured = true; }
                else { theDex[curEntry].ShinyCaptured = false; }

                theDex[curEntry].ShiniesSeen = int.Parse(values[4]);
                theDex[curEntry].ShiniesCaught = int.Parse(values[5]);
                theDex[curEntry].NormalSeen = int.Parse(values[6]);
                theDex[curEntry].NormalCaught = int.Parse(values[7]);
                curEntry++;
            }
        }
    }
    public void SaveTheDex()
    {
        string[] stringyDex = new string[810];

        for (int i = 0; i < theDex.Count; i++)
        {
            stringyDex[i] = 
                theDex[i].PokeNumber.ToString()     + "," +
                theDex[i].Captured.ToString()       + "," +
                theDex[i].Seen.ToString()           + "," +
                theDex[i].ShinyCaptured.ToString()  + "," +
                theDex[i].ShiniesSeen.ToString()    + "," +
                theDex[i].ShiniesCaught.ToString()  + "," +
                theDex[i].NormalSeen.ToString()     + "," +
                theDex[i].NormalCaught.ToString();
        }

        System.IO.File.WriteAllLines(filePath, stringyDex);
    }
    private void OnApplicationQuit()
    {
        SaveTheDex();
    }
}
