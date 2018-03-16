using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool isGamePaused;
    private bool shouldGameBePaused;

    public bool IsGamePaused { get; private set; } 
    public bool ShouldGameBePaused { get; private set; }

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
        //GameEvents.events.onGamePause += PauseGame;
        GameEvents.events.onGamePause += SetTimeScale;
	}
	
	// Update is called once per frame
	void Update () {

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if(isGamePaused){
                shouldGameBePaused = false;
            } else if (!isGamePaused){
                shouldGameBePaused = true;
            }

            GameEvents.events.onGamePause();
        } 
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

    /*public void PauseGame()
    {
        isGamePaused = true;
    }*/

    public void SetTimeScale(){
        if(shouldGameBePaused){
            Time.timeScale = 0f;
            isGamePaused = true;
            print("Stopped time");
        } else if(!shouldGameBePaused){
            Time.timeScale = 1f;
            isGamePaused = false;
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
