using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour {
    /* The script that will propel the agent based on the voice input
     of the player. */

    /*------ GENERAL VARIABLES ------*/
    
    public LauncherState State
    {
        get;
        set;
    }

    /*------ AGENT-MANIPULATING VARIABLES ------*/


    //The current agent
    public GameObject CurrentAgent;

    public Transform launcher_posn;

    

    /* ------ OTHER ------ */
    public BackendManager backend;

    public RecordingManager record;

    public GameManager gm;

    public Spawner spawn;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //CurrentAgent = FindObjectOfType<Agent>().gameObject;
        backend = FindObjectOfType<BackendManager>();

        /* EVENT SUBSCRIPTIONS */
        backend.AccuracyProcessed += Backend_AccuracyProcessed;

        spawn = FindObjectOfType<Spawner>();
    }

    private void Backend_AccuracyProcessed(object sender, EventArgs e)
    {
        FindObjectOfType<Agent>().OnJump();
        CurrentAgent.AddComponent<Jump>();
        State = LauncherState.Launch;
        
    }


    private void FixAgent()
    {
       
        CurrentAgent = FindObjectOfType<Agent>().gameObject;
        CurrentAgent.transform.position = launcher_posn.transform.position;
        State = LauncherState.Idle;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(State.ToString());
        }
        
        switch (State)
        {
            case LauncherState.Idle:

                if (spawn.Agents.Count - 1 == spawn.agentIndex)
                {
                    State = LauncherState.End;
                    break;
                }

                FixAgent();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    record.Record();
                }
                break;

            case LauncherState.End:
                gm.CurrentState = GameState.Finished;

                break;

            case LauncherState.Launch:

                break;
            
        }
     
    }

    private IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(2);
        State = LauncherState.Idle;
    }

}
