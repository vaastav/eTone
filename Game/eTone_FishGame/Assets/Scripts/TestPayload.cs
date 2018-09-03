using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPayload : MonoBehaviour {


    public GameObject payload;

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Agent")
        {
            Quaternion rotation = new Quaternion();
            Vector3 posn = new Vector3(collision.gameObject.transform.position.x, 1,
                collision.gameObject.transform.position.z);
            Instantiate(payload, posn, rotation);
        }
    }
}
