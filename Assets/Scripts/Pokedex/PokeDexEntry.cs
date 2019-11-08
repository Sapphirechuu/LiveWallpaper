using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeDexEntry 
{
    public int PokeNumber;

    public bool Captured = false;
    public bool Seen = false;
    public bool ShinyCaptured = false;

    public int ShiniesSeen = 0;
    public int ShiniesCaught = 0;
    public int NormalSeen = 0;
    public int NormalCaught = 0;
}
