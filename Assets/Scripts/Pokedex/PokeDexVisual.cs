using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PokeDexVisual : MonoBehaviour
{
    public PokeDex CurrentDex;
    public Sprite PokeSprite;
    public int PokeNum;

    private bool initalSet = false;

    // Start is called before the first frame update
    void Start()
    {
        CurrentDex = GameObject.Find("Manager").GetComponent<PokeDex>();
        //string[] sprites = AssetDatabase.FindAssets(PokeNum.ToString() + "-", new[] { "Assets/Resources/PokedexIcons/regular" });        

        //PokeSprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(sprites[0]));

        PokeSprite = Resources.Load<Sprite>("PokedexIcons/regular/4-charmander");
    }

 
    void Update()
    {
        if (!initalSet)
        {
            if (CurrentDex.theDex != null)
            {
                SetSprite();
                initalSet = true;
            }
        }
        else
        {
            UpdateSprite();
        }
    }

    void SetSprite()
    {
        if (CurrentDex.theDex[PokeNum].Captured)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = PokeSprite;
        }
        else if (CurrentDex.theDex[PokeNum].Seen)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = PokeSprite;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
        }
    }
    void UpdateSprite()
    {
        if (CurrentDex.theDex[PokeNum].Captured)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
        }
        else if (CurrentDex.theDex[PokeNum].Seen)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = PokeSprite;
            gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
        }
    }
}
