using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Agent : MonoBehaviour
{

    public GameObject Head;

    /*Agent Class */

	void Start () {
        //Dont make the trail visible until the agent is actually flying. Make sure its in the foreground!
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";

        //no gravity at first
        GetComponent<Rigidbody>().isKinematic = true;
        //make the collider bigger to allow for easy touching
        State = AgentState.BeforeThrown;
    }



    public void OnThrow()
        {
            //Show the trail renderer.
            GetComponent<TrailRenderer>().enabled = true;

            //Allow gravity
            GetComponent<Rigidbody>().isKinematic = false;


           State = AgentState.Thrown;
        }


    IEnumerator DestroyAfter(float secs)
        {
            yield return new WaitForSeconds(secs);
            Destroy(gameObject);
        }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            //If we collided with the water, then we take that ring's payload object that is associated with it, and make it a child object of the object
            //to attach it to.

            Payload P = collision.gameObject.GetComponent<Payload>();
            GameObject reward = P.GetComponent<GameObject>();

            reward.transform.parent = Head.transform;

        }
    }


    public AgentState State
    {
        get;
        private set;
    }
}
