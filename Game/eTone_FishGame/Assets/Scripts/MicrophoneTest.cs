using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MicrophoneTest : MonoBehaviour {

    public string[] GetV;

    public string theString;

    public AudioSource aud;



	// Use this for initialization
	void Start () {

        GetV = Microphone.devices;

        theString = (string) GetV.GetValue(0);

        foreach (string microphone in GetV)
        {
            Debug.Log("Microphone is" + microphone.ToString());
        }


        aud = GetComponent<AudioSource>();

	}

    void Update()
    {
     
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Recording now.");
            aud.clip = Microphone.Start("", true, 10, 44100);
        }

        if (Input.GetKeyUp("space"))
        {
            Debug.Log("End recording.");
            Microphone.End("");
        }
    



    }

}
