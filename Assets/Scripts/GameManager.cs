using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool isGamePaused = false;

	private void Awake()
	{
        GameEvents.Initialise();
	}

	// Use this for initialization
	void Start () {
        GameEvents.events.onGamePause += SetTimeScale;
	}
	
	// Update is called once per frame
	void Update () {
        print(Time.timeScale);
	}

    public void StartGame(){
        return;
    }

    public void ResetGame(){
        GameEvents.events.onGameStart();
    }

    public void PauseGame()
    {
        GameEvents.events.onGamePause();
    }

    public void SetTimeScale(){
        if(!isGamePaused){
            Time.timeScale = 0f;
            isGamePaused = true; 
        } else {
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }

	private void OnDestroy()
	{
        GameEvents.events.onGamePause -= SetTimeScale;
	}

	public void QuitGame(){
        Application.Quit();
    }

}
