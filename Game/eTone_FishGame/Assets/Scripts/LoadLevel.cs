using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    GameManager gm;
    public int LevelNumber;
    public event EventHandler<LvlArgs> LoadTheLevel;

    public class LvlArgs : EventArgs
    {
        public int Level
        {
            get;
            set;
        }
    }

	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();
	}
	
	public void ChoseLevel()
    {
        FindObjectOfType<GameManager>().CurrentState = Assets.Scripts.GameState.Intermediary;
        SceneManager.LoadScene(LevelNumber);

    }
}
