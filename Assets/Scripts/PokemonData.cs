using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonData : MonoBehaviour
{
    //Rarities: 
    //Common - 4
    //Uncommon - 3
    //Rare - 2
    //Ultra Rare - 1
    public int rarity;
    //Shiny or not, Shiny is always Ultra Rare
    public bool shiny;
    //Pokedex Number of the Pokemon
    public int pokeNum;

    public bool isFlying;

    public GameObject shinyPrefab = null;

    //For deerling, 0 = winter, 1 = summer, 2 = fall. Spring is default
    public List<GameObject> variants;

    private void Awake()
    {
        if (gameObject.name.Contains("Deerling"))
        {
            variants[0].name = "Winter";
            variants[1].name = "Summer";
            variants[2].name = "Fall";
        }
    }
}
