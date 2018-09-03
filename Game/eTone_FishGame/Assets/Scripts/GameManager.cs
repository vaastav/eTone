using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameManager : MonoBehaviour {

    /* GENERAL VARIABLES */
    private static GameManager GameMan_Instance;

    public GameState CurrentState;

    private BackendManager backend;

    private RecordingManager recorder;

    int intermediaryCount = 0;

    /* PAYLOAD VARS */
    public List<GameObject> payloads = new List<GameObject>(4);

    public GameObject capturedPayload;


    /* AGENT VARS */

    public Spawner spawn;

    public List<GameObject> agents;

    public Launcher launcher;


    /*----------FUNCTIONS----------*/

    public IEnumerator DestroyAfter(float seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(obj);
    }

    public IEnumerator ChangeActiveAfter(float seconds, GameObject obj, bool activity)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(activity);
    }

    void Start () {

        //Instantiate GameManager Singleton Instance
        if (GameMan_Instance == null)
        {
            GameMan_Instance = this;
        } else if (GameMan_Instance != this)
        {
            Destroy(gameObject);
            GameMan_Instance = this;
        }

        //We always must start at preload.
        CurrentState = GameState.Preload;
	}
	
	// Update is called once per frame
	void Update () {

        switch (CurrentState)
        {
            case GameState.Preload:
                //Start the game. This is for DDOL objects. 
                CurrentState = GameState.Play; 

                break;

            case GameState.Play:
                //Load the main menu. First scene we should go to.
                SceneManager.LoadScene(1);
                CurrentState = GameState.MainMenu;

                break;

            case GameState.MainMenu:
                
                break;

            case GameState.Intermediary:
                if (intermediaryCount == 0)
                {
                    StartCoroutine(InstantiateSceneVars());
                    intermediaryCount++;
                }

                break;

            case GameState.Playing:



                if (agents.Count == 0)
                {
                    int temp = 0;
                    //We ran out of agents. Tally up captured payloads (if any)
                    foreach (GameObject obj in payloads)
                    {
                        Payload p = obj.GetComponent<Payload>();
                        int multiplier = p.NumTimesCaptured;
                        int total = p.rarity * multiplier;
                        temp += total;
                    }


                }

                break;


            case GameState.Finished:

                if (Input.GetKeyDown(KeyCode.R))
                {
                    CurrentState = GameState.Play;
                }
                break;
            
        }
	}

    /* STARTING LEVEL FUNCS */

    private IEnumerator InstantiateSceneVars()
    {
       
        yield return new WaitForSeconds(2);
        backend = FindObjectOfType<BackendManager>();
        backend.AccuracyProcessed += Backend_AccuracyProcessed;

        spawn = FindObjectOfType<Spawner>();
        agents = spawn.Agents;
        launcher = FindObjectOfType<Launcher>();



        CurrentState = GameState.Playing;

    }


    /************************/


    private void CalculateScore(int score)
    {
        throw new System.Exception();
    }

    /***************************************************/

    private void Backend_AccuracyProcessed(object sender, System.EventArgs e)
    {

        float temp = backend.Accuracy;

        int rare = 1;

        if (temp < 0)
        {
            return;
        }
        else if (temp < 24 || temp >= 0)
        {
            rare = 1;
        }
        else if (temp < 49 || temp >= 25)
        {
            rare = 2;
        }
        else if (temp < 74 || temp >= 50)
        {
            rare = 3;
        }
        else if (temp < 99 || temp >= 75)
        {
            rare = 4;
        }

        capturedPayload = CheckRarity(rare);


    }

    /* Payload Functions*/
    GameObject CheckRarity(int rarity)
    {

        if (rarity > 0)
        {
            GameObject obj = payloads[rarity];
            Payload p = obj.GetComponent<Payload>();

            p.NumTimesCaptured++;
        }

        return payloads[rarity];
    }

    
}
