using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VoicePrompt : MonoBehaviour {
    /* The voice playback of the default, native speaker's phrase. */

    private AudioSource voice;

    private float timeToPlay = 5;

    private float timePassed = 0;

    private float End = 0;


	// Use this for initialization
	void Start () {

        voice = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

        timePassed += Time.deltaTime;
       

        if (timePassed >= timeToPlay && End == 0)
        {
            
            timePassed = 0;
            voice.Play();
            End++;
        }

        if (Input.GetKeyDown("p") && timePassed > timeToPlay)
        {
            voice.Play();
        }
        
	}
}
