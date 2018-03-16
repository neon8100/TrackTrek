using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool isGamePaused;

    public bool IsGamePaused { get; private set; } 

	private void Awake()
	{
        GameEvents.Initialise();
        LoadUIManagerScene();

        if(gameObject.tag != "GameManager"){
            gameObject.tag = "GameManager";
        }
	}

	// Use this for initialization
	void Start () {
        GameEvents.events.onGamePause += SetTimeScale;
	}
	
	// Update is called once per frame
	void Update () {
        print(Time.timeScale);
        print("Game is paused: "+isGamePaused);
	}

	private void FixedUpdate()
	{
       
	}

	public void StartGame(){
        GameEvents.events.onGameStart();
    }

    public void ResetGame(){
        GameEvents.events.onGameStart();
    }

    public void PauseGame()
    {
        GameEvents.events.onGamePause();
    }

    public void SetTimeScale(){
        if (!isGamePaused) { isGamePaused = true; }
        else if (isGamePaused) { isGamePaused = false; }

        if(isGamePaused){
            Time.timeScale = 0f;
            print("Stopped time");
        } else {
            Time.timeScale = 1f;
            print("Unstopped time");
        }
    }

    private void LoadUIManagerScene(){
        if (!SceneManager.GetSceneByName("UIScene").isLoaded)
        {
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
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
