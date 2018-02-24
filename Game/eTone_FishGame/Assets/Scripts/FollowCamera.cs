using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    /*The camera sits above and behind the player that rotates around the character as they turn. */

    public GameObject AgentTarget;
    private Vector3 offset;
    public float damping = 1;


	void Start () {
        Debug.Log("Agent's transform is:", AgentTarget.transform);
        offset = AgentTarget.transform.position - transform.position;
	}
	
	void LateUpdate () {
        //Orient camera behind target. Get the angle. 
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = AgentTarget.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, damping);
        Quaternion rotate = Quaternion.Euler(0, angle, 0);

        transform.position = AgentTarget.transform.position - (rotate * offset);

        transform.LookAt(AgentTarget.transform);
	}
}
