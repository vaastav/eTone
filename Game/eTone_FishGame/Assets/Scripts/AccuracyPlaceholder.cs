using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccuracyPlaceholder : MonoBehaviour {


    public Slider accuracyBar;

    //public VoiceMove vm;

    public VoiceMoveRawVer vm;


	// Use this for initialization
	void Start () {
        accuracyBar = GameObject.Find("Slider").GetComponent<Slider>();
	}
	
    public void SetAcc()
    {
        vm.Acc = accuracyBar.value;
        Debug.Log("Set the accuracy to " + vm.Acc.ToString());
    }
}
