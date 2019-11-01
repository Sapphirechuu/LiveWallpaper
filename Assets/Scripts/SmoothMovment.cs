using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmoothMovment : MonoBehaviour {
    public GameObject overLord;
    private GameObject parent;
    private List<GameObject> path = new List<GameObject>();
    private List<Vector3> pathVecs = new List<Vector3>();
    public GameObject camerTarget;
   
    public float delay;
    public float speed = 0.5f;

    private float startTime;

    private float journeyLength;

    private int startPoint = 0;
    private int midPoint = 1;
    private int endPonit = 2;

    [ReadOnlyField]
    public int curChild = 0;

    private bool delayed = false;  

    Vector3 rotTarg;
    Vector3 posStart;
    Vector3 posTarg;           

	void Start ()
    {
        path = new List<GameObject>();
        parent = overLord.transform.GetChild(curChild).gameObject;
        PathSetting();       
    }
	

	void Update ()
    {
        if (camerTarget != null)
        {
            transform.LookAt(camerTarget.transform.position);
            if (delayed)
            {

                if (Vector3.Distance(transform.position, path[path.Count - 1].gameObject.transform.position) > 1.0)
                {
                    float distanceCovered = (Time.time - startTime) * speed;

                    float fracJourney = distanceCovered / journeyLength;

                    //Debug.Log(fracJourney);
                    transform.position = RecurveLerp(pathVecs, fracJourney);
                    if (Vector3.Distance(transform.position, path[endPonit].transform.position) < 0.1f)
                    {
                        startTime = Time.time;
                        startPoint += 2;
                        midPoint += 2;
                        endPonit += 2;
                        if (!(endPonit > (parent.transform.childCount)))
                        {
                            journeyLength = Vector3.Distance(path[startPoint].transform.position, path[midPoint].transform.position);
                        }
                    }
                }
                else
                {
                    Destroy(gameObject);
                    Destroy(camerTarget.gameObject);
                    Debug.Log("finished");
                }
            }
            else
            {
                if (delay > 0) { delay -= Time.deltaTime; }
                else
                {
                    delayed = true;

                    startTime = Time.time;

                    journeyLength = Vector3.Distance(path[startPoint].transform.position, path[midPoint].transform.position);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 Lerp1(Vector3 vec1, Vector3 vec2, float frac)
    {
        return Vector3.Lerp(vec1,vec2,frac);
    }

    private Vector3 Lerp2(Vector3 vec1, Vector3 vec2, Vector3 vec3, float frac)
    {
        return Vector3.Lerp(Lerp1(vec1, vec2, frac), Lerp1(vec2, vec3, frac), frac);
    }

    private void PathSetting()
    {
        for (int i = 0; i < (parent.transform.childCount); i++)
        {
            path.Add(parent.transform.GetChild(i).gameObject);
            pathVecs.Add(parent.transform.GetChild(i).gameObject.transform.position);
        }
    }

    private Vector3 RecurveLerp(List<Vector3> points , float frac)
    {
        List<Vector3> newPoionts = new List<Vector3>();
        Vector3 finalVector;
        if (points.Count == 1)
        {
            return points[0]; 
        }
        for (int i = 0; i < points.Count - 1; i++)
        {
            newPoionts.Add(Vector3.Lerp(points[i], points[i + 1], frac));
        }
        finalVector = RecurveLerp(newPoionts, frac);
        return finalVector;
    } 
}
