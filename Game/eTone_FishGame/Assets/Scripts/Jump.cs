using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    public float acc = 0;

    public Vector3 AgentVector;

    Vector3 EndVector;

    Vector3 Distance;

    //public float distance;

    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;

    public BackendManager backend;

    public Launcher launch;

    public GameManager gm;

    // Use this for initialization
    void Start()
    {

        backend = FindObjectOfType<BackendManager>();
        acc = backend.Accuracy / 10;

        launch = FindObjectOfType<Launcher>();

        AgentVector = transform.position;
        EndVector = new Vector3((AgentVector.x + acc), -1, 0);

        gm = FindObjectOfType<GameManager>();




        startTime = Time.time;

    }



    private void Update()
    {


        Vector3 center = (AgentVector + EndVector) * 0.5f;

        center -= new Vector3(0, 1, 0);

        // Interpolate over the arc relative to center
        Vector3 riseRelCenter = AgentVector - center;
        Vector3 setRelCenter = EndVector - center;

        // The fraction of the animation that has happened so far is
        // equal to the elapsed time divided by the desired time for
        // the total journey.
        float fracComplete = (Time.time - startTime) / journeyTime;


        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

        StartCoroutine(gm.DestroyAfter(2, gameObject));

       // StartCoroutine(gDestroyAfter(2));

        launch.State = Assets.Scripts.LauncherState.Idle;
    }


    /*
    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    */
}
