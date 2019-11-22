using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformingInput : MonoBehaviour
{
    public float force = 10f;
    public float forceOffset = .1f;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(inputRay, out hit))
            {
                MeshTessDeformation deformer = hit.collider.GetComponent<MeshTessDeformation>();
                if(deformer)
                {
                    Vector3 point = hit.point;
                    point += hit.normal * forceOffset;
                    deformer.AddDeformingForce(point, force);
                }
            }
        }
    }
}
