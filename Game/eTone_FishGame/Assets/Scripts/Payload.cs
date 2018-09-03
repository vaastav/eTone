using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Payload : MonoBehaviour {
    /* Payload class is the reward player recieves for getting a high accuracy score */

    private GameManager gm;

    private BackendManager bm;

    public Agent agent;

    public PayloadState State;

    public int NumTimesCaptured;

    //rarity coincides with accuracy. The higher the accuracy the more rare the payload.
    public int rarity;

    // Use this for initialization
    void Start() {

        //Get current Game Manager instance
        gm = FindObjectOfType<GameManager>();

        //Get current backend instance
        bm = FindObjectOfType<BackendManager>();

        //By default the payload is uncaptured. Only changes when an agent catches it.
        State = PayloadState.Uncaptured;

        agent.AgentLanded += Agent_AgentLanded;

    }

    private void Agent_AgentLanded(object sender, Agent.AgentLocation e)
    {
        //Change our state
        State = PayloadState.Captured;

        //Instantiate the bubble object on the spot the agent landed
        Vector3 locn = new Vector3(e.Location.x, 0, e.Location.z);
        Quaternion rotation = new Quaternion();
        Instantiate(gameObject, locn, rotation);


        //SetInactive after 3 secs
        StartCoroutine(gm.ChangeActiveAfter(3, gameObject, false));

        return;
    }



    

    // Update is called once per frame
    void Update () {
	    	
	}
}
