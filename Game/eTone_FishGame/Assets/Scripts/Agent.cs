using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class Agent : MonoBehaviour {
    /* The agents are controlled by the player. A La "Angry Birds",
     * multiple are instantiated at one time and controlled sequentially
     by the player. */

    /* FOR GENERAL */

    //See scripts.enums
    public AgentState State
    {
        get;
        private set;
    }

    private Rigidbody Rigidbody;

    private TrailRenderer Trail;

    public Launcher launcher;


	void Start () {

        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.isKinematic = true;

        Trail = GetComponent<TrailRenderer>();
        Trail.enabled = false;

        State = AgentState.Idle;

        launcher = FindObjectOfType<Launcher>();

        
	}


    public void OnJump()
    {

        //Show the trail! Also add gravity 
        Trail.enabled = true;
        Rigidbody.isKinematic = false;

        FindObjectOfType<Animator>().Play("Jump");

        //Change Agent's State to Jumping
        State = AgentState.Jumping;
    }


    public event EventHandler<AgentLocation> AgentLanded;

    public class AgentLocation : EventArgs
    {
        public Vector3 Location
        {
            get;
            set;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        State = AgentState.Landed;

        EventHandler<AgentLocation> handler = AgentLanded;

        AgentLocation locn = new AgentLocation();
        locn.Location = transform.position;

        
        //If we hit water, we landed.
        if (collision.gameObject.tag == "Water") { 

            if (handler != null)
            {
                handler(this, locn);
            }
        }

    

    }


    void FixedUpdate ()
    {
 
	}




}
