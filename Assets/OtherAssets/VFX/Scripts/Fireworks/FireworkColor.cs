using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.VFX;
using UnityEditor.Experimental.Rendering.HDPipeline;

public class FireworkColor : MonoBehaviour
{
    public UnityEngine.Experimental.VFX.VisualEffect visualEffect;
    public List<Gradient> gradColor;

    // Start is called before the first frame update
    void Start()
    {
        visualEffect = GetComponent<UnityEngine.Experimental.VFX.VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        int rInt = Random.Range(0, gradColor.Count);

        visualEffect.SetGradient("ExplosionColor", gradColor[rInt]);
    }
}
