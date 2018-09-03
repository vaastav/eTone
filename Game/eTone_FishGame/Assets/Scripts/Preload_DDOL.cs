using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preload_DDOL : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	

}
