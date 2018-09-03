using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePrompt : MonoBehaviour {

    public AudioSource audSource;

    public AudioClip clip;

    public GameManager gm;
	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();

		if (audSource == null)
        {
            audSource = FindObjectOfType<AudioSource>();
        }

        BeginVoicePrompt();
	}


    void BeginVoicePrompt()
    {
        StartCoroutine(gm.ChangeActiveAfter(3, gameObject, true));

        audSource.Play();

        StartCoroutine(gm.ChangeActiveAfter(2, gameObject, false));
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            BeginVoicePrompt();
        }
    }
}
