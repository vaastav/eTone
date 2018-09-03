using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Scripts;

public class RecordingManager : MonoBehaviour {

    /* The manager handles recording of the voice inputs the 
     player utilizes to control the agents. It will format it,
     and then give it to the corresponding backend manager. */

    /* --------- GENERAL VARIABLES --------- */

    const int LENGTH = 5;

    const string DEFAULT_FILENAME = "PlayerVoiceClip.wav";

    //singleton stuff
    private static RecordingManager instance;

    public BackendManager backendManager;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            instance = this;
        }

        backendManager = FindObjectOfType<BackendManager>();
    }


    public void Record()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = Microphone.Start("", false, LENGTH, 44100);

        backendManager.FormatRequest(FormatAudioFile(source.clip));

    }

    private byte[] FormatAudioFile(AudioClip clip)
    {
        byte[] buffer;

        SavWav.Save("", clip);

        var filepath = Path.Combine(Application.persistentDataPath, DEFAULT_FILENAME);

        buffer = File.ReadAllBytes(filepath);

        return buffer;
    }
}


