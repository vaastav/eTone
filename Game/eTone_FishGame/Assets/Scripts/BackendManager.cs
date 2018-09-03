using UnityEngine;
using System.Net;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;



//The backend point of communication between Vaastav's backend and the game.

public class BackendManager : MonoBehaviour
{

    private string backendURL = "http://127.0.0.1:8000/api/upload/1/uwu";

    public float Accuracy;

    public void FormatRequest(byte[] data)
    {

        byte[] buffer = data;

        WWWForm form = new WWWForm();
        form.AddBinaryData("", buffer);

        System.Diagnostics.StackTrace stackTrace = new System.Diagnostics.StackTrace();
        string callee = stackTrace.GetFrame(1).GetMethod().Name;
        StartCoroutine(HandleRequest(form, callee));

    }

    /* ------------ EVENT: PROCESSED ACCURACY ------------ */

    //EVENT: When we recieve accuracy from the backend. 
    //DELEGATE: Event Handler<AccuracyArgs> <- UNIQUE CLASS SPECIFICALLY FOR HOLDING ACC
    public event EventHandler AccuracyProcessed;

    //EVENT ARGUMENT CLASS


    //EVENT RAISER: raises AccuracyProcessed event when we finish backend req.
    public void OnAccuracyRecieved()
    {

        EventHandler handler = AccuracyProcessed;

        if (handler != null)
        {

            handler(this, EventArgs.Empty);
        }

    }


    private System.Collections.IEnumerator HandleRequest(WWWForm request, string callee)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(backendURL, request))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

                OnAccuracyRecieved();
            }
            else
            {
                Debug.Log("Form upload complete!");

                OnAccuracyRecieved();
            }
        }

    }

}



