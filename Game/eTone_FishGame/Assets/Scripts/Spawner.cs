using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour {

    public List<GameObject> Agents;

    public Launcher launch;

    public int agentIndex {
        get;
        private set;
        }

	// Use this for initialization
	void Start () {
        agentIndex = 0;
        Agents = new List<GameObject>(GameObject.FindGameObjectsWithTag("Agent"));

        launch = FindObjectOfType<Launcher>();

        foreach (GameObject cat in Agents)
        {
            if (cat != launch.CurrentAgent)
            {
                cat.SetActive(false); 
            }
                
        }

	}
	
	// Update is called once per frame
	void Update () {


        if (agentIndex == (Agents.Count - 1))
        {
            GameManager gm = FindObjectOfType<GameManager>();
            gm.CurrentState = Assets.Scripts.GameState.Finished;

            return;
        }

		if (launch.CurrentAgent == null && agentIndex < Agents.Count)
        {
            Debug.Log(Agents.Count.ToString());
            Debug.Log(agentIndex.ToString());
            agentIndex++;
            Agents[agentIndex].SetActive(true);
            launch.CurrentAgent = Agents[agentIndex];
        }
	}
}
