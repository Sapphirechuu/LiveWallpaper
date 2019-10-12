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

    public GameObject shinyPrefab = null;
}
