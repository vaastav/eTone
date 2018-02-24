using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceMove : MonoBehaviour {
    /* How the agent moves about the scene. Voice input is taken, recorded in a seperate class, and then passed on to the server for processing. 
       It will be passed back in JSON format which should be processed and parsed in another script. The JSON file to be passed back is finally passed back 
       here, where it is used to calulcate the vector of the agent's end position.
       */
     
    /* Attach to the agent*/

    //Reference to the agent. 
    public GameObject Agent;

    public Animator AgentAnim;

    public Vector3 StartPos;

    //Vector to be calculated. 
    Vector3 EndPos;

    float Height = 3f;

    public bool StartThrow = false;

    float inc = 0;


    /*PLACEHOLDERS
     
         I don't need start when we implemented the real controls. I also wouldn't need the float "accuracy measure" */
    public float Acc;

    public Vector3 LowAcc;
    public Vector3 MidAcc;
    public Vector3 HighAcc;
    public Vector3 Goal; 

    void Start()
    {
        Agent.transform.position = StartPos;
        SetEndPos(Acc);


    }
    void Update () {

        Debug.Log("Accuracy is: " + Acc.ToString());
        

        if (GetIsThown() && transform.position != EndPos)
        {
            if (Acc < 25) AgentAnim.Play("Roll");
            if (Acc >= 25) AgentAnim.Play("Jump");
            inc += 0.02f;
            Vector3 curr = Vector3.Lerp(StartPos, EndPos, inc);
            curr.y += Height *Mathf.Sin(Mathf.Clamp01(inc) * Mathf.PI);
            transform.position = curr;
        }
		if (GetIsThown() && transform.position == EndPos)
        {
            
            StartThrow = false;
            inc = 0;
            Vector3 tempPos = StartPos;
            StartPos = transform.position;
            EndPos = tempPos;
            
        }
        if (Input.GetKeyDown("space"))
        {
            SetIsThrown(true);
        }

        if (Input.GetKeyDown("s"))
        {
            SetEndPos(Acc);
        }
	}

    private void SetEndPos(float accuracy)
    {
        if (accuracy >= 0 && accuracy < 24.99)
        {
            EndPos = LowAcc;
        }
        else if (accuracy >= 25 && accuracy < 49.99)
        {
            EndPos = MidAcc;
        }
        else if (accuracy >= 50 && accuracy < 74.99)
        {
            EndPos = HighAcc;
        }
        else
        {
            EndPos = Goal;
        }
    }


    public void SetIsThrown(bool b)
    {
        StartThrow = b;
    }

    public bool GetIsThown()
    {
        return StartThrow;
    }

    public void ChangeAcc(float f)
    {
        Acc = f;
    }

    public float CheckAcc()
    {
        Debug.Log("Acc is" + Acc.ToString());
        return Acc;
    }
}
