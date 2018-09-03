using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyPlaceholder : MonoBehaviour {


    public Slider accuracyBar;

    //public VoiceMove vm;

    public BackendManager bm;

	// Use this for initialization
	void Start () {
        accuracyBar = GameObject.Find("Slider").GetComponent<Slider>();
        bm = FindObjectOfType<BackendManager>();
    }
	
    public void SetAcc()
    {
        bm.Accuracy = accuracyBar.value;

    }
}
