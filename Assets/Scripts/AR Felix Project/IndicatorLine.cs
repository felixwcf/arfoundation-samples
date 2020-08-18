using UnityEngine;
using System.Collections;

public class IndicatorLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    float counter;
    float dist;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.startWidth = .01f;
        lineRenderer.endWidth = .01f;

        dist = Vector3.Distance(origin.position, destination.position);
    }

    void Update()
    {
        if(counter < dist)
        {
            //counter += .1f / lineDrawSpeed;

            counter += 0.1f / lineDrawSpeed;
            lineRenderer.SetPosition(1, Vector3.Lerp(origin.position, destination.position, counter));


            //Vector3 pointA = origin.position;
            //Vector3 pointB = destination.position;

            //// Get the unit vector in the desired direction, multiplied by desired length and add the starting point.
            //Vector3 pointALongLine = x * Vector3.Normalize(pointB - pointA) + pointB;

            //lineRenderer.SetPosition(1, pointALongLine);
        }
    }


    //public GameObject gameObject1;          // Reference to the first GameObject
    //public GameObject gameObject2;          // Reference to the second GameObject

    //private LineRenderer line;                           // Line Renderer

    //// Use this for initialization
    //void Start()
    //{
    //    // Add a Line Renderer to the GameObject
    //    line = gameObject.GetComponent<LineRenderer>();
    //    // Set the width of the Line Renderer
    //    line.startWidth = 0.005f;
    //    line.endWidth = 0.005f;
    //    // Set the number of vertex fo the Line Renderer
    //    line.positionCount = 2;

    //    StartCoroutine(LineDraw());
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Check if the GameObjects are not null
    //    //if (gameObject1 != null && gameObject2 != null)
    //    //{
    //    //    // Update position of the two vertex of the Line Renderer
    //    //    line.SetPosition(0, gameObject1.transform.position);
    //    //    line.SetPosition(1, gameObject2.transform.position);
    //    //}
    //}

    //IEnumerator LineDraw()
    //{
    //    float t = 0;
    //    float time = 2;
    //    line.SetPosition(1, gameObject1.transform.position);
    //    Vector3 newpos;
    //    for (; t < time; t += Time.deltaTime)
    //    {
    //        newpos = Vector3.Lerp(gameObject1.transform.position, gameObject2.transform.position, t / time);
    //        line.SetPosition(1, newpos);
    //        yield return null;
    //    }
    //    line.SetPosition(1, gameObject2.transform.position);
    //}

}