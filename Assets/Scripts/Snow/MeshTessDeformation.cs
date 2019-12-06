using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshTessDeformation : MonoBehaviour
{
    public float springForce = 20f;
    public float dampening = 5f;

    private Mesh deformingMesh;
    private Vector3[] originalVerts, displacedVerts; // holds the positions of all the mesh vertices
    private Vector3[] vertVel; // holds the velocity of each vertex
    private float uniformScale = 1f;

    // Start is called before the first frame update
    void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVerts = deformingMesh.vertices;
        displacedVerts = new Vector3[originalVerts.Length];

        for(int i = 0; i < originalVerts.Length; i++)
        {
            displacedVerts[i] = originalVerts[i];
        }

        vertVel = new Vector3[originalVerts.Length];
    }

    private void Update()
    {
        uniformScale = transform.localScale.x;

        for(int i = 0; i < displacedVerts.Length; i++)
        {
            Vector3 vel = vertVel[i];
            Vector3 displacement = displacedVerts[i] - originalVerts[i];
            displacement *= uniformScale;
            vel -= displacement * springForce * Time.deltaTime;
            vel *= 1f - dampening * Time.deltaTime;
            vertVel[i] = vel;
            displacedVerts[i] += vel * (Time.deltaTime / uniformScale);
        }

        deformingMesh.vertices = displacedVerts;
        deformingMesh.RecalculateNormals();
    }

    public void AddDeformingForce(Vector3 point, float force)
    {
        point = transform.InverseTransformDirection(point);
        for(int i = 0; i < displacedVerts.Length; i++)
        {
            Vector3 pointToVertex = displacedVerts[i] - point;
            pointToVertex *= uniformScale;
            float aForce = force / (1f + pointToVertex.sqrMagnitude);
            float velocity = aForce * Time.deltaTime;
            vertVel[i] += pointToVertex.normalized * velocity;
        }
    }
}