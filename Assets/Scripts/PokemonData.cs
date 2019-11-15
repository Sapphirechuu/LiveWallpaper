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
    [ReadOnlyField]
    public int rarity = 0;
    public int defaultRarity;
    //Shiny or not, Shiny is always Ultra Rare
    public bool shiny;
    //Pokedex Number of the Pokemon
    public int pokeNum;

    public bool isFlying;

    public bool seasonal;
    public bool weathered;
    public bool nocturnal;
    public bool diurnal;

    public bool comMorn;
    public bool comDay;
    public bool comEven;
    public bool comNight;

    public GameObject shinyPrefab = null;

    public bool captured = false;

    //For Seasonal, 0 = winter, 1 = summer, 2 = fall. Spring is default
    public List<GameObject> variants;

    private void Awake()
    {
        if (gameObject.name.Contains("Spring"))
        {
            variants[0].name = "Winter";
            variants[1].name = "Summer";
            variants[2].name = "Fall";
        }
        rarity = defaultRarity;
    }
}
